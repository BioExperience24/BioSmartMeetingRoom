using _3.BusinessLogic.Services.Implementation;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace _1.PAMA.Razor.Views.Pages.Department;

public class DetailModel : PageModel
{
    private readonly IDepartmentService _service;

    public DetailModel(IDepartmentService service)
    {
        _service = service;
    }

    public DepartmentViewModel Department { get; set; } = default!;
    public async Task<IActionResult> OnGetAsync(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var department = await _service.GetById(id);
        if (department == null)
        {
            return NotFound();
        }
        else
        {
            Department = department;
        }
        return Page();
    }
}
