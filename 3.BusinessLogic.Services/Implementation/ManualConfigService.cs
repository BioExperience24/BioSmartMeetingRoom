
using _5.Helpers.Consumer;

public class ManualConfigService : IManualConfigService
{
    private readonly IConfiguration _config;

    public ManualConfigService(IConfiguration config)
    {
        _config = config;
    }


    public EmailModel GetSMTPSetting()
    {
        SMTPSettingModel smtpSetting;
        var configData = _config["SMTP_Setting"];
        if (!string.IsNullOrEmpty(configData))
        {
            smtpSetting = JsonSerializer.Deserialize<SMTPSettingModel>(configData);
        }
        else
        {
            smtpSetting = _config.GetSection("SMTP_SETTING").Get<SMTPSettingModel>();
        }

        var result = new EmailModel()
        {
            FromAddress = smtpSetting.FromAddress,
            Host = smtpSetting.Host,
            Port = smtpSetting.Port,
            Username = smtpSetting.Username,
            Password = smtpSetting.Password,
            isSecure = smtpSetting.isSecure,
            secureSocket = smtpSetting.secureSocket,
            isAuth = smtpSetting.isAuth
        };

        return result;
    }

}
