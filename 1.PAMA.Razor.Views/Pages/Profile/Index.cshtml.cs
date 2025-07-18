using _3.BusinessLogic.Services.Implementation;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.PAMA.Razor.Views.Pages.Profile;

[Authorize]
public class IndexModel : PageModel
{
    private readonly IFacilityService _service;
    private readonly IConfiguration _config;
    public string? BaseUrl { get; private set; }
    public string? BaseWeb { get; private set; }
    public string? GetProfile { get; private set; }
    public string? GetAuthUser { get; private set; }
    public string? GetLevels { get; private set; }
    public string? UpdateEmployee { get; private set; }

    public string? UpdateUsername { get; private set; }
    public string? UpdatePassword { get; private set; }

    public IndexModel(IFacilityService service, IConfiguration config)
    {
        _service = service;
        _config = config;
        BaseUrl = _config["ApiUrls:BaseUrl"];
        BaseWeb = _config["App:BaseUrl"];
        GetProfile = config["ApiUrls:Endpoints:GetProfile"];
        GetAuthUser = config["ApiUrls:Endpoints:GetAuthUser"];
        GetLevels = config["ApiUrls:Endpoints:GetLevels"];
        UpdateEmployee = config["ApiUrls:Endpoints:UpdateProfile"];
        UpdateUsername = config["ApiUrls:Endpoints:UpdateUsername"];
        UpdatePassword = config["ApiUrls:Endpoints:UpdatePassword"];
    }

    public IList<FacilityViewModel> Facility { get; set; } = default!;
    public async Task OnGetAsync()
    {
        Facility = (await _service.GetAll()).ToList();
    }
}
