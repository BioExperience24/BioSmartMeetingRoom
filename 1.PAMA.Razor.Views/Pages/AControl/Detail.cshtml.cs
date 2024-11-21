using _3.BusinessLogic.Services.Implementation;
using _4.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace _1.PAMA.Razor.Views.Pages.AControl;

public class DetailModel : PageModel
{
    private readonly IAccessControlService _service;

    public DetailModel(IAccessControlService service)
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
}
