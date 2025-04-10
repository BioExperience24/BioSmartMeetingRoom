using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer._Response;
using _5.Helpers.Consumer.EnumType;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using _5.Helpers.Consumer.Policy;

namespace Controllers;

/// <summary>
/// Controller for managing SMTP settings.
/// </summary>
/// <remarks>
/// Provides endpoints for CRUD operations on SMTP settings.
/// </remarks>
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = AuthorizationWebviewPolicies.OnlyNonWebview)]
[Route("api/[controller]/[action]")]
[ApiController]
public class SettingSmtpController(ISettingSmtpService service)
    : BaseController<SettingSmtpViewModel>(service)
{
    [HttpGet]
    public async Task<IActionResult> GetAllSettingSmtps()
    {
        var response = await service.GetAllSettingSmtpsAsync();
        ReturnalModel ret = new()
        {
            Collection = response?.Data
        };
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpGet]
    public async Task<IActionResult> GetSettingSmtp()
    {
        var response = await service.GetAllSettingSmtpsAsync();
        ReturnalModel ret = new()
        {
            Collection = response?.Data
        };
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpGet]
    public async Task<IActionResult> GetSettingSmtpMenu(string id)
    {
        var response = await service.GetAllSettingSmtpsAsync();
        ReturnalModel ret = new()
        {
            Collection = response?.Data
        };
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] SettingSmtpCreateViewModelFR CReq)
    {
        var type = await service.CreateSettingSmtpAsync(CReq);
        ReturnalModel ret = new()
        {
            Message = $"Success create a SettingSmtp {CReq.Name}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed create a SettingSmtp {CReq.Name}";
        }

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> Update([FromForm] SettingSmtpUpdateViewModelFR UReq)
    {
        var type = await service.UpdateSettingSmtpAsync(UReq);
        ReturnalModel ret = new()
        {
            Message = $"Success update a SettingSmtp {UReq.Name}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed update a SettingSmtp {UReq.Name}";
        }

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromForm] SettingSmtpDeleteViewModelFR DReq)
    {
        var type = await service.DeleteSettingSmtpAsync(DReq);
        ReturnalModel ret = new()
        {
            Message = $"Success delete a SettingSmtp {DReq.Name}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed delete a SettingSmtp {DReq.Name}";
        }

        return StatusCode(ret.StatusCode, ret);
    }
}