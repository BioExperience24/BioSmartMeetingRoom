using _4.Data.ViewModels;
using _7.Entities.Models;

namespace _3.BusinessLogic.Services.Interface
{
    public interface ILicenseSettingService : IBaseService<LicenseSettingViewModel>
    {
        Task<LicenseSettingVMResponse> GetAllLicenseSettingsAsync();
        Task<LicenseSettingVMResponse> GetLicenseSettingByIdAsync(LicenseSettingUpdateViewModelFR request);
        Task<LicenseSetting?> CreateLicenseSettingAsync(LicenseSettingCreateViewModelFR request);
        Task<LicenseSetting?> UpdateLicenseSettingAsync(LicenseSettingUpdateViewModelFR request);
        Task<LicenseSetting?> DeleteLicenseSettingAsync(LicenseSettingDeleteViewModelFR request);
    }
}