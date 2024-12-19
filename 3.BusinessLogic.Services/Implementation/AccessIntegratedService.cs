

using System.Transactions;

namespace _3.BusinessLogic.Services.Implementation
{
    public class AccessIntegratedService(IMapper mapper, AccessIntegratedRepository repo)
        : BaseLongService<AccessIntegratedViewModel, AccessIntegrated>(repo, mapper), IAccessIntegratedService
    {
        private readonly AccessIntegratedRepository _repo = repo;

        public async Task<IEnumerable<AccessIntegratedViewModel>> GetAllItemByAccessIdAsync(string accessId)
        {
            var entity = new AccessIntegrated { AccessId = accessId };
            
            var items = await _repo.GetAllItemAsync(entity);

            var result = _mapper.Map<List<AccessIntegratedViewModel>>(items);

            return result;
        }

        public async Task<bool> AssignAsync(AccessIntegratedVMAssignFR request)
        {

            using (var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    var accessIntegratedCreates = new List<AccessIntegrated>();
                    var accessIntegratedDeletes = new List<AccessIntegrated>();

                    foreach (var item in request.StrData)
                    {
                        var vmRoom = item.Value;
                        AccessIntegrated entity = new AccessIntegrated {
                            AccessId = request.AccessId,
                            RoomId = vmRoom.Room
                        };
                        var accessIntegrateds = await _repo.GetAllItemWithEntity(entity);
                        
                        if (vmRoom.Status == 1)
                        {
                            if (!accessIntegrateds.Any())
                            {
                                entity.IsDeleted = 0;
                                // await _repo.Create(entity);
                                accessIntegratedCreates.Add(entity);
                            }
                        } else {
                            // await _repo.DeleteBulk(accessIntegrateds);
                            accessIntegratedDeletes.AddRange(accessIntegrateds);
                        }
                    }

                    if (accessIntegratedCreates.Any())
                    {
                        await _repo.CreateBulk(accessIntegratedCreates);
                    }

                    if (accessIntegratedDeletes.Any())
                    {
                        await _repo.DeleteBulk(accessIntegratedDeletes);
                    }

                    scope.Complete();

                    return true;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}