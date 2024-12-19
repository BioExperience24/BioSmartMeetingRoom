using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;

namespace _3.BusinessLogic.Services.Interface;

public interface ISettingEmailTemplateService : IBaseService<SettingEmailTemplateViewModel>
{
    public Task<SettingEmailTemplateVMResponse> GetAllSettingEmailTemplatesAsync();
    public Task<SettingEmailTemplateVMResponse> GetSettingEmailTemplateByIdAsync(long id);

    public Task<SettingEmailTemplate?> CreateSettingEmailTemplateAsync(SettingEmailTemplateCreateViewModelFR request);

    public Task<SettingEmailTemplate?> UpdateSettingEmailTemplateAsync(SettingEmailTemplateUpdateViewModelFR request);

    public Task<SettingEmailTemplate?> DeleteSettingEmailTemplateAsync(SettingEmailTemplateDeleteViewModelFR request);
}