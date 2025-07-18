using _3.BusinessLogic.Services.Interface;
using _5.Helpers.Consumer.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _2.Web.API.Controllers.Controllers.Webview
{
    // [AccessIdAuthorize("2")]
    [Authorize(Policy = AuthorizationWebviewPolicies.OnlyWebview)]
    [ApiController]
    [ApiExplorerSettings(GroupName = "webview")]
    [Tags("User")]
    [Route("api/webview/user")]
    public class UserController(IUserService _service) : ControllerBase
    {
        [HttpGet("get-claims")]
        public async Task<IActionResult> GetClaims()
        {
            var claims = _service.GetAllClaims();
            return Ok(claims);
        }
    }
}