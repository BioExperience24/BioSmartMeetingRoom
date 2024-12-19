using _3.BusinessLogic.Services.Implementation;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.PAMA.Razor.Views.Pages.Setting.General;

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
