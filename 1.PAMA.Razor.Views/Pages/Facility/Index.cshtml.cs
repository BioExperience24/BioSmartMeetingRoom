using _3.BusinessLogic.Services.Implementation;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.PAMA.Razor.Views.Pages.Facility;

[Authorize]
public class IndexModel : PageModel
{
    private readonly HttpContext _httpContext;

    private readonly IConfiguration _config;

    private readonly IFacilityService _service;

    public IndexModel(
        IHttpContextAccessor httpContextAccessor,
        IConfiguration config,
        IFacilityService service)
    {
        _httpContext = httpContextAccessor.HttpContext ?? throw new("HttpContext is not available.");
        _config = config;
        _service = service;
    }
    
    public string BaseUrl { get; private set; } = string.Empty;

    public string GetFacilities { get; set; } = string.Empty;


    public void OnGet()
    {
        var baseUrl = _config["ApiUrls:BaseUrl"];
        BaseUrl = $"{baseUrl}";
        GetFacilities = $"{baseUrl}{_config["ApiUrls:Endpoints:GetFacilities"]}";
    }

    /* public IList<FacilityViewModel> Facility { get; set; } = default!;
    public async Task OnGetAsync()
    {
        Facility = (await _service.GetAll()).ToList();
    } */


    //public async Task<IActionResult> OnPostAsync()
    //{
    //    //if (!ModelState.IsValid)
    //    //{
    //    //    return Page();
    //    //}

    //    await _service.Create(Facility);

    //    return RedirectToPage("./Index");
    //}
}
