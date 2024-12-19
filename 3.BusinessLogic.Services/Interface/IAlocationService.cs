
using _4.Data.ViewModels;
using _7.Entities.Models;

namespace _3.BusinessLogic.Services.Interface
{
    public interface IAlocationService : IBaseService<AlocationViewModel>
    {
        Task<IEnumerable<AlocationViewModel>> GetItemsAsync();

        Task<IEnumerable<AlocationViewModel>> GetItemsByTypeAsync(string type);

        Task<AlocationViewModel?> CreateAsync(AlocationVMCreateFR request);

        Task<AlocationViewModel?> UpdateAsync(AlocationVMUpdateFR request);
        
        Task<AlocationViewModel?> DeleteAsync(AlocationVMDefaultFR request);
    }
}