using _1.PAMA.Razor.Views.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.PAMA.Razor.Views.Pages.ApprovalOrder
{
    [Authorize]
    [RejectWebviewUser]
    [PermissionAccess("/approval-order")]
    public class PrintModel(IConfiguration config) : PageModel
    {
        public string Pid { get; private set; } = string.Empty;
        public string AppUrl { get; set; } = config["App:BaseUrl"] ?? string.Empty;
        public string ApiUrl { get; set; } = config["ApiUrls:BaseUrl"] ?? string.Empty;

        public string GetPrintOrderApproval { get; private set; } = 
            config["ApiUrls:Endpoints:PantryTransaksi:GetPrintOrderApproval"] ?? string.Empty;

        public void OnGet(string pid)
        {
            Pid = pid;
        }
    }
}