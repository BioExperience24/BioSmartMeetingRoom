using _3.BusinessLogic.Services.Implementation;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.PAMA.Razor.Views.Pages.Facility;

public class CreateModel : PageModel
{
    private readonly IFacilityService _service;
    public CreateModel(IFacilityService service)
    {
        _service = service;
    }
    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public FacilityViewModel Facility { get; set; } = default!;

    // For more information, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        //if (!ModelState.IsValid)
        //{
        //    return Page();
        //}

        await _service.Create(Facility);

        return RedirectToPage("./Index");
    }
}
