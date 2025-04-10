using _2.BusinessLogic.Services.Interface;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer.EnumType;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using _5.Helpers.Consumer.Policy;

namespace _1.PAMA.Razor.Views.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = AuthorizationWebviewPolicies.OnlyNonWebview)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployeeService _service;
        private readonly IAttachmentListService _attachmentListService;

        public EmployeeController(IEmployeeService service, IAttachmentListService attachmentListService, IConfiguration config) 
        {
            _service = service;

            attachmentListService.SetTableFolder(config["UploadFileSetting:tableFolder:employee"] ?? "employee");
            attachmentListService.SetExtensionAllowed(config["UploadFileSetting:imageExtensionAllowed"]!);
            attachmentListService.SetTypeAllowed(config["UploadFileSetting:imageContentTypeAllowed"]!);
            attachmentListService.SetSizeLimit(Convert.ToInt32(config["UploadFileSetting:imageSizeLimit"] ?? "8")); // MB
            _attachmentListService = attachmentListService;
        }

        [HttpGet]
        public async Task<ActionResult> GetItems()
        {
            ReturnalModel ret = new();

            ret.Message = "Get Success";
            ret.Collection = await _service.GetItemsAsync();

            return StatusCode(ret.StatusCode, ret);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> GetProfile()
        {
            ReturnalModel ret = new();

            ret.Collection = await _service.GetProfileAsync();

            return StatusCode(ret.StatusCode, ret);
        }

        [HttpGet]
        public async Task<ActionResult> GetItemsWithoutUser()
        {
            ReturnalModel ret = new();

            ret.Message = "Get Success";
            ret.Collection = await _service.GetItemsWithoutUserAsync();
        
            return StatusCode(ret.StatusCode, ret);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetItemById(string id)
        {
            ReturnalModel ret = new();

            ret.Message = "Get Success";
            ret.Collection = await _service.GetById(id);

            if (ret.Collection == null)
            {
                ret.Status = ReturnalType.Failed;
                ret.Message = "Get Failed";
            }

            return StatusCode(ret.StatusCode, ret);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        // public IActionResult CreateItem([FromForm] EmployeeVMCreateFR request)
        public async Task<IActionResult> Create([FromForm] EmployeeVMCreateFR request)
        {
            ReturnalModel ret = new();

            // Upload file photo
            if (request.FilePhoto != null && request.FilePhoto.Length > 0)
            {
                var (fileName, errMsg) = await _attachmentListService.FileUploadProcess(request.FilePhoto);
                if (errMsg != null)
                {
                    ret.Status = ReturnalType.Failed;
                    // ret.Message = "Failed to upload file";
                    ret.Message = errMsg;
                    return StatusCode(ret.StatusCode, ret);
                }
                request.Photo = fileName;
            }

            ret.Message = $"Success create a employee {request.Name}";
            ret.Collection = await _service.CreateAsync(request);

            if (ret.Collection == null)
            {
                ret.Status = ReturnalType.Failed;
                ret.Message = $"Failed create a employee {request.Name}";
            }

            
            return StatusCode(ret.StatusCode, ret);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Update(string id, [FromForm] EmployeeVMDefaultFR request)
        {
            ReturnalModel ret = new();

            ret.Message = $"Success update a employee {request.Name}";
            ret.Collection = await _service.UpdateAsync(id, request);

            if (ret.Collection == null)
            {
                ret.Status = ReturnalType.Failed;
                // ret.Message = $"Failed update a employee vip";
                ret.Message = $"Failed update a employee {request.Name}";
            }

            
            return StatusCode(ret.StatusCode, ret);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateVip(string id, [FromForm] EmployeeVMUpdateVipFR request)
        {
            ReturnalModel ret = new();

            ret.Message = $"Success update a employee vip";
            ret.Collection = await _service.UpdateVipAsync(id, request);

            if (ret.Collection == null)
            {
                ret.Status = ReturnalType.Failed;
                // ret.Message = $"Failed update a employee vip";
                ret.Message = $"Data employee not found";
            }

            
            return StatusCode(ret.StatusCode, ret);
        }

        [HttpPost]
        public async Task<IActionResult> Remove([FromForm] EmployeeVMDeleteFR request)
        {
            ReturnalModel ret = new();

            ret.Message = $"Success delete a employee {request.Name}";
            ret.Collection = await _service.DeleteAsync(request.Id);

            if (ret.Collection == null)
            {
                ret.Status = ReturnalType.Failed;
                // ret.Message = $"Failed update a employee vip";
                ret.Message = $"Failed delete a employee {request.Name}";
            }

            
            return StatusCode(ret.StatusCode, ret);
        }
    }
}