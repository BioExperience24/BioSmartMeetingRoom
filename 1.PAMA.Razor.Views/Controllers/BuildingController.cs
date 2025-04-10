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
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BuildingController : ControllerBase
    {
        private readonly IBuildingService _service;

        public BuildingController(IBuildingService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var response = await _service.GetAllItemAsync();
            ReturnalModel ret = new()
            {
                Collection = response
            };
            return StatusCode(ret.StatusCode, ret);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(long id)
        {
            ReturnalModel ret = new() {};
            
            var response = await _service.GetItemByIdAsync(id);

            if (response == null)
            {
                ret.Status = ReturnalType.Failed;
                ret.Message = "Get failed";
            }

            ret.Collection = response;

            return StatusCode(ret.StatusCode, ret);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] BuildingVMDefaultFR request)
        {   
            ReturnalModel ret = new() {};

            var (fileName, errMsg) = await _service.DoUploadAsync(request.FileImage);

            if (errMsg != null)
            {
                ret.Status = ReturnalType.Failed;
                // ret.Message = "Gagal Upload";
                ret.Message = errMsg;
                return StatusCode(ret.StatusCode, ret);
            }

            if (fileName != null)
            {
                request.Image = fileName;
            }

            ret.Collection = await _service.CreateAsync(request);
            
            return StatusCode(ret.StatusCode, ret);
        }

        [HttpPost("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update(long id, [FromForm] BuildingVMDefaultFR request)
        {
            ReturnalModel ret = new() {};

            var (fileName, errMsg) = await _service.DoUploadAsync(request.FileImage);

            if (errMsg != null)
            {
                ret.Status = ReturnalType.Failed;
                // ret.Message = "Gagal Upload";
                ret.Message = errMsg;
                return StatusCode(ret.StatusCode, ret);
            }

            if (fileName != null)
            {
                request.Image = fileName;
            }

            var result = await _service.UpdateAsync(id, request);

            if (result == null)
            {
                ret.Status = ReturnalType.Failed;
                ret.Message = "Failed update a building";
                return StatusCode(ret.StatusCode, ret);
            }

            ret.Collection = result;
            ret.Message = "Success update a building";

            return StatusCode(ret.StatusCode, ret);
        }

        [HttpPost]
        public async Task<IActionResult> Remove([FromForm] BuildingVMDeleteFR request)
        {
            ReturnalModel ret = new() {};

            ret.Message = "Success delete a building";

            var result = await _service.DeleteAsync(request);
            
            if (result == null)
            {
                ret.Status = ReturnalType.Failed;
                ret.Message = "Failed delete a building";
            }

            return StatusCode(ret.StatusCode, ret);
        }
    }
}