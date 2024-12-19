using System.Text.Json;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.PAMA.Razor.Views.Pages.Display
{
    public class IndexModel(
        IConfiguration config,
        IModuleBackendService moduleBackendService
    ) 
        : PageModel
    {
        private readonly IModuleBackendService _moduleBackendService = moduleBackendService;

        public string AppUrl { get; set; } = config["App:BaseUrl"] ?? string.Empty;
        public string ApiUrl { get; set; } = config["ApiUrls:BaseUrl"] ?? string.Empty;

        public string GetRoomRoomDisplays { get; set; } = config["ApiUrls:Endpoints:Access:GetRoomRoomDisplays"] ?? string.Empty;
        public string GetRoomDisplays { get; set; } = config["ApiUrls:Endpoints:RoomDisplay:GetRoomDisplays"] ?? string.Empty;
        public string PostCreate { get; set; } = config["ApiUrls:Endpoints:RoomDisplay:PostCreate"] ?? string.Empty;
        public string PostUpdate { get; set; } = config["ApiUrls:Endpoints:RoomDisplay:PostUpdate"] ?? string.Empty;
        public string PostDelete { get; set; } = config["ApiUrls:Endpoints:RoomDisplay:PostDelete"] ?? string.Empty;
        public string PostChangeStatusDisplay { get; set; } = config["ApiUrls:Endpoints:RoomDisplay:PostChangeStatusDisplay"] ?? string.Empty;

        public string ModuleDisplay { get; set; } = "{}";
        public string CurrentDate { get; set; } = DateTime.Now.ToString("dd MMM yyyy");

        public async Task OnGetAsync()
        {
            ModuleBackendViewModel vm = new ModuleBackendViewModel {
                ModuleText = "module_display"
            };
            var moduleDisplay = await _moduleBackendService.GetItemAsync(vm);
            if (moduleDisplay != null)
            {
                var dictModuleDisplay = new Dictionary<string, object>{
                    {"display", moduleDisplay}
                };
                ModuleDisplay = JsonSerializer.Serialize(dictModuleDisplay);
            }
        }
    }
}
