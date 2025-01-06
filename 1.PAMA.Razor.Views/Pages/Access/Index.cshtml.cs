using System.Text.Json;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.PAMA.Razor.Views.Pages.Access
{
    [Authorize]
    public class IndexModel(
        IConfiguration config,
        IModuleBackendService moduleBackendService,
        IAccessControllerTypeService accessControllerTypeService)
        : PageModel
    {
        private readonly IModuleBackendService _moduleBackendService = moduleBackendService;
        private readonly IAccessControllerTypeService _accessControllerTypeService = accessControllerTypeService;

        public string AppUrl { get; set; } = config["App:BaseUrl"] ?? string.Empty;
        public string ApiUrl { get; set; } = config["ApiUrls:BaseUrl"] ?? string.Empty;

        public string GetAccessControls { get; set; } = config["ApiUrls:Endpoints:Access:GetAccessControls"] ?? string.Empty;
        public string GetAccessControlById { get; set; } = config["ApiUrls:Endpoints:Access:GetAccessControlById"] ?? string.Empty;
        public string GetRooms { get; set; } = config["ApiUrls:Endpoints:Access:GetRooms"] ?? string.Empty;
        public string PostCreate { get; set; } = config["ApiUrls:Endpoints:Access:PostCreate"] ?? string.Empty;
        public string PostUpdate { get; set; } = config["ApiUrls:Endpoints:Access:PostUpdate"] ?? string.Empty;
        public string PostDelete { get; set; } = config["ApiUrls:Endpoints:Access:PostDelete"] ?? string.Empty;
        public string GetAccessChannels { get; set; } = config["ApiUrls:Endpoints:AccessChannel:GetAccessChannels"] ?? string.Empty;
        public string GetAccessIntegrateds { get; set; } = config["ApiUrls:Endpoints:AccessIntegrated:GetAccessIntegrateds"] ?? string.Empty;
        public string PostAssign { get; set; } = config["ApiUrls:Endpoints:AccessIntegrated:PostAssign"] ?? string.Empty;

        public string ModuleAccessDoor { get; set; } = "{}";
        public string ControllerTypes { get; set; } = "[]";

        public async Task OnGetAsync()
        {
            ModuleBackendViewModel vm = new ModuleBackendViewModel
            {
                ModuleText = "module_access_door"
            };
            var moduleAccessDoor = await _moduleBackendService.GetItemAsync(vm);
            if (moduleAccessDoor != null)
            {
                var dictModuleAccessDoor = new Dictionary<string, object>{
                    {"access_door", moduleAccessDoor}
                };
                ModuleAccessDoor = JsonSerializer.Serialize(dictModuleAccessDoor);
            }

            var controllerTypes = await _accessControllerTypeService.GetAll();
            ControllerTypes = JsonSerializer.Serialize(controllerTypes);
        }
    }
}
