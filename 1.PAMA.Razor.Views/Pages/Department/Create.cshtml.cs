using _3.BusinessLogic.Services.Implementation;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.PAMA.Razor.Views.Pages.Department;

public class CreateModel : PageModel
{
    private readonly IDepartmentService _service;
    public CreateModel(IDepartmentService service)
    {
        _service = service;
    }
    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public DepartmentViewModel Department { get; set; } = default!;

    // For more information, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        //if (!ModelState.IsValid)
        //{
        //    return Page();
        //}

        await _service.Create(Department);

        return RedirectToPage("./Index");
    }
}
