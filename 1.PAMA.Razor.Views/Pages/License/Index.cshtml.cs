using _1.PAMA.Razor.Views.Attributes;
using _3.BusinessLogic.Services.Implementation;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.PAMA.Razor.Views.Pages.License;

[Authorize]
[PermissionAccess]
public class IndexModel : PageModel
{
    private readonly IFacilityService _service;
    public IndexModel(IFacilityService service)
    {
        _service = service;
    }

    public IList<FacilityViewModel> Facility { get; set; } = default!;
    public async Task OnGetAsync()
    {
        Facility = (await _service.GetAll()).ToList();
    }
}
