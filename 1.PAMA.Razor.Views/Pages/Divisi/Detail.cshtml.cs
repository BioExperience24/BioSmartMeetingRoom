using _3.BusinessLogic.Services.Implementation;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace _1.PAMA.Razor.Views.Pages.Divisi;

[Authorize]
public class DetailModel : PageModel
{
    private readonly IDivisiService _service;

    public DetailModel(IDivisiService service)
    {
        _service = service;
    }

    public DivisiViewModel Divisi { get; set; } = default!;
    public async Task<IActionResult> OnGetAsync(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var divisi = await _service.GetById(id);
        if (divisi == null)
        {
            return NotFound();
        }
        else
        {
            Divisi = divisi;
        }
        return Page();
    }
}
