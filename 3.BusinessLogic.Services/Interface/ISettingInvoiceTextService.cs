using _4.Data.ViewModels;
using _7.Entities.Models;

namespace _3.BusinessLogic.Services.Interface
{
    public interface ISettingInvoiceTextService : IBaseService<SettingInvoiceTextViewModel>
    {
        Task<SettingInvoiceTextVMResponse> GetAllSettingInvoiceTextsAsync();
        Task<SettingInvoiceTextVMResponse> GetSettingInvoiceTextByIdAsync(SettingInvoiceTextUpdateViewModelFR request);
        Task<SettingInvoiceText?> CreateSettingInvoiceTextAsync(SettingInvoiceTextCreateViewModelFR request);
        Task<SettingInvoiceText?> UpdateSettingInvoiceTextAsync(SettingInvoiceTextUpdateViewModelFR request);
        Task<SettingInvoiceText?> DeleteSettingInvoiceTextAsync(SettingInvoiceTextDeleteViewModelFR request);
    }
}