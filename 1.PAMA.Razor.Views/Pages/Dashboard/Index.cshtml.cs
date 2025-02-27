using _1.PAMA.Razor.Views.Attributes;
using _3.BusinessLogic.Services.Implementation;
using _3.BusinessLogic.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.PAMA.Razor.Views.Pages.Dashboard
{
    [Authorize]
    [PermissionAccess]
    public class IndexModel(
        IConfiguration config,
        IRoomService roomService,
        IEmployeeService employeeService,
        IBookingService bookingService
    ) : PageModel
    {
        private readonly IRoomService _roomService = roomService;
        private readonly IEmployeeService _employeeService = employeeService;
        private readonly IBookingService _bookingService = bookingService;

        public string AppUrl { get; set; } = config["App:BaseUrl"] ?? string.Empty;
        public string ApiUrl { get; set; } = config["ApiUrls:BaseUrl"] ?? string.Empty;

        public string GetListChart { get; set; } = config["ApiUrls:Endpoints:Booking:GetListChart"] ?? string.Empty;
        public string GetOngoingBookings { get; set; } = config["ApiUrls:Endpoints:Booking:GetOngoingBookings"] ?? string.Empty;
        public string GetChartTopRoom { get; set; } = config["ApiUrls:Endpoints:Room:GetChartTopRoom"] ?? string.Empty;
        
        public string CurrentDate { get; set; } = DateTime.Now.ToString("dd MMM yyyy");

        public int CountRoom { get; set; } = 0;
        public int CountEmployee { get; set; } = 0;
        public int CountTransaction { get; set; } = 0;

        public async Task OnGetAsync()
        {
            CountRoom = await _roomService.GetCountRoomItemAsync(false);

            CountEmployee = await _employeeService.GetCountAsync();

            CountTransaction = await _bookingService.GetCountAsync();
        }
    }
}