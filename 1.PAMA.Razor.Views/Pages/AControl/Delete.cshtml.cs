using _3.BusinessLogic.Services.Implementation;
using _4.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.PAMA.Razor.Views.Pages.AControl;

public class DeleteModel : PageModel
{
    private readonly IAccessControlService _service;

    public DeleteModel(IAccessControlService service)
    {
        _service = service;
    }

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

    public async Task<IActionResult> OnPostAsync(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return NotFound();
        }

        var accesscontrol = await _service.SoftDelete(id);

        return RedirectToPage("./Index");
    }
}
