using _1.PAMA.Razor.Views.Attributes;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace _1.PAMA.Razor.Views.Pages.Company;

[Authorize]
[PermissionAccess]
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
