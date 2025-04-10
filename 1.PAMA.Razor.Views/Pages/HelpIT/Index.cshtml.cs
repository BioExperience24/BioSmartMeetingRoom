using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.PAMA.Razor.Views.Pages.HelpIT
{
    [Authorize]
    public class IndexModel(IConfiguration config) : PageModel
    {
        public string AppUrl { get; set; } = config["App:BaseUrl"] ?? string.Empty;
        public string ApiUrl { get; set; } = config["ApiUrls:BaseUrl"] ?? string.Empty;

        public string GetHelpDataTables { get; private set; } = 
            config["ApiUrls:Endpoints:Help:GetHelpDataTables"] ?? string.Empty;
        public string ChangeStatus { get; private set; } = 
            config["ApiUrls:Endpoints:Help:ChangeStatus"] ?? string.Empty;
        public void OnGet()
        {
        }
    }
}