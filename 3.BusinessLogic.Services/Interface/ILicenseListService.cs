using _4.Data.ViewModels;
using _7.Entities.Models;

namespace _3.BusinessLogic.Services.Interface
{
    public interface ILicenseListService : IBaseService<LicenseListViewModel>
    {
        Task<LicenseListVMResponse> GetAllLicenseListsAsync();
        Task<LicenseListVMResponse> GetLicenseListByIdAsync(LicenseListUpdateViewModelFR request);
        Task<LicenseList?> CreateLicenseListAsync(LicenseListCreateViewModelFR request);
        Task<LicenseList?> UpdateLicenseListAsync(LicenseListUpdateViewModelFR request);
        Task<LicenseList?> DeleteLicenseListAsync(LicenseListDeleteViewModelFR request);
    }
}