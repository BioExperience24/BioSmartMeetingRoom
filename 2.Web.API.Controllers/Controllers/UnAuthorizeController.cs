using System.Net;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[AllowAnonymous]
[ApiExplorerSettings(GroupName = "pama_smeet")]
[ApiController]
[Route("api")]
public class UnAuthorizeController(IUserService userService, IAPIMainDisplayService _APIMainDisplayService,
        IS3Service _s3Service)
    : ControllerBase
{

    [HttpPost]
    [Route("_Auth/RequestToken")]
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
    
    [AllowAnonymous]
    [HttpGet("display/qr/detail-view")]
    public IActionResult GetQrCodeDetailView(string fileName)
    {

        var file = _s3Service.GetPublicFileUrl("qr", fileName);

        return StatusCode((int)HttpStatusCode.OK, file);
    }
}