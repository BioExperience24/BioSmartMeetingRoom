using _1.PAMA.Razor.Views.Attributes;
using _3.BusinessLogic.Services.Implementation;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.PAMA.Razor.Views.Pages.Integration;

    [Authorize]
    [RejectWebviewUser]
    [PermissionAccess]
    public class IndexModel(
        IConfiguration config
    ) : PageModel
    {
        public string AppUrl { get; set; } = config["App:BaseUrl"] ?? string.Empty;
    public string ApiUrl { get; set; } = config["ApiUrls:BaseUrl"] ?? string.Empty;

    public string GetIntegrationData { get; private set; } =
        config["ApiUrls:Endpoints:Integration:GetIntegrationData"] ?? string.Empty;
    public string GetFacilities { get; private set; } =
        config["ApiUrls:Endpoints:GetFacilities"] ?? string.Empty;
        public void OnGet()
        {
        }
    }

