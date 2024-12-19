using _4.Data.ViewModels;
using _7.Entities.Models;

namespace _3.BusinessLogic.Services.Interface
{
    public interface ISettingSmtpService : IBaseService<SettingSmtpViewModel>
    {
        Task<SettingSmtpVMResponse> GetAllSettingSmtpsAsync();
        Task<SettingSmtpVMResponse> GetSettingSmtpByIdAsync(SettingSmtpUpdateViewModelFR request);
        Task<SettingSmtp?> CreateSettingSmtpAsync(SettingSmtpCreateViewModelFR request);
        Task<SettingSmtp?> UpdateSettingSmtpAsync(SettingSmtpUpdateViewModelFR request);
        Task<SettingSmtp?> DeleteSettingSmtpAsync(SettingSmtpDeleteViewModelFR request);
    }
}