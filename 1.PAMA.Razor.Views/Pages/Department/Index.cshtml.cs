using _3.BusinessLogic.Services.Implementation;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.PAMA.Razor.Views.Pages.Department;

public class IndexModel : PageModel
{
    private readonly IDepartmentService _service;
    public IndexModel(IDepartmentService service)
    {
        _service = service;
    }

    public IList<DepartmentViewModel> Department { get; set; } = default!;
    public async Task OnGetAsync()
    {
        Department = (await _service.GetAll()).ToList();
    }
}
