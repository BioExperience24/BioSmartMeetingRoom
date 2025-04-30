using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer.EnumType;
using _5.Helpers.Consumer.Policy;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _1.PAMA.Razor.Views.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CheckLogin([FromForm] LoginModel request)
    {
        var ret = await _service.CheckLogin(request);

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> RequestToken([FromForm] LoginModel request)
    {
        var ret = await _service.RequestToken(request);

        return StatusCode(ret.StatusCode, ret);
    }

    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(UserViewModel), 200)]
    public async Task<IActionResult> GetAuthUser()
    {
        var users = await _service.GetAuthUser();
        return Ok(users);
    }
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAllClaims()
    {
        var claims = _service.GetAllClaims();
        return Ok(claims);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = AuthorizationWebviewPolicies.OnlyNonWebview)]
    [HttpGet]
    public async Task<ActionResult> GetItems()
    {
        ReturnalModel ret = new();

        ret.Collection = await _service.GetItemsAsync();

        return StatusCode(ret.StatusCode, ret);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = AuthorizationWebviewPolicies.OnlyNonWebview)]
    [HttpGet("{id}")]
    public async Task<ActionResult> GetItemById(long id)
    {
        ReturnalModel ret = new();

        ret.Collection = await _service.GetById(id);

        if (ret.Collection == null)
        {
            ret.Status = ReturnalType.Failed;
            ret.Message = "Get failed";
        }


        return StatusCode(ret.StatusCode, ret);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = AuthorizationWebviewPolicies.OnlyNonWebview)]
    [HttpPost]
    public async Task<IActionResult> Create([FromForm] UserVMCreateFR request)
    {
        ReturnalModel ret = new();

        ret.Message = "Success create a user";
        ret.Collection = await _service.CreateAsync(request);

        if (ret.Collection == null)
        {
            ret.Status = ReturnalType.Failed;
            ret.Message = "Failed create a user";
        }

        return StatusCode(ret.StatusCode, ret);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = AuthorizationWebviewPolicies.OnlyNonWebview)]
    [HttpPost]
    public async Task<IActionResult> Update([FromForm] UserVMUpdateFR request)
    {
        ReturnalModel ret = new();

        ret.Message = "Success update a user";
        ret.Collection = await _service.UpdateAsync(request);

        if (ret.Collection == null)
        {
            ret.Status = ReturnalType.Failed;
            ret.Message = "Failed update a user";
        }

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost("{id}")]
    public async Task<IActionResult> UpdateUsername([FromForm] UserVMUpdateUsernameFR request, long id)
    {
        ReturnalModel ret = new();

        ret.Message = "Success update username.";
        var updatedUsername = await _service.UpdateUsernameAsync(request, id);

        if (updatedUsername.Status == ReturnalType.Failed)
        {
            ret.Status = ReturnalType.Failed;
            ret.Message = updatedUsername.Message;
        }

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost("{id}")]
    public async Task<IActionResult> UpdatePassword([FromForm] UserVMUpdatePasswordFR request, long id)
    {
        ReturnalModel ret = new();

        ret.Message = "Success update password.";
        var updatedPassword = await _service.UpdatePassword(request, id);

        if (updatedPassword.Status == ReturnalType.Failed)
        {
            ret.Status = ReturnalType.Failed;
            ret.Message = updatedPassword.Message;
        }

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> Disable([FromForm] UserVMDisableFR request)
    {
        ReturnalModel ret = new();

        ret.Message = "Success disable a user";
        ret.Collection = await _service.DisableAsync(request);


        if (ret.Collection == null)
        {
            ret.Status = ReturnalType.Failed;
            ret.Message = "Failed disable a user";
        }

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> Remove([FromForm] UserVMDeleteFR request)
    {
        ReturnalModel ret = new();

        ret.Message = "Success delete a user";
        ret.Collection = await _service.DeleteAsync(request);


        if (ret.Collection == null)
        {
            ret.Status = ReturnalType.Failed;
            ret.Message = "Failed delete a user";
        }

        return StatusCode(ret.StatusCode, ret);
    }

}