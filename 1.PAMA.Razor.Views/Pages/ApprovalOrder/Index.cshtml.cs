using _1.PAMA.Razor.Views.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.PAMA.Razor.Views.Pages.ApprovalOrder
{
    [Authorize]
    [RejectWebviewUser]
    public class IndexModel(IConfiguration config) : PageModel
    {
        public string AppUrl { get; set; } = config["App:BaseUrl"] ?? string.Empty;
        public string ApiUrl { get; set; } = config["ApiUrls:BaseUrl"] ?? string.Empty;

        public string PantryPackages { get; private set; } = 
            config["ApiUrls:Endpoints:PantryPackage:GetAll"] ?? string.Empty;

        public string GetBookingWithApprovalDataTables { get; private set; } = 
            config["ApiUrls:Endpoints:PantryTransaksi:GetWithApprovalDataTables"] ?? string.Empty;
        public string ProcessOrderApproval { get; private set; } = 
            config["ApiUrls:Endpoints:PantryTransaksi:ProcessOrderApproval"] ?? string.Empty;
        public string GetOrderDetail { get; private set; } = 
            config["ApiUrls:Endpoints:PantryTransaksi:GetPrintOrderApproval"] ?? string.Empty;

        public void OnGet()
        {
        }
    }
}