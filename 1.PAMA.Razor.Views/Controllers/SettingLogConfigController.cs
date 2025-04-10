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
/// 
/// </summary>
/// <remarks>
/// 
/// </remarks>
/// <param name="service"></param>
/// <param name="httpCont"></param>
//[Authorize]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = AuthorizationWebviewPolicies.OnlyNonWebview)]
[Route("api/[controller]/[action]")]
[ApiController]
public class SettingLogConfigController(ISettingLogConfigService service)
    : BaseController<SettingLogConfigViewModel>(service)
{

    [HttpGet]
    public async Task<IActionResult> GetAllSettingLogConfigs()
    {
        var response = await service.GetAllSettingLogConfigsAsync();
        ReturnalModel ret = new()
        {
            Collection = response?.Data
        };
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpGet]
    public async Task<IActionResult> GetSettingLogConfig()
    {
        var response = await service.GetAllSettingLogConfigsAsync();
        ReturnalModel ret = new()
        {
            Collection = response?.Data
        };
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpGet]
    public async Task<IActionResult> GetSettingLogConfigMenu(string id)
    {
        var response = await service.GetAllSettingLogConfigsAsync();
        ReturnalModel ret = new()
        {
            Collection = response?.Data
        };
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] SettingLogConfigCreateViewModelFR CReq)
    {
        var type = await service.CreateSettingLogConfigAsync(CReq);
        ReturnalModel ret = new()
        {
            Message = $"Success create a SettingLogConfig {CReq.Text}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed create a SettingLogConfig {CReq.Text}";
        }

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> Update([FromForm] SettingLogConfigUpdateViewModelFR UReq)
    {
        var type = await service.UpdateSettingLogConfigAsync(UReq);
        ReturnalModel ret = new()
        {
            Message = $"Success update a SettingLogConfig {UReq.Text}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed update a SettingLogConfig {UReq.Text}";
        }

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromForm] SettingLogConfigDeleteViewModelFR DReq)
    {
        var type = await service.DeleteSettingLogConfigAsync(DReq);
        ReturnalModel ret = new()
        {
            Message = $"Success delete a SettingLogConfig {DReq.Name}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed delete a SettingLogConfig {DReq.Name}";
        }

        return StatusCode(ret.StatusCode, ret);
    }
}