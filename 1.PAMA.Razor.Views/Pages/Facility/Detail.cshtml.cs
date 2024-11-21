using _3.BusinessLogic.Services.Implementation;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace _1.PAMA.Razor.Views.Pages.Facility;

public class DetailModel : PageModel
{
    private readonly IFacilityService _service;

    public DetailModel(IFacilityService service)
    {
        _service = service;
    }

    public FacilityViewModel Facility { get; set; } = default!;
    public async Task<IActionResult> OnGetAsync(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var facility = await _service.GetById(id);
        if (facility == null)
        {
            return NotFound();
        }
        else
        {
            Facility = facility;
        }
        return Page();
    }
}
