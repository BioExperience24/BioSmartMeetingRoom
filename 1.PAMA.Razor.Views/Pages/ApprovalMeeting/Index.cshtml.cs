using _1.PAMA.Razor.Views.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.PAMA.Razor.Views.Pages.ApprovalMeeting
{
    [Authorize]
    [RejectWebviewUser]
    public class IndexModel(IConfiguration config) : PageModel
    {
        public string AppUrl { get; set; } = config["App:BaseUrl"] ?? string.Empty;
        public string ApiUrl { get; set; } = config["ApiUrls:BaseUrl"] ?? string.Empty;
        public string GetRooms { get; private set; } = 
            config["ApiUrls:Endpoints:Room:GetRooms"] ?? string.Empty;
        public string GetBookingWithApprovalDataTables { get; private set; } = 
            config["ApiUrls:Endpoints:Booking:GetWithApprovalDataTables"] ?? string.Empty;
        public string ProcessMeetingApproval { get; private set; } = 
            config["ApiUrls:Endpoints:Booking:ProcessMeetingApproval"] ?? string.Empty;

        public void OnGet()
        {
        }
    }
}