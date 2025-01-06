using System.Text.Json;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer._Response;
using _7.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.PAMA.Razor.Views.Pages.BeaconFloor;

[Authorize]
public class IndexModel : PageModel
{
    private readonly IConfiguration _config;

    private readonly IBuildingService _buildingService;

    public IndexModel(IConfiguration config, IBuildingService buildingService)
    {
        _config = config;
        _buildingService = buildingService;
    }

    public string Buildings { get; set; } = default!;
    public string GetBeaconFloors { get; private set; } = string.Empty;
    public string GetBeaconFloorById { get; private set; } = string.Empty;
    public string CreateBeaconFloors { get; private set; } = string.Empty;
    public string UpdateBeaconFloors { get; private set; } = string.Empty;
    public string DeleteBeaconFloors { get; private set; } = string.Empty;

    public async Task OnGetAsync()
    // public void OnGet()
    {
        var buildings = await _buildingService.GetAll();
        Buildings = JsonSerializer.Serialize(buildings);

        // Gabungkan BaseUrl dan Endpoint
        var baseUrl = _config["ApiUrls:BaseUrl"];
        GetBeaconFloors = $"{baseUrl}{_config["ApiUrls:Endpoints:GetBeaconFloors"]}";
        GetBeaconFloorById = $"{baseUrl}{_config["ApiUrls:Endpoints:GetBeaconFloorById"]}";
        CreateBeaconFloors = $"{baseUrl}{_config["ApiUrls:Endpoints:CreateBeaconFloors"]}";
        UpdateBeaconFloors = $"{baseUrl}{_config["ApiUrls:Endpoints:UpdateBeaconFloors"]}";
        DeleteBeaconFloors = $"{baseUrl}{_config["ApiUrls:Endpoints:DeleteBeaconFloors"]}";
    }
}
