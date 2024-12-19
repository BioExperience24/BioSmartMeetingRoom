

using System.Transactions;

namespace _3.BusinessLogic.Services.Implementation;

public class AccessControlService(
    IMapper mapper,
    AccessControlRepository repo,
    AccessControllerFalcoRepository accessCtrlFalcoRepo,
    AccessIntegratedRepository accessIntegratedRepo)
    : BaseService<AccessControlViewModel, AccessControl>(repo, mapper), IAccessControlService
{
    private readonly AccessControlRepository _repo = repo;
    private readonly AccessControllerFalcoRepository _accessCtrlFalcoRepo = accessCtrlFalcoRepo;
    private readonly AccessIntegratedRepository _accessIntegratedRepo = accessIntegratedRepo;

    public async Task<IEnumerable<AccessControlViewModel>> GetAllItemAsync()
    {
        var items = await _repo.GetAllItemAsync();

        var result = _mapper.Map<List<AccessControlViewModel>>(items);

        return result;
    }

    public override async Task<AccessControlViewModel?> GetById(string id)
    {
        var item = await _repo.GetItemByIdAsync(id);
    
        return item == null ? null : _mapper.Map<AccessControlViewModel>(item);
    }

    // TODO: Jika menu meeting room - room management sudah di buat, merge method ini
    public async Task<IEnumerable<AccessControlVMRoom>> GetAllItemRoomAsync()
    {
        var items = await _repo.GetAllItemRoomAsync();

        // TODO: kurang get data room_detail, ada di controller room method getData

        var result = _mapper.Map<List<AccessControlVMRoom>>(items);

        return result;
    }

    // TODO: Jika menu meeting room - room management sudah di buat, merge method ini
    public async Task<IEnumerable<AccessControlVMRoom>> GetAllItemRoomRoomDisplayAsync()
    {
        var items = await _repo.GetAllItemRoomRoomDisplayAsync();

        var result = _mapper.Map<List<AccessControlVMRoom>>(items);

        return result;
    }

    public async Task<IEnumerable<AccessControlVMRoom>> GetAllItemRoomWithRadidsAsycn(string[] radIds)
    {
        var items = await _repo.GetAllItemRoomWithRadidsAsycn(radIds);

        var result = _mapper.Map<List<AccessControlVMRoom>>(items);

        return result;
    }
    
    public async Task<AccessControlViewModel?> CreateAsync(AccessControlVMCreateFR request)
    {

        using (var scope = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled
        ))
        {
            try
            {
                DateTime now = DateTime.Now;
                // var radid = _Random.Numeric(10);
                var radid = new DateTimeOffset(now).ToUnixTimeMilliseconds() + _Random.Numeric(3);

                // Data access_controller_falco (jika memilih request.Type == falcoid)
                if (request.Type == "falcoid")
                {
                    var accessControllerFalco = new AccessControllerFalco {
                        AccessId = radid.ToString(),
                        GroupAccess = request.GroupAccess ?? string.Empty,
                        FalcoIp = request.IpController ?? string.Empty,
                        UnitNo = request.UnitNo,
                        IsDeleted = 0
                    };

                    await _accessCtrlFalcoRepo.Create(accessControllerFalco);
                }

                
                // Data access_integrated
                if (request.Room.Any())
                {
                    var accessIntegratedEntities = new List<AccessIntegrated>();
                    foreach (var item in request.Room)
                    {
                        accessIntegratedEntities.Add(new AccessIntegrated {
                            AccessId = radid.ToString(),
                            RoomId = item,
                            IsDeleted = 0
                        });
                    }
                    await _accessIntegratedRepo.CreateBulk(accessIntegratedEntities);
                }

                // Data access_control
                var accessControlEntity = _mapper.Map<AccessControl>(request);
                accessControlEntity.Id = radid.ToString();
                accessControlEntity.IsDeleted = 0;
                accessControlEntity.CreatedAt = now;
                accessControlEntity.UpdatedAt = now;
                accessControlEntity.ControllerList = string.Empty;
                accessControlEntity.RoomControllerFalco = string.Empty;
                // accessControlEntity.CreatedBy = ""; // comment jika data auth sudah ada

                var accessControl = await _repo.AddAsync(accessControlEntity);

                scope.Complete();

                return _mapper.Map<AccessControlViewModel>(accessControl);
            }
            catch (Exception)
            {
                throw;
            }
        }
        
    }

    public async Task<AccessControlViewModel?> UpdateAsync(AccessControlVMUpdateFR request)
    {

        var accessEntity = await _repo.GetByIdAsync(request.Id);

        if (accessEntity == null)
        {
            return null;
        }

        using (var scope = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled
        ))
        {
            try
            {
                // Data access_controller_falco (jika memilih request.Type == falcoid)
                var accessCtrlFalcoEntity = await _accessCtrlFalcoRepo.GetOneByField("access_id", accessEntity.Id!, true);
                if (request.Type == "falcoid")
                {
                    if (accessCtrlFalcoEntity == null)
                    {
                        var accessControllerFalco = new AccessControllerFalco {
                            AccessId = accessEntity.Id!,
                            GroupAccess = request.GroupAccess ?? string.Empty,
                            FalcoIp = request.IpController ?? string.Empty,
                            UnitNo = request.UnitNo,
                            IsDeleted = 0
                        };

                        await _accessCtrlFalcoRepo.Create(accessControllerFalco);
                    } else {
                        if (
                            accessCtrlFalcoEntity.GroupAccess != request.GroupAccess
                            || accessCtrlFalcoEntity.FalcoIp != request.IpController
                            || accessCtrlFalcoEntity.UnitNo != request.UnitNo)
                        {
                            accessCtrlFalcoEntity.GroupAccess = request.GroupAccess ?? string.Empty;
                            accessCtrlFalcoEntity.FalcoIp = request.IpController ?? string.Empty;
                            accessCtrlFalcoEntity.UnitNo = request.UnitNo;
                            accessCtrlFalcoEntity.IsDeleted = 0;
                            
                            await _accessCtrlFalcoRepo.Update(accessCtrlFalcoEntity);
                        }
                    }
                } else {
                    if (accessCtrlFalcoEntity != null)
                    {
                        accessCtrlFalcoEntity.IsDeleted = 1;
                        await _accessCtrlFalcoRepo.Update(accessCtrlFalcoEntity);
                    }
                }
                
                // Data access_integrated
                var accessIntegrateds = await _accessIntegratedRepo.GetListByField("access_id", accessEntity.Id!);
                if (accessIntegrateds.Any())
                {
                    await _accessIntegratedRepo.DeleteBulk(accessIntegrateds);
                }

                if (request.Room.Any())
                {
                    var accessIntegratedEntities = new List<AccessIntegrated>();
                    foreach (var item in request.Room)
                    {
                        accessIntegratedEntities.Add(new AccessIntegrated {
                            AccessId = accessEntity!.Id,
                            RoomId = item,
                            IsDeleted = 0
                        });
                    }

                    await _accessIntegratedRepo.CreateBulk(accessIntegratedEntities);
                }

                
                // Data access_control
                DateTime now = DateTime.Now;
                _mapper.Map(request, accessEntity);
                if (request.Type == "falcoid")
                {
                    accessEntity.AccessId = null;
                }
                accessEntity.UpdatedAt = now;
                // accessEntity.UpdatedBy = ""; // comment jika data auth sudah ada
                accessEntity.ControllerList = string.Empty;
                accessEntity.RoomControllerFalco = string.Empty;

                await _repo.UpdateAsync(accessEntity);
                
                scope.Complete();

                return _mapper.Map<AccessControlViewModel>(accessEntity);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public async Task<AccessControlViewModel?> DeleteAsync(AccessControlVMDeleteFR request)
    {
        var accessControl = await _repo.GetByIdAsync(request.Id);

        if (accessControl == null)
        {
            return null;
        }

        using (var scope = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled
        ))
        {
            try
            {
                var accessCtrlFalco = await _accessCtrlFalcoRepo.GetOneByField("access_id", accessControl.Id!);
                if (accessCtrlFalco != null)
                {
                    accessCtrlFalco.IsDeleted = 1;
                    await _accessCtrlFalcoRepo.Update(accessCtrlFalco);
                }

                DateTime now = DateTime.Now;
                
                accessControl.UpdatedAt = now;
                // accessControl.UpdatedBy = ""; // comment jika data auth sudah ada
                accessControl.ControllerList = string.Empty;
                accessControl.RoomControllerFalco = string.Empty;
                accessControl.IsDeleted = 1;

                await _repo.UpdateAsync(accessControl);

                scope.Complete();

                return _mapper.Map<AccessControlViewModel>(accessControl);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
