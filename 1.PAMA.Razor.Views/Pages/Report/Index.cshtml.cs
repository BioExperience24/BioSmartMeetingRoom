using System.Text.Json;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer._Response;
using _7.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.PAMA.Razor.Views.Pages.Report;

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

}
