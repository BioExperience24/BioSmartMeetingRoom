using _3.BusinessLogic.Services.Implementation;
using _4.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.PAMA.Razor.Views.Pages.AControl;

public class UpdateModel : PageModel
{
    private readonly IAccessControlService _service;

    public UpdateModel(IAccessControlService service)
    {
        _service = service;
    }

    [BindProperty]
    public AccessControlViewModel AccessControl { get; set; } = default!;
    public async Task<IActionResult> OnGetAsync(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var accesscontrol = await _service.GetById(id);
        if (accesscontrol == null)
        {
            return NotFound();
        }
        else
        {
            AccessControl = accesscontrol;
        }
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more information, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {

        await _service.Update(AccessControl);

        return RedirectToPage("./Index");
    }
}
