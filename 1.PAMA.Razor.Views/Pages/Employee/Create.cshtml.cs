using _3.BusinessLogic.Services.Implementation;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer.EnumType;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace _1.PAMA.Razor.Views.Pages.Employee;

public class CreateModel : PageModel
{
    private readonly IEmployeeService _service;
    private readonly ICompanyService _companyService;
    private readonly IDepartmentService _departmentService;
    private IDivisiService _divisiService;
    public CreateModel(IEmployeeService service, ICompanyService companyService
        ,IDepartmentService deptService, IDivisiService divService)
    {
        _service = service;
        _companyService = companyService;
        _departmentService = deptService;
        _divisiService = divService;
    }
    public async Task<IActionResult> OnGetAsync()
    {
        SelectList companies = new SelectList (await _companyService.GetAll(), "Id", "Name");
        //SelectList departments = new SelectList(await _departmentService.GetAll(), "Id", "DepartmentName");
        SelectList divisi = new SelectList(await _divisiService.GetAll(), "Id", "DivisiName");
        SelectList gender = new SelectList(new List<string>
            {
                Gender.Male,
                Gender.Female,
                Gender.Other
            });

        if (companies == null)
        {
            return NotFound();
        }
        else
        {
            Employee = new EmployeeViewModel()
            {
                IsVip = false, 
                VipApproveBypass = false,
                VipLimitCapBypass = false,
                VipLockRoom = false,
                Gender = Gender.Male
            };
            Companies = companies;
            Departements = new List<SelectListItem>();
            Divisi = divisi;
            Genders = gender;
        };
        return Page();
    }

    [BindProperty]
    public EmployeeViewModel Employee { get; set; } = default!;
    [BindProperty]
    public SelectList Companies { get; set; } = default!;
    [BindProperty]
    public List<SelectListItem> Departements { get; set; } = default!;
    [BindProperty]
    public SelectList Divisi { get; set; } = default!;
    [BindProperty]
    public SelectList Genders { get; set; } = default!;
    public bool IsVIP => false;


    // For more information, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        //if (!ModelState.IsValid)
        //{
        //    return Page();
        //}

        Genders = new SelectList(new List<string>
            {
                Gender.Male,
                Gender.Female,
                Gender.Other
            });
        await _service.Create(Employee);

        return RedirectToPage("./Index");
    }

    public async Task<JsonResult> OnGetDepartmentsAsync(string companyId)
    {
        var departments = (await _departmentService.GetAll()).ToList().Where(d => d.IdPerusahaan == companyId).ToList();
        
        return new JsonResult(departments);
    }
}
