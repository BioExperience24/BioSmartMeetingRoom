using _3.BusinessLogic.Services.Implementation;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.PAMA.Razor.Views.Pages.Company;

public class IndexModel : PageModel
{
    private readonly ICompanyService _service;
    public IndexModel(ICompanyService service)
    {
        _service = service;
    }

    public IList<CompanyViewModel> Company { get; set; } = default!;
    public async Task OnGetAsync()
    {
        Company = (await _service.GetAll()).ToList();
    }
}
