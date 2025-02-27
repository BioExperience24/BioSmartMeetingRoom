using System.Text.Json;
using _3.BusinessLogic.Services.Interface;
using _5.Helpers.Consumer._Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.PAMA.Razor.Views.Pages.Booking
{
    [Authorize]
    public class IndexModel(
        IConfiguration config
        // IModuleBackendService moduleBackendService,
        // ISettingRuleBookingService settingRuleBookingService,
        // ISettingInvoiceTextService settingInvoiceTextService,
        // IRoomForUsageService roomForUsageService,
        // IBuildingService buildingService,
        // IRoomService roomService,
        // IFacilityService facilityService
    ) : PageModel
    {
        // private readonly IModuleBackendService _moduleBackendService = moduleBackendService;
        // private readonly ISettingRuleBookingService _settingRuleBookingService = settingRuleBookingService;
        // private readonly ISettingInvoiceTextService _settingInvoiceTextService = settingInvoiceTextService;
        // private readonly IRoomForUsageService _roomForUsageService = roomForUsageService;
        // private readonly IBuildingService _buildingService = buildingService;
        // private readonly IRoomService _roomService = roomService;
        // private readonly IFacilityService _facilityService = facilityService;

        public string AppUrl { get; set; } = config["App:BaseUrl"] ?? string.Empty;
        public string ApiUrl { get; set; } = config["ApiUrls:BaseUrl"] ?? string.Empty;

        public string GetEmployees { get; private set; } = 
            config["ApiUrls:Endpoints:GetEmployees"] ?? string.Empty;
        public string GetBuildings { get; private set; } = 
            config["ApiUrls:Endpoints:GetBuildings"] ?? string.Empty;
        public string GetRooms { get; private set; } = 
            config["ApiUrls:Endpoints:Room:GetRooms"] ?? string.Empty;
        public string GetFacilities { get; private set; } = 
            config["ApiUrls:Endpoints:GetFacilities"] ?? string.Empty;
        public string GetAvailableRooms { get; private set; } = 
            config["ApiUrls:Endpoints:Room:GetAvailableRooms"] ?? string.Empty;
        public string GetAlocation { get; private set; } = 
            config["ApiUrls:Endpoints:GetAlocation"] ?? string.Empty;
        public string PostCreateReserve { get; private set; } = 
            config["ApiUrls:Endpoints:Booking:PostCreateReserve"] ?? string.Empty;
        public string PantryPackages { get; private set; } = 
            config["ApiUrls:Endpoints:PantryPackage:GetAll"] ?? string.Empty;
        public string PantryPackageDetailById { get; private set; } = 
            config["ApiUrls:Endpoints:PantryPackage:GetById"] ?? string.Empty;
        public string GetBookingDataTables { get; private set; } = 
            config["ApiUrls:Endpoints:Booking:GetDataTables"] ?? string.Empty;

        // public string Modules { get; set; } = "{}";
        // public string SettingGeneral { get; set; } = "{}";
        // public string Invoices { get; set; } = "[]";
        // public string Categories { get; set; } = "[]";
        // public string Buildings { get; set; } = "[]";
        // public string Rooms { get; set; } = "[]";
        // public string Employees { get; set; } = "[]";
        // public string Facilities { get; set; } = "[]";

        // public async Task OnGetAsync()
        public void OnGet()
        {
            /* var modules = await _moduleBackendService.GetItemsByModuleTextAsync(new string[]{
                "module_pantry",
                "module_automation",
                "module_price",
                "module_int_365",
                "module_int_google",
                "module_room_advance",
                "module_user_vip"
            });

            var dictModules = new Dictionary<string, object>();

            foreach (var item in modules)
            {
                dictModules.Add(_Dictionary.ModuleAliasDictionary[item.ModuleText], item);
            }

            Modules = JsonSerializer.Serialize(dictModules); */
            

            /* var settingGeneral = await _settingRuleBookingService.GetSettingRuleBookingTopOne();
            if (settingGeneral != null)
            {
                SettingGeneral = JsonSerializer.Serialize(settingGeneral);
            } */

            /* var invoices = await _settingInvoiceTextService.GetAll();
            if (invoices.Any())
            {
                Invoices = JsonSerializer.Serialize(invoices);
            } */

            /* var categories = await _roomForUsageService.GetAll();
            if (categories.Any())
            {
                Categories = JsonSerializer.Serialize(categories);
            } */

            /* var buildings = await _buildingService.GetAllItemAsync();
            if (buildings.Any())
            {
                Buildings = JsonSerializer.Serialize(buildings);
            } */

            /* var rooms = await _roomService.GetAllRoomItemAsync(false);
            if (rooms.Any())
            {
                Rooms = JsonSerializer.Serialize(rooms);
                Employees = JsonSerializer.Serialize(rooms);
            } */

            /* var facilities = await  _facilityService.GetAll();
            if (facilities.Any())
            {
                Facilities = JsonSerializer.Serialize(facilities);
            } */

           // // TODO: Kondisi jika session levelid-nya == 1
            // On progress

            // NOTE: Buat ketika data auth (session) sudah ada 
            // // TODO: Kondisi jika session levelid-nya == 2
            // On progress
            
            // NOTE: Buat ketika data auth (session) sudah ada 
            // // TODO: Kondisi jika session levelid-nya != 1 & 2
            // Error return
        }
    }
}