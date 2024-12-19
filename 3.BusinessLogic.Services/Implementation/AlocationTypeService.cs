using System.Transactions;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _6.Repositories.Repository;
using _7.Entities.Models;
using AutoMapper;

namespace _3.BusinessLogic.Services.Implementation
{
    public class AlocationTypeService : BaseService<AlocationTypeViewModel, AlocationType>, IAlocationTypeService
    {
        private readonly AlocationTypeRepository _repo;
    
        private readonly IMapper __mapper;

        public AlocationTypeService(AlocationTypeRepository repo, IMapper mapper) : base(repo, mapper)
        { 
            _repo = repo;
            __mapper = mapper;
        }

        public async Task<AlocationTypeViewModel?> CreateAsync(AlocationTypeVMDefaultFR request)
        {
            using (var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    AlocationType item = __mapper.Map<AlocationType>(request);
                    item.IsDeleted = 0;
                    item.CreatedAt = DateTime.Now;
                    // item.CreatedBy = // uncomment jika sudah memiliki auth


                    var alocationType = await _repo.AddAsync(item);

                    // Commit transaction if all commands succeed, transaction will auto-rollback
                    // when disposed if either commands fails
                    scope.Complete();
                
                    var result = __mapper.Map<AlocationTypeViewModel>(alocationType);

                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
                
            }
        }

        public async Task<AlocationTypeViewModel?> UpdateAsync(AlocationTypeVMUpdateFR request)
        {
            var alocationType = await _repo.GetByIdAsync(request.Id);

            if (alocationType == null)
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
                    __mapper.Map(request, alocationType);

                    // alocationType.InvoiceStatus = request.InvoiceStatus;
                    alocationType.UpdatedAt = DateTime.Now;
                    // alocationType.UpdatedBy = // uncomment jika sudah memiliki auth

                    await _repo.UpdateAsync(alocationType);
                    // if (await _repo.UpdateAsync(alocationType) < 1)
                    // {
                    //     return null;
                    // }

                    // Commit transaction if all commands succeed, transaction will auto-rollback
                    // when disposed if either commands fails
                    scope.Complete();

                    var result = __mapper.Map<AlocationTypeViewModel>(alocationType);

                    return result;
                }
                catch (Exception)
                {                    
                    throw;
                }
            }
        }

        public async Task<AlocationTypeViewModel?> DeleteAsync(AlocationTypeVMDefaultFR request)
        {
            var alocationType = await _repo.GetByIdAsync(request.Id);

            if (alocationType == null)
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
                    __mapper.Map(request, alocationType);

                    alocationType.IsDeleted = 1;
                    alocationType.UpdatedAt = DateTime.Now;
                    // alocationType.UpdatedBy = // uncomment jika sudah memiliki auth

                    await _repo.UpdateAsync(alocationType);

                    // if (await _repo.UpdateAsync(alocationType) < 1)
                    // {
                    //     return null;
                    // }
                    
                    // Commit transaction if all commands succeed, transaction will auto-rollback
                    // when disposed if either commands fails
                    scope.Complete();

                    var result = __mapper.Map<AlocationTypeViewModel>(alocationType);

                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}