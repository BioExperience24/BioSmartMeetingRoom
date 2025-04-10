using System.Text.Json;
using _3.BusinessLogic.Services.Implementation;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer.EnumType;
using _5.Helpers.Consumer.Policy;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _1.PAMA.Razor.Views.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = AuthorizationWebviewPolicies.OnlyNonWebview)]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccessControlController : ControllerBase
    {
        private readonly IAccessControlService _service;
        private readonly IAccessIntegratedService _accessIntegratedService;

        public AccessControlController(IAccessControlService service, IAccessIntegratedService accessIntegratedService)
        {
            _service = service;
            _accessIntegratedService = accessIntegratedService; 
        }

        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            ReturnalModel ret = new();
            ret.Message = "Get success";

            ret.Collection = await _service.GetAllItemAsync();

            return StatusCode(ret.StatusCode, ret);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemById(string id)
        {
            ReturnalModel ret = new();

            var access = await _service.GetById(id);

            if (access == null)
            {
                ret.Status = ReturnalType.Failed;
                ret.Message = "error get data";
                return StatusCode(ret.StatusCode, ret);
            }

            var integrate = await _accessIntegratedService.GetAllItemByAccessIdAsync(id);


            ret.Collection = new Dictionary<string, object>()
            {
                {"access", access!},
                {"integrate", integrate!}
            };

            return StatusCode(ret.StatusCode, ret);
        }

        [HttpGet]
        public async Task<IActionResult> GetItemRooms()
        {
            ReturnalModel ret = new();
            ret.Message = "Get success";

            ret.Collection = await _service.GetAllItemRoomAsync();

            return StatusCode(ret.StatusCode, ret);
        }

        [HttpGet]
        public async Task<IActionResult> GetItemRoomRoomDisplays()
        {
            ReturnalModel ret = new();
            ret.Message = "Get success";

            ret.Collection = await _service.GetAllItemRoomRoomDisplayAsync();

            return StatusCode(ret.StatusCode, ret);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] AccessControlVMCreateFR request)
        {
            ReturnalModel ret = new();
            ret.Message = "Success create a access control";

            ret.Collection = await _service.CreateAsync(request);

            return StatusCode(ret.StatusCode, ret);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Update(string id, [FromForm] AccessControlVMUpdateFR request)
        {
            ReturnalModel ret = new();

            request.Id = id;

            ret.Message = "Success update a access";

            var result = await _service.UpdateAsync(request);

            if (result == null)
            {
                ret.Status = ReturnalType.Failed;
                ret.Message = "Failed update a access";
            }

            return StatusCode(ret.StatusCode, ret);
        }

        [HttpPost]
        public async Task<IActionResult> Remove([FromForm] AccessControlVMDeleteFR request)
        {
            ReturnalModel ret = new();

            ret.Message = "Success delete a access";

            var result = await _service.DeleteAsync(request);

            if (result == null)
            {
                ret.Status = ReturnalType.Failed;
                ret.Message = "Failed delete a access";
            }

            return StatusCode(ret.StatusCode, ret);
        }
    }
}