using _1.PAMA.Razor.Views.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.PAMA.Razor.Views.Pages.Setting.License;

[Authorize]
[PermissionAccess]
public class IndexModel(IConfiguration config) : PageModel
{

    public string AppUrl { get; set; } = config["App:BaseUrl"] ?? string.Empty;
    public string ApiUrl { get; set; } = config["ApiUrls:BaseUrl"] ?? string.Empty;
    public string GetLicenseSettings { get; private set; } = 
        config["ApiUrls:Endpoints:LicenseSetting:GetLicenseSettings"] ?? string.Empty;
    public string GetLicenseList { get; private set; } = 
        config["ApiUrls:Endpoints:LicenseList:GetLicenseList"] ?? string.Empty;

    public void OnGet()
    {
    }
}
