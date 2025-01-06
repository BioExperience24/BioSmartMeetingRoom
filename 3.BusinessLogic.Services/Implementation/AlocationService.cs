using System.Text.Json;
using System.Transactions;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer._Common;
using _6.Repositories.Repository;
using _7.Entities.Models;
using AutoMapper;

namespace _3.BusinessLogic.Services.Implementation
{
    public class AlocationService : BaseService<AlocationViewModel, Alocation>, IAlocationService
    {
        private readonly AlocationRepository _repo;
        
        private readonly IMapper __mapper;

        public AlocationService(AlocationRepository repo, IMapper mapper) : base(repo, mapper)
        { 
            _repo = repo;
            __mapper = mapper;
        }

        public async Task<IEnumerable<AlocationViewModel>> GetItemsAsync()
        {
            var list = await _repo.GetItemsAsync();
            
            var result = __mapper.Map<List<AlocationViewModel>>(list);

            return result;
        }

        public async Task<IEnumerable<AlocationViewModel>> GetItemsByTypeAsync(string type)
        {
            var list = await _repo.GetItemByTypeAsync(type);
            
            var result = __mapper.Map<List<AlocationViewModel>>(list);

            return result;
        }

        public async Task<AlocationViewModel?> CreateAsync(AlocationVMCreateFR request)
        {
            using (var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            ))
            {
                try
                {
                    Alocation item = __mapper.Map<Alocation>(request);

                    DateTime now = DateTime.Now;
                    var gen = now.ToString("yyyyMMddHHmmss");
                    var kode = request.Id != "" 
                        ? request.Id
                        : gen;

                    // request.Id = $"{gen}{_String.RandomAlNum(4)}";
                    request.Id = $"{gen}{_Random.AlphabetNumeric(4)}";

                    item.DepartmentCode = kode;
                    item.CreatedAt = now;
                    // item.CreatedBy = // uncomment jika sudah memiliki auth
                    item.IsDeleted = 0;

                    var alocation = await _repo.AddAsync(item);

                    scope.Complete();

                    var result = __mapper.Map<AlocationViewModel>(alocation);

                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    

        public async Task<AlocationViewModel?> UpdateAsync(AlocationVMUpdateFR request)
        {
            var alocation = await _repo.GetByIdAsync(request.Id);

            if (alocation == null)
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
                    __mapper.Map(request, alocation);

                    alocation.UpdatedAt = DateTime.Now;
                    // alocation.UpdatedBy = // uncomment jika sudah memiliki auth

                    await _repo.UpdateAsync(alocation);
                    // if (!await _repo.UpdateItemAsync(alocation))
                    // {
                    //     return null;
                    // }

                    scope.Complete();

                    var result = __mapper.Map<AlocationViewModel>(alocation);

                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task<AlocationViewModel?> DeleteAsync(AlocationVMDefaultFR request)
        {
            var alocation = await _repo.GetByIdAsync(request.Id);

            if (alocation == null)
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
                    __mapper.Map(request, alocation);

                    alocation.IsDeleted = 1;
                    alocation.UpdatedAt = DateTime.Now;
                    // alocation.UpdatedBy = // uncomment jika sudah memiliki auth

                    await _repo.UpdateAsync(alocation);
                    // if (!await _repo.UpdateItemAsync(alocation))
                    // {
                    //     return null;
                    // }

                    scope.Complete();

                    var result = __mapper.Map<AlocationViewModel>(alocation);

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