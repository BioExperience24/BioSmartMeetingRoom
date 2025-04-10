using System.Net;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.Webview;

[AllowAnonymous]
[ApiExplorerSettings(GroupName = "webview")]
[ApiController]
[Route("api/webview")]
public class UnAuthorizeController(IUserService userService)
    : ControllerBase
{


    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> WebviewLogin(LoginWebviewModel request)
    {
        var ret = await userService.WebviewLogin(request);
        return StatusCode(ret.StatusCode, ret);
    }
}