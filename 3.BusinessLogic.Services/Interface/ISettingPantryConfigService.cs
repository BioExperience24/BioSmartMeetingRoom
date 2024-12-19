using _4.Data.ViewModels;
using _7.Entities.Models;

namespace _3.BusinessLogic.Services.Interface
{
    public interface ISettingPantryConfigService : IBaseServiceId<SettingPantryConfigViewModel>
    {
        Task<SettingPantryConfigVMResponse> GetAllSettingPantryConfigsAsync();
        Task<SettingPantryConfigVMResponse> GetSettingPantryConfigByIdAsync(SettingPantryConfigUpdateViewModelFR request);
        Task<SettingPantryConfig?> CreateSettingPantryConfigAsync(SettingPantryConfigCreateViewModelFR request);
        Task<SettingPantryConfig?> UpdateSettingPantryConfigAsync(SettingPantryConfigCreateViewModelFR request);
        Task<SettingPantryConfig?> UpdateOrCreateSettingPantryConfigAsync(SettingPantryConfigCreateViewModelFR request);
        Task<SettingPantryConfig?> DeleteSettingPantryConfigAsync(SettingPantryConfigDeleteViewModelFR request);
    }
}