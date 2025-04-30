using System.Text.Json;
using _1.PAMA.Razor.Views.Attributes;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer._Response;
using _7.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.PAMA.Razor.Views.Pages.Employee;

[Authorize]
[PermissionAccess]
public class IndexModel : PageModel
{
    private readonly HttpContext _httpContext;

    private readonly IConfiguration _config;

    private readonly _Json _jsonResponse;

    private readonly IEmployeeService _service;

    private readonly IAlocationService _alocationService;
    private readonly IAlocationTypeService _alocationTypeService;

    public IndexModel(
        IHttpContextAccessor httpContextAccessor,
        IConfiguration config,
        IEmployeeService service,
        IAlocationService alocationSvc,
        IAlocationTypeService alocationTypeService)
    {
        _httpContext = httpContextAccessor.HttpContext ?? throw new("HttpContext is not available.");

        _config = config;

        _jsonResponse = new _Json(_httpContext);

        _service = service;

        _alocationService = alocationSvc;

        _alocationTypeService = alocationTypeService;
    }

    public string AlocationTypes { get; set; } = default!;

    public string GetEmployees { get; private set; } = string.Empty;

    public string GetEmployeeById { get; private set; } = string.Empty;
    public string CreateEmployee { get; private set; } = string.Empty;

    public string UpdateEmployee { get; set; } = string.Empty;

    public string UpdateEmployeeVip { get; set; } = string.Empty;

    public string DeleteEmployee { get; set; } = string.Empty;

    public string GetAlocationByTypeId { get; private set; } = string.Empty;
    
    public string ImportEmployee { get; set; } = string.Empty;

    public async Task OnGetAsync()
    {
        var alocationTypes = await _alocationTypeService.GetAll();
        AlocationTypes = JsonSerializer.Serialize(alocationTypes);

        // Gabungkan BaseUrl dan Endpoint
        var baseUrl = _config["ApiUrls:BaseUrl"];
        GetEmployees = $"{baseUrl}{_config["ApiUrls:Endpoints:GetEmployees"]}";
        GetEmployeeById = $"{baseUrl}{_config["ApiUrls:Endpoints:GetEmployeeById"]}";
        CreateEmployee = $"{baseUrl}{_config["ApiUrls:Endpoints:CreateEmployee"]}";
        UpdateEmployee = $"{baseUrl}{_config["ApiUrls:Endpoints:UpdateEmployee"]}";
        UpdateEmployeeVip = $"{baseUrl}{_config["ApiUrls:Endpoints:UpdateEmployeeVip"]}";
        DeleteEmployee = $"{baseUrl}{_config["ApiUrls:Endpoints:DeleteEmployee"]}";
        GetAlocationByTypeId = $"{baseUrl}{_config["ApiUrls:Endpoints:GetAlocationByTypeId"]}";
        ImportEmployee = $"{baseUrl}{_config["ApiUrls:Endpoints:ImportEmployee"]}";
    }

    /* public async Task<IActionResult> OnGetEmployees()
    {
        var response = await _service.GetAllItemAsync();
            
        string status = "success";
        string message = "Get Success";
        
        // if (response.Error != null)
        // {
        //     status = "fail";
        //     message = "Get Failed";
        // }

        return _jsonResponse
                    .Set(status, message, response)
                    .Generate();
    } */

    /* public async Task<IActionResult> OnGetEmployeeById(string id)
    {
        var employee = await _service.GetItemByIdAsync(id);

        string status = "success";
        string message = "Get Success";

        if (employee == null)
        {
            status = "fail";
            message = "Get Failed";
        }

        return _jsonResponse
                    .Set(status, message, employee)
                    .Generate();
    } */

    /* public async Task<IActionResult> OnGetDepartmentById(string id)
    {
        var response = await _alocationService.GetAllItemByTypeAsycn(id);
            
        string status = "success";
        string message = "Get Success";
        
        // if (response.Error != null)
        // {
        //     status = "fail";
        //     message = "Get Failed";
        // }

        return _jsonResponse
                    .Set(status, message, response)
                    .Generate();
    } */
}
