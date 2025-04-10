using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer.Policy;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _1.PAMA.Razor.Views.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = AuthorizationWebviewPolicies.OnlyNonWebview)]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccessChannelController : ControllerBase
    {
        private readonly IAccessChannelService _service;

        public AccessChannelController(IAccessChannelService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            ReturnalModel ret = new();

            ret.Message = "Get success";

            ret.Collection = await _service.GetAll();

            return StatusCode(ret.StatusCode, ret);
        }

        [HttpGet]
        public async Task<IActionResult> TestApi()
        {
            return Ok("Test API");
        }
    }
}