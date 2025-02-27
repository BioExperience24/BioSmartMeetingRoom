using _1.PAMA.Razor.Views.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.PAMA.Razor.Views.Pages.DisplayKiosk
{
    [Authorize]
    [PermissionAccess]
    public class IndexModel(
        IConfiguration config
    )
        : PageModel
    {
        public string AppUrl { get; set; } = config["App:BaseUrl"] ?? string.Empty;
        public string ApiUrl { get; set; } = config["ApiUrls:BaseUrl"] ?? string.Empty;

        public string GetKioskDisplays { get; set; } = config["ApiUrls:Endpoints:KioskDisplay:GetKioskDisplays"] ?? string.Empty;
        public string PostCreate { get; set; } = config["ApiUrls:Endpoints:KioskDisplay:PostCreate"] ?? string.Empty;
        public string PostUpdate { get; set; } = config["ApiUrls:Endpoints:KioskDisplay:PostUpdate"] ?? string.Empty;

        public void OnGet()
        {
        }
    }
}