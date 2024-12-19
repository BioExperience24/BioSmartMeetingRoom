using System.Text.Json;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer._Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _1.PAMA.Razor.Views.Pages.Alocation
{
    public class IndexModel : PageModel
    {

        private readonly HttpContext _httpContext;

        private readonly IConfiguration _config;

        // private readonly _Json _jsonResponse;

        // private readonly IAlocationService _alocationService;

        // private readonly IAlocationTypeService _alocationTypeService;

        // private readonly IEmployeeService _employeeService;

        public IndexModel(
            IHttpContextAccessor httpContextAccessor, 
            IConfiguration config,
            IAlocationService alocationService,
            IAlocationTypeService alocationTypeService,
            IEmployeeService employeeService)
        {
            _httpContext = httpContextAccessor.HttpContext ?? throw new("HttpContext is not available.");
            // _jsonResponse = new _Json(_httpContext);
            _config = config;
            
            // _alocationService = alocationService;
            // _alocationTypeService = alocationTypeService;
            // _employeeService = employeeService;
        }

        public string GetEmployees { get; private set; } = string.Empty;
        public string GetAlocation { get; private set; } = string.Empty;
        public string GetAllAlocationType { get; private set; } = string.Empty;
        public string CreateType { get; private set; } = string.Empty;
        public string UpdateType { get; private set; } = string.Empty;
        public string DeleteType { get; private set; } = string.Empty;
        public string CreateAlocation { get; private set; } = string.Empty;
        public string UpdateAlocation { get; private set; } = string.Empty;
        public string DeleteAlocation { get; private set; } = string.Empty;
        public void OnGet()
        {
            // Gabungkan BaseUrl dan Endpoint
            var baseUrl = _config["ApiUrls:BaseUrl"];
            GetEmployees = $"{baseUrl}{_config["ApiUrls:Endpoints:GetEmployees"]}";
            GetAlocation = $"{baseUrl}{_config["ApiUrls:Endpoints:GetAlocation"]}";
            GetAllAlocationType = $"{baseUrl}{_config["ApiUrls:Endpoints:GetAllAlocationType"]}";
            CreateType = $"{baseUrl}{_config["ApiUrls:Endpoints:CreateType"]}";
            UpdateType = $"{baseUrl}{_config["ApiUrls:Endpoints:UpdateType"]}";
            DeleteType = $"{baseUrl}{_config["ApiUrls:Endpoints:DeleteType"]}";
            CreateAlocation = $"{baseUrl}{_config["ApiUrls:Endpoints:CreateAlocation"]}";
            UpdateAlocation = $"{baseUrl}{_config["ApiUrls:Endpoints:UpdateAlocation"]}";
            DeleteAlocation = $"{baseUrl}{_config["ApiUrls:Endpoints:DeleteAlocation"]}";
        }

        /* public async Task<IActionResult> OnGetTypes()
        {

            var response = await _alocationTypeService.GetAllItemAsync();
            
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

        /* [BindProperty]
        public AlocationTypeVMReq CTypeReq { set; get; } = new();
        
        public async Task<IActionResult> OnPostCreateType()
        {
            var type = await _alocationTypeService.CreateItemAsync(CTypeReq);
            
            string status = "success";
            string message = $"Success create a alocation type {CTypeReq.Name}";
            
            if (type == null)
            {
                status = "fail";
                message = $"Failed create a alocation type {CTypeReq.Name}";
            }

            return _jsonResponse
                        .SetStatus(status)
                        .SetMessage(message)
                        .Generate();
        } */

        /* [BindProperty]
        public AlocationTypeVMUpdateReq UTypeReq { get; set; } = new();
        
        public async Task<IActionResult> OnPostUpdateType()
        {
            var type = await _alocationTypeService.UpdateItemAsync(UTypeReq); 

            string status = "success";
            string message = $"Success update a alocation type {UTypeReq.Name}";
            
            if (type == null)
            {
                status = "fail";
                message = $"Failed update a alocation type {UTypeReq.Name}";
            }

            return _jsonResponse
                        .SetStatus(status)
                        .SetMessage(message)
                        .Generate();
        } */

        /* [BindProperty]
        public AlocationTypeVMReq DTypeReq { get; set; } = new();

        public async Task<IActionResult> OnPostDeleteType()
        {
            var type = await _alocationTypeService.DeleteItemAsync(DTypeReq); 

            string status = "success";
            string message = $"Success delete a alocation type {DTypeReq.Name}";
            if (type == null)
            {
                status = "fail";
                message = $"Failed delete a alocation type {DTypeReq.Name}";
            }

            return _jsonResponse
                        .SetStatus(status)
                        .SetMessage(message)
                        .Generate();
        } */

        /* public async Task<IActionResult> OnGetAlocations()
        {
            var response = await _alocationService.GetAllItemAsync();
            
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


        /* [BindProperty]
        public AlocationVMCreateReq CAlocationReq { get; set; } = new();

        public async Task<IActionResult> OnPostCreateAlocation()
        {
            var alocation = await _alocationService.CreateItemAsync(CAlocationReq);

            string status = "success";
            string message = $"Success create a department {CAlocationReq.Name}";
            if (alocation == null)
            {
                status = "fail";
                message = $"Failed create a department {CAlocationReq.Name}";
            }

            return _jsonResponse
                        .SetStatus(status)
                        .SetMessage(message)
                        .Generate();
        } */

        /* [BindProperty]
        public AlocationVMUpdateReq UAlocationReq { get; set; } = new();

        public async Task<IActionResult> OnPostUpdateAlocation()
        {
            var alocation = await _alocationService.UpdateItemAsync(UAlocationReq); 

            string status = "success";
            string message = $"Success update a department {UAlocationReq.Name}";
            if (alocation == null)
            {
                status = "fail";
                message = $"Failed update a department {UAlocationReq.Name}";
            }

            return _jsonResponse
                        .SetStatus(status)
                        .SetMessage(message)
                        .Generate();
        } */

        /* [BindProperty]
        public AlocationVMReq DAlocationReq { get; set; } = new();

        public async Task<IActionResult> OnPostDeleteAlocation()
        {
            var alocation = await _alocationService.DeleteItemAsync(DAlocationReq); 

            string status = "success";
            string message = $"Success delete a department {DAlocationReq.Name}";
            if (alocation == null)
            {
                status = "fail";
                message = $"Failed delete a department {DAlocationReq.Name}";
            }

            return _jsonResponse
                        .SetStatus(status)
                        .SetMessage(message)
                        .Generate();
        } */

        /* public async Task<IActionResult> OnPostEmployees()
        {
            var response = await _employeeService.GetAllItemAsync();
            
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
}
