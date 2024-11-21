using _3.BusinessLogic.Services.Implementation;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace _1.PAMA.Razor.Views.Pages.Company;

public class DetailModel : PageModel
{
    private readonly ICompanyService _service;

    public DetailModel(ICompanyService service)
    {
        _service = service;
    }

    public CompanyViewModel Company { get; set; } = default!;
    public async Task<IActionResult> OnGetAsync(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var company = await _service.GetById(id);
        if (company == null)
        {
            return NotFound();
        }
        else
        {
            Company = company;
        }
        return Page();
    }
}
