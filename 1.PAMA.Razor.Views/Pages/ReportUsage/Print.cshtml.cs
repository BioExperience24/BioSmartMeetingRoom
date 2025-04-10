using System.Globalization;
using _1.PAMA.Razor.Views.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.PAMA.Razor.Views.Pages.ReportUsage
{
    [Authorize]
    [PermissionAccess("/report-usage")]
    public class PrintModel(IConfiguration config) : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public string Type { get; set; } = string.Empty;

        [BindProperty(Name = "start_date", SupportsGet = true)]
        public string StartDate { get; set; } = string.Empty;
        public string StartDateFormatted { get; set; } = string.Empty;

        [BindProperty(Name = "end_date", SupportsGet = true)]
        public string EndDate { get; set; } = string.Empty;
        public string EndDateFormatted { get; set; } = string.Empty;

        [BindProperty(Name = "nik", SupportsGet = true)]
        public string NIK { get; set; } = string.Empty;

        [BindProperty(Name = "nik_name", SupportsGet = true)]
        public string NIKName { get; set; } = string.Empty;

        [BindProperty(Name = "building_id", SupportsGet = true)]
        public string BuildingId { get; set; } = string.Empty;

        [BindProperty(Name = "building_name", SupportsGet = true)]
        public string BuildingName { get; set; } = string.Empty;

        [BindProperty(Name = "room_id", SupportsGet = true)]
        public string RoomId { get; set; } = string.Empty;

        [BindProperty(Name = "room_name", SupportsGet = true)]
        public string RoomName { get; set; } = string.Empty;

        [BindProperty(Name = "department_id", SupportsGet = true)]
        public string DepartmentId { get; set; } = string.Empty;

        [BindProperty(Name = "department_name", SupportsGet = true)]
        public string DepartmentName { get; set; } = string.Empty;

        public string AppUrl { get; set; } = config["App:BaseUrl"] ?? string.Empty;
        public string ApiUrl { get; set; } = config["ApiUrls:BaseUrl"] ?? string.Empty;
        public string GetOrganizerUsageReport { get; private set; } = 
            config["ApiUrls:Endpoints:Report:GetOrganizerUsages"] ?? string.Empty;

        public string GetAttendanceDataTables { get; private set; } = 
            config["ApiUrls:Endpoints:Report:GetAttendees"] ?? string.Empty;

        public string GetRoomUsageDataTables { get; private set; } = 
            config["ApiUrls:Endpoints:Report:GetRoomUsages"] ?? string.Empty;

        public void OnGet()
        {
            if (!string.IsNullOrEmpty(StartDate))
            {
                if (DateTime.TryParseExact(StartDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                {
                    // Format the DateTime object to the desired format
                    StartDateFormatted = parsedDate.ToString("MMM dd, yyyy", CultureInfo.InvariantCulture);
                }
            }

            if (!string.IsNullOrEmpty(EndDate))
            {
                if (DateTime.TryParseExact(EndDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                {
                    // Format the DateTime object to the desired format
                    EndDateFormatted = parsedDate.ToString("MMM dd, yyyy", CultureInfo.InvariantCulture);
                }
            }
        }
    }
}