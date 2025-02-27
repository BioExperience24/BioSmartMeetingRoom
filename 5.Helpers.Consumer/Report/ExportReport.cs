using _5.Helpers.Consumer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
namespace _4.Helpers.Consumer.Report;


public interface IExportReport
{
    Task<string> SendMailReport(EmailModel model);
}

public class ExportReport : IExportReport
{

    private readonly IWebHostEnvironment _env;
    private readonly IConfiguration _config;

    public ExportReport(IWebHostEnvironment env, IConfiguration config)
    {
        _env = env;
        _config = config;
    }
 

    public async Task<string> SendMailReport(EmailModel model)
    {
        var manualConfig = _config["SMTP_SETTING"];
        var emailConfigured = bool.Parse(manualConfig);
        if (emailConfigured)
        {

            try
            {
                var sendMailKit = new SendMailKit(_env);
                return await sendMailKit.SendMail(model);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        else
        {
            return "Mail Is Not Configured";
        }
    }


    


}
