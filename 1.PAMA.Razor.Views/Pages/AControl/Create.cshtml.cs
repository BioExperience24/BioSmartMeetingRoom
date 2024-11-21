using _3.BusinessLogic.Services.Implementation;
using _4.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.PAMA.Razor.Views.Pages.AControl;

public class CreateModel : PageModel
{
    private readonly IAccessControlService _service;
    public CreateModel(IAccessControlService service)
    {
        _service = service;
    }
    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public AccessControlViewModel AccessControl { get; set; } = default!;

    // For more information, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        //if (!ModelState.IsValid)
        //{
        //    return Page();
        //}

        await _service.Create(AccessControl);

        return RedirectToPage("./Index");
    }
}
