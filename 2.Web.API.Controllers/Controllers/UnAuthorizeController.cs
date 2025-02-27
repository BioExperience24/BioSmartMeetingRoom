using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[AllowAnonymous]
[ApiController]
[Route("v1")]
public class UnAuthorizeController(IUserService userService)
    : ControllerBase
{

    [HttpPost]
    [Route("/api/_Auth/RequestToken")]
    public async Task<ActionResult> RequestToken(LoginModel request)
    {
        var ret = await userService.RequestToken(request);
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost("pantry/login")]
    public async Task<IActionResult> PantryLogin(LoginModel request)
    {
        var ret = await userService.PantryLogin(request);
        return StatusCode(ret.StatusCode, ret);
    }

    [AllowAnonymous]
    [HttpPost("display/login")]
    public async Task<IActionResult> DisplayLogin(LoginModel request)
    {
        var ret = await userService.DisplayLogin(request);
        return StatusCode(ret.StatusCode, ret);
    }
}