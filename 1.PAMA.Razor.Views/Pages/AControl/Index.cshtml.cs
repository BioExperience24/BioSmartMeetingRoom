using _3.BusinessLogic.Services.Implementation;
using _4.Data.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.PAMA.Razor.Views.Pages.AControl;

public class IndexModel : PageModel
{
    private readonly IAccessControlService _service;
    public IndexModel(IAccessControlService service)
    {
        _service = service;
    }

    public IList<AccessControlViewModel> AccessControl { get; set; } = default!;
    public async Task OnGetAsync()
    {
        AccessControl = (await _service.GetAll()).ToList();
    }
}
