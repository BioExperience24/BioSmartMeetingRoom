using _4.Data.ViewModels;
using _7.Entities.Models;

namespace _3.BusinessLogic.Services.Interface
{
    public interface ISettingLogConfigService : IBaseService<SettingLogConfigViewModel>
    {
        Task<SettingLogConfigVMResponse> GetAllSettingLogConfigsAsync();
        Task<SettingLogConfigVMResponse> GetSettingLogConfigByIdAsync(SettingLogConfigUpdateViewModelFR request);
        Task<SettingLogConfig?> CreateSettingLogConfigAsync(SettingLogConfigCreateViewModelFR request);
        Task<SettingLogConfig?> UpdateSettingLogConfigAsync(SettingLogConfigUpdateViewModelFR request);
        Task<SettingLogConfig?> DeleteSettingLogConfigAsync(SettingLogConfigDeleteViewModelFR request);
    }
}