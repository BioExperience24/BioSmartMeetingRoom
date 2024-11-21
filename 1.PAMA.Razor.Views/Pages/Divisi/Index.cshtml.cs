using _3.BusinessLogic.Services.Implementation;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.PAMA.Razor.Views.Pages.Divisi;

public class IndexModel : PageModel
{
    private readonly IDivisiService _service;
    public IndexModel(IDivisiService service)
    {
        _service = service;
    }

    public IList<DivisiViewModel> Divisi { get; set; } = default!;
    public async Task OnGetAsync()
    {
        Divisi = (await _service.GetAll()).ToList();
    }
}
