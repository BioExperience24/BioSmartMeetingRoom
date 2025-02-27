using _5.Helpers.Consumer;

namespace _3.BusinessLogic.Services.Interface;
public interface IManualConfigService
{
    EmailModel GetSMTPSetting();
}