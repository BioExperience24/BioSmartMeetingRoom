
using System.Transactions;
using _2.BusinessLogic.Services.Interface;
using _5.Helpers.Consumer._Encryption;
using _5.Helpers.Consumer._Encryption._Secure;

namespace _3.BusinessLogic.Services.Implementation;

public class BuildingFloorService : BaseLongService<BuildingFloorViewModel, BuildingFloor>, IBuildingFloorService
{
    private readonly BuildingFloorRepository _repo;
    private readonly IMapper __mapper;
    private readonly _Aes _aes;
    private readonly IAttachmentListService _attachmentListService;
    private readonly ISecureService _secureService;

    public BuildingFloorService(
        BuildingFloorRepository repo, 
        IMapper mapper, 
        IConfiguration config,
        IAttachmentListService attachmentListService,
        ISecureService secureService) 
        : base(repo, mapper)
    { 
        _repo = repo;
        __mapper = mapper;

        _aes = new _Aes(config["EncryptSetting:AesKeyEncryptor"] ?? "VeRYV3rYSUp3RdUuP3RrRsEcr3tKeY!!");

        _secureService = secureService;

        attachmentListService.SetTableFolder(
            config["UploadFileSetting:tableFolder:buildingFloor"] ?? "floor");
        attachmentListService.SetExtensionAllowed(config["UploadFileSetting:imageExtensionAllowed"]!);
        attachmentListService.SetTypeAllowed(config["UploadFileSetting:imageContentTypeAllowed"]!);
        attachmentListService.SetSizeLimit(Convert.ToInt32(config["UploadFileSetting:imageSizeLimit"] ?? "8")); // MB
        _attachmentListService = attachmentListService;
    }

    public async Task<IEnumerable<BuildingFloorViewModel>> GetAllItemAsync(BuildingFloorViewModel? vm = null)
    {
        BuildingFloor entity = new();

        if (vm?.BuildingEncId != null)
        {
            // var buildingId = _aes.Decrypt(vm?.BuildingEncId!);
            var buildingId = _secureService.Decrypt(vm?.BuildingEncId!);
            entity.BuildingId = buildingId != null ? Convert.ToInt64(buildingId) : null;
        }

        var items = await _repo.GetAllItemAsync(entity);

        var results = __mapper.Map<List<BuildingFloorViewModel>>(items);

        // List untuk task generasi thumbnail
        var tasks = results.Select(async item =>
        {
            // item.EncId = item.Id != null ? _aes.Encrypt(item.Id.ToString()!) : null;
            item.EncId = item.Id != null ? _secureService.Encrypt(item.Id.ToString()!) : null;
            // item.BuildingEncId = item.BuildingId != null ? _aes.Encrypt(item.BuildingId.ToString()!) : null;
            item.BuildingEncId = item.BuildingId != null ? _secureService.Encrypt(item.BuildingId.ToString()!) : null;
            item.Image = item.Image != null
                ? await _attachmentListService.GenerateThumbnailBase64(item.Image, 480)
                : "";
            
            item.Id = null;
            item.BuildingId = null;
        }).ToList();

        // Tunggu semua task selesai
        await Task.WhenAll(tasks);

        return results;
    }

    public async Task<BuildingFloorViewModel?> GetItemByEntityAsync(BuildingFloorViewModel vm)
    {
        // var floorId = vm.EncId != null ? _aes.Decrypt(vm.EncId) : null;
        var floorId = vm.EncId != null ? _secureService.Decrypt(vm.EncId) : null;
        // var buildingId = vm.BuildingEncId != null ? _aes.Decrypt(vm.BuildingEncId) : null;
        var buildingId = vm.BuildingEncId != null ? _secureService.Decrypt(vm.BuildingEncId) : null;

        BuildingFloor entity =  new BuildingFloor {
            Id = floorId != null ? Convert.ToInt64(floorId) : null,
            BuildingId = buildingId != null ? Convert.ToInt64(buildingId) : null,
            Name = vm.Name
        };

        var item = await _repo.GetItemByEntityAsync(entity, false);

        if (item == null)
        {
            return null;
        }

        var result = __mapper.Map<BuildingFloorViewModel>(item);

        // result.EncId = result.Id != null ? _aes.Encrypt(result.Id.ToString()!) : null;
        result.EncId = result.Id != null ? _secureService.Encrypt(result.Id.ToString()!) : null;
        // result.BuildingEncId = result.BuildingId != null ? _aes.Encrypt(result.BuildingId.ToString()!) : null;
        result.BuildingEncId = result.BuildingId != null ? _secureService.Encrypt(result.BuildingId.ToString()!) : null;
        result.Image = result.Image != null ? await _attachmentListService.GenerateThumbnailBase64(result.Image, 480) : "";

        result.Id = null;
        result.BuildingId = null;

        return result;
    }

    public override async Task<BuildingFloorViewModel?> Create(BuildingFloorViewModel vm)
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
                // var floorId = now.ToString("yyyyMMddHHmmss");
                var floorId = new DateTimeOffset(now).ToUnixTimeMilliseconds() + _Random.Numeric(3);
                // var buildingId = vm.BuildingEncId != null ? _aes.Decrypt(vm.BuildingEncId) : null;
                var buildingId = vm.BuildingEncId != null ? _secureService.Decrypt(vm.BuildingEncId) : null;

                BuildingFloor entity =  new BuildingFloor {
                    BuildingId = buildingId != null ? Convert.ToInt64(buildingId) : null,
                    Name = vm.Name
                };

                var lastBuildingFloor = await _repo.GetItemByEntityAsync(entity);
                
                var position = lastBuildingFloor?.Position + 1 ?? 1;

                entity.Id = floorId;
                entity.CreatedAt = now;
                entity.UpdatedAt = now;
                // entity.CreatedBy = ""; // uncomment jika sudah ada data auth
                entity.Position = position;
                entity.IsDeleted = 0;


                var item = await _repo.Create(entity);
                
                scope.Complete();

                var result = __mapper.Map<BuildingFloorViewModel>(item);

                // result.EncId = result.Id != null ? _aes.Encrypt(result.Id.ToString()!) : null;
                result.EncId = result.Id != null ? _secureService.Encrypt(result.Id.ToString()!) : null;
                // result.BuildingEncId = result.BuildingId != null ? _aes.Encrypt(result.BuildingId.ToString()!) : null;
                result.BuildingEncId = result.BuildingId != null ? _secureService.Encrypt(result.BuildingId.ToString()!) : null;
                result.Image = result.Image != null ? await _attachmentListService.GenerateThumbnailBase64(result.Image, 480) : "";

                result.Id = null;
                result.BuildingId = null;

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public override async Task<BuildingFloorViewModel?> Update(BuildingFloorViewModel vm)
    {
        // var floorId = vm.EncId != null ? _aes.Decrypt(vm.EncId) : null;
        var floorId = vm.EncId != null ? _secureService.Decrypt(vm.EncId) : null;
        // var buildingId = vm.BuildingEncId != null ? _aes.Decrypt(vm.BuildingEncId) : null;
        var buildingId = vm.BuildingEncId != null ? _secureService.Decrypt(vm.BuildingEncId) : null;

        BuildingFloor entity =  new BuildingFloor {
            Id = floorId != null ? Convert.ToInt64(floorId) : null,
            BuildingId = buildingId != null ? Convert.ToInt64(buildingId) : null,
            Name = vm.Name
        };

        var item = await _repo.GetItemByEntityAsync(entity, false);

        if (item == null)
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
                DateTime now = DateTime.Now;
                item.Name = vm.Name;
                item.UpdatedAt = now;
                // item.UpdatedBy = ""; // uncomment jika auth sudah ada

                await _repo.Update(item);
                
                scope.Complete();

                var result = __mapper.Map<BuildingFloorViewModel>(item);

                // result.EncId = result.Id != null ? _aes.Encrypt(result.Id.ToString()!) : null;
                result.EncId = result.Id != null ? _secureService.Encrypt(result.Id.ToString()!) : null;
                // result.BuildingEncId = result.BuildingId != null ? _aes.Encrypt(result.BuildingId.ToString()!) : null;
                result.BuildingEncId = result.BuildingId != null ? _secureService.Encrypt(result.BuildingId.ToString()!) : null;
                result.Image = result.Image != null ? await _attachmentListService.GenerateThumbnailBase64(result.Image, 480) : "";

                result.Id = null;
                result.BuildingId = null;

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public async Task<BuildingFloorViewModel?> Delete(BuildingFloorViewModel vm)
    {
        // var floorId = vm.EncId != null ? _aes.Decrypt(vm.EncId) : null;
        var floorId = vm.EncId != null ? _secureService.Decrypt(vm.EncId) : null;

        BuildingFloor entity =  new BuildingFloor {
            Id = floorId != null ? Convert.ToInt64(floorId) : null,
        };

        var item = await _repo.GetItemByEntityAsync(entity, false);

        if (item == null)
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
                DateTime now = DateTime.Now;
                item.IsDeleted = 1;
                item.UpdatedAt = now;
                // item.UpdatedBy = ""; // uncomment jika auth sudah ada

                await _repo.Update(item);
                
                scope.Complete();

                var result = __mapper.Map<BuildingFloorViewModel>(item);

                // result.EncId = result.Id != null ? _aes.Encrypt(result.Id.ToString()!) : null;
                result.EncId = result.Id != null ? _secureService.Encrypt(result.Id.ToString()!) : null;
                // result.BuildingEncId = result.BuildingId != null ? _aes.Encrypt(result.BuildingId.ToString()!) : null;
                result.BuildingEncId = result.BuildingId != null ? _secureService.Encrypt(result.BuildingId.ToString()!) : null;
                result.Image = result.Image != null ? await _attachmentListService.GenerateThumbnailBase64(result.Image, 480) : "";

                result.Id = null;
                result.BuildingId = null;

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public async Task<(string?, string?)> DoUploadAsync(IFormFile? file)
    {
        if (file != null && file.Length > 0)
        {
            return await _attachmentListService.FileUploadProcess(file);
        }

        return (null, null);
    }

    public async Task<BuildingFloorViewModel?> UploadAsync(BuildingFloorViewModel vm)
    {
        using (var scope = new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
            TransactionScopeAsyncFlowOption.Enabled
        ))
        {
            try
            {
                // var floorId = vm.EncId != null ? _aes.Decrypt(vm.EncId) : null;
                var floorId = vm.EncId != null ? _secureService.Decrypt(vm.EncId) : null;

                BuildingFloor entity =  new BuildingFloor {
                    Id = floorId != null ? Convert.ToInt64(floorId) : null,
                };

                var item = await _repo.GetItemByEntityAsync(entity, false);

                if (item == null)
                {
                    if (vm.Image != null)
                    {
                        await _attachmentListService.DeleteFile(vm.Image);
                    }
                    return null;
                }

                var oldImage = item.Image;

                DateTime now = DateTime.Now;

                if (vm.Image != null)
                {
                    item.Image = vm.Image;
                }                
                item.UpdatedAt = now;
                // item.UpdatedBy = ""; // uncomment jika auth sudah ada

                await _repo.Update(item);
                
                scope.Complete();

                if (oldImage != null && vm.Image != null)
                {
                    await _attachmentListService.DeleteFile(oldImage);
                }

                var result = __mapper.Map<BuildingFloorViewModel>(item);

                // result.EncId = result.Id != null ? _aes.Encrypt(result.Id.ToString()!) : null;
                result.EncId = result.Id != null ? _secureService.Encrypt(result.Id.ToString()!) : null;
                // result.BuildingEncId = result.BuildingId != null ? _aes.Encrypt(result.BuildingId.ToString()!) : null;
                result.BuildingEncId = result.BuildingId != null ? _secureService.Encrypt(result.BuildingId.ToString()!) : null;
                result.Image = result.Image != null ? await _attachmentListService.GenerateThumbnailBase64(result.Image, 480) : "";

                result.Id = null;
                result.BuildingId = null;

                return result;
            }
            catch (Exception)
            {
                if (vm.Image != null)
                {
                    await _attachmentListService.DeleteFile(vm.Image);
                }
                throw;
            }
        }
    }
}
