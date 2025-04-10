using System.Text.Json;
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
    public class AccessIntegratedController : ControllerBase
    {
        private readonly IAccessIntegratedService _service;

        public AccessIntegratedController(IAccessIntegratedService service)
        {
            _service = service;
        }

        [HttpPost("{accessId}")]
        public async Task<IActionResult> GetItemsByAccessId(string accessId)
        {
            ReturnalModel ret = new();
            ret.Message = "Get success";

            ret.Collection = await _service.GetAllItemByAccessIdAsync(accessId);

            return StatusCode(ret.StatusCode, ret);
        }

        [HttpPost]
        public async Task<IActionResult> Assign([FromForm] AccessIntegratedVMAssignFR request)
        {
            ReturnalModel ret = new();
            ret.Message = "Success save a access integrated";

            if(! await _service.AssignAsync(request))
            {
                ret.Status = ReturnalType.Failed;
                ret.Message = "Failed save a access integrated";
            }

            return StatusCode(ret.StatusCode, ret);
        }
    }
}