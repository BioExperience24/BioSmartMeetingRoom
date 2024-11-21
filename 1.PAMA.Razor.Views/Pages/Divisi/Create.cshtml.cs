using _3.BusinessLogic.Services.Implementation;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.PAMA.Razor.Views.Pages.Divisi;

public class CreateModel : PageModel
{
    private readonly IDivisiService _service;
    public CreateModel(IDivisiService service)
    {
        _service = service;
    }
    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public DivisiViewModel Divisi { get; set; } = default!;

    // For more information, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        //if (!ModelState.IsValid)
        //{
        //    return Page();
        //}

        await _service.Create(Divisi);

        return RedirectToPage("./Index");
    }
}
