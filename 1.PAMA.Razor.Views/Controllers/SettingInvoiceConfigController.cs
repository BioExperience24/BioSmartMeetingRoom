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
/// Controller for managing invoice configurations.
/// </summary>
/// <remarks>
/// Provides endpoints for CRUD operations on invoice configurations.
/// </remarks>

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = AuthorizationWebviewPolicies.OnlyNonWebview)]
[Route("api/[controller]/[action]")]
[ApiController]
public class SettingInvoiceConfigController(ISettingInvoiceConfigService service)
    : BaseController<SettingInvoiceConfigViewModel>(service)
{
    [HttpGet]
    public async Task<IActionResult> GetAllInvoiceConfigs()
    {
        var response = await service.GetAllSettingInvoiceConfigsAsync();
        ReturnalModel ret = new()
        {
            Collection = response?.Data
        };
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpGet]
    public async Task<IActionResult> GetInvoiceConfig(long id)
    {
        var response = await service.GetSettingInvoiceConfigByIdAsync(id);
        ReturnalModel ret = new()
        {
            Collection = response?.Data
        };
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] SettingInvoiceConfigCreateViewModelFR CReq)
    {
        var type = await service.CreateSettingInvoiceConfigAsync(CReq);
        ReturnalModel ret = new()
        {
            Message = $"Success create an Invoice Config {CReq.UpText}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed to create an Invoice Config {CReq.UpText}";
        }

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> Update([FromForm] SettingInvoiceConfigUpdateViewModelFR UReq)
    {
        var type = await service.UpdateSettingInvoiceConfigAsync(UReq);
        ReturnalModel ret = new()
        {
            Message = $"Success update an Invoice Config {UReq.UpText}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed to update an Invoice Config {UReq.UpText}";
        }

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromForm] SettingInvoiceConfigDeleteViewModelFR DReq)
    {
        
        var type = await service.DeleteSettingInvoiceConfigAsync(DReq);
        ReturnalModel ret = new()
        {
            Message = $"Success delete an Invoice Config {DReq.Name}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed to delete an Invoice Config {DReq.Name}";
        }

        return StatusCode(ret.StatusCode, ret);
    }
}