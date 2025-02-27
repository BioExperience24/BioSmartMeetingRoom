using _1.PAMA.Razor.Views.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.PAMA.Razor.Views.Pages.ReportUsage
{
    [Authorize]
    [PermissionAccess]
    public class IndexModel(IConfiguration config) : PageModel
    {
        public string AppUrl { get; set; } = config["App:BaseUrl"] ?? string.Empty;
        public string ApiUrl { get; set; } = config["ApiUrls:BaseUrl"] ?? string.Empty;
        public string GetEmployees { get; private set; } = 
            config["ApiUrls:Endpoints:GetEmployees"] ?? string.Empty;
        public string GetBuildings { get; private set; } = 
            config["ApiUrls:Endpoints:GetBuildings"] ?? string.Empty;
        public string GetRooms { get; private set; } = 
            config["ApiUrls:Endpoints:Room:GetRooms"] ?? string.Empty;
        public string GetAlocation { get; private set; } = 
            config["ApiUrls:Endpoints:GetAlocation"] ?? string.Empty;
        public string GetSettingInvoiceTexts { get; private set; } = 
            config["ApiUrls:Endpoints:SettingInvoiceText:GetSettingInvoiceTexts"] ?? string.Empty;
        public string GetRoomUsageDataTables { get; private set; } = 
            config["ApiUrls:Endpoints:Report:GetRoomUsages"] ?? string.Empty;
        public string GetOrganizerUsageReport { get; private set; } = 
            config["ApiUrls:Endpoints:Report:GetOrganizerUsages"] ?? string.Empty;
        public string GetAttendanceDataTables { get; private set; } = 
            config["ApiUrls:Endpoints:Report:GetAttendees"] ?? string.Empty;

        public void OnGet()
        {

        }
    }
}