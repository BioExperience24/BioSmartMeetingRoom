using _3.BusinessLogic.Services.Implementation;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace _1.PAMA.Razor.Views.Pages.Employee;

public class DetailModel : PageModel
{
    private readonly IEmployeeService _service;

    public DetailModel(IEmployeeService service)
    {
        _service = service;
    }

    public EmployeeViewModel Employee { get; set; } = default!;
    public async Task<IActionResult> OnGetAsync(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var employee = await _service.GetById(id);
        if (employee == null)
        {
            return NotFound();
        }
        else
        {
            Employee = employee;
        }
        return Page();
    }
}
