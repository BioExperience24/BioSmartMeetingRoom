using _4.Data.ViewModels;
using _7.Entities.Models;

namespace _3.BusinessLogic.Services.Interface
{
    public interface ISettingInvoiceConfigService : IBaseService<SettingInvoiceConfigViewModel>
    {
        Task<SettingInvoiceConfigVMResponse> GetAllSettingInvoiceConfigsAsync();
        Task<SettingInvoiceConfigVMResponse> GetSettingInvoiceConfigByIdAsync(long id);
        Task<SettingInvoiceConfig?> CreateSettingInvoiceConfigAsync(SettingInvoiceConfigCreateViewModelFR request);
        Task<SettingInvoiceConfig?> UpdateSettingInvoiceConfigAsync(SettingInvoiceConfigUpdateViewModelFR request);
        Task<SettingInvoiceConfig?> DeleteSettingInvoiceConfigAsync(SettingInvoiceConfigDeleteViewModelFR request);
    }
}