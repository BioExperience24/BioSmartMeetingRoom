using System.Text.Json;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer._Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.PAMA.Razor.Views.Pages.Company;

[Authorize]
public class IndexModel : PageModel
{
    private readonly IConfiguration _config;
    private readonly ICompanyService _companyService;

    public IndexModel(IConfiguration config, ICompanyService companyService)
    {
        _config = config;
        _companyService = companyService;
    }

    public CompanyViewModel? Company { get; set; }
    public string? CompanyParse { get; set; }
    public string UploadFileCompany { get; private set; } = string.Empty;
    public string UpdateCompany { get; private set; } = string.Empty;

    public async Task OnGetAsync()
    {
        Company = await _companyService.GetOneItemAsync();

        if (Company != null)
        {
            CompanyParse = JsonSerializer.Serialize(Company);
        }

        var baseUrl = _config["ApiUrls:BaseUrl"];
        UploadFileCompany = $"{baseUrl}{_config["ApiUrls:Endpoints:UploadFileCompany"]}";
        UpdateCompany = $"{baseUrl}{_config["ApiUrls:Endpoints:UpdateCompany"]}";
    }
}
