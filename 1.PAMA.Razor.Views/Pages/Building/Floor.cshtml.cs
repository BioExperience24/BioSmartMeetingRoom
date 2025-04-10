using System.Text.Json;
using _1.PAMA.Razor.Views.Attributes;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer._Encryption;
using _5.Helpers.Consumer._Encryption._Secure;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.PAMA.Razor.Views.Pages.Building
{
    [Authorize]
    [PermissionAccess("/building")]
    public class FloorModel(IConfiguration config, IBuildingService service, ISecureService secureService) : PageModel
    {
        private readonly IConfiguration _config = config;
        private readonly IBuildingService _service = service;
        private readonly ISecureService _secureService = secureService;

        private readonly _Aes _aes = new _Aes(config["EncryptSetting:AesKeyEncryptor"] ?? "SUp3RsEcr3tKeY!!");

        [BindProperty(Name = "building", SupportsGet = true)]
        public string BuildingEncId { get; set; } = string.Empty;

        public long BuildingId { get; set; } = default!;
        public string? Building;
        public IEnumerable<BuildingViewModel> BuildingList = default!;

        public string? AppUrl { get; set; } = config["App:BaseUrl"];
        public string? ApiUrl { get; set; } = config["ApiUrls:BaseUrl"];
        public string? GetBuildingFloors { get; set; } = config["ApiUrls:Endpoints:GetBuildingFloors"];
        public string? CreateBuildingFloor { get; set; } = config["ApiUrls:Endpoints:CreateBuildingFloor"];
        public string? GetShowBuildingFloor { get; set; } = config["ApiUrls:Endpoints:GetShowBuildingFloor"];
        public string? UpdateBuildingFloor { get; set; } = config["ApiUrls:Endpoints:UpdateBuildingFloor"];
        public string? DeleteBuildingFloor { get; set; } = config["ApiUrls:Endpoints:DeleteBuildingFloor"];
        public string? UploadBuildingFloor { get; set; } = config["ApiUrls:Endpoints:UploadBuildingFloor"];

        public async Task<IActionResult?> OnGetAsync()
        {
            BuildingEncId = BuildingEncId.Replace(" ", "+");
            BuildingId = Convert.ToInt64(_secureService.Decrypt(BuildingEncId));
            // BuildingId = Convert.ToInt64(_aes.Decrypt(BuildingEncId));

            var building = await _service.GetItemByIdAsync(BuildingId);
            if (building == null)
            {
                return Redirect("/building");
            }

            Building = building != null ? JsonSerializer.Serialize(building) : null;

            BuildingList = await _service.GetAllItemAsync(BuildingId);

            return Page();
        }
    }
}