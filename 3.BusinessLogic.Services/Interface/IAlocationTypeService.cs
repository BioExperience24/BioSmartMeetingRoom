using _4.Data.ViewModels;
using Microsoft.Identity.Client;

namespace _3.BusinessLogic.Services.Interface
{
    public interface IAlocationTypeService : IBaseService<AlocationTypeViewModel>
    {        
        Task<AlocationTypeViewModel?> CreateAsync(AlocationTypeVMDefaultFR request);

        Task<AlocationTypeViewModel?> UpdateAsync(AlocationTypeVMUpdateFR request);
        
        Task<AlocationTypeViewModel?> DeleteAsync(AlocationTypeVMDefaultFR request);
    }
}