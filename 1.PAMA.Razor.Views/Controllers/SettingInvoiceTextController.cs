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
/// Controller for managing invoice text settings.
/// </summary>
/// <remarks>
/// Provides endpoints for CRUD operations on invoice text settings.
/// </remarks>
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = AuthorizationWebviewPolicies.OnlyNonWebview)]
[Route("api/[controller]/[action]")]
[ApiController]
public class SettingInvoiceTextController(ISettingInvoiceTextService service)
    : BaseController<SettingInvoiceTextViewModel>(service)
{
    [HttpGet]
    public async Task<IActionResult> GetAllSettingInvoiceTexts()
    {
        var response = await service.GetAllSettingInvoiceTextsAsync();
        ReturnalModel ret = new()
        {
            Collection = response?.Data
        };
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpGet]
    public async Task<IActionResult> GetSettingInvoiceText(string id)
    {
        var request = new SettingInvoiceTextUpdateViewModelFR { Id = id };
        var response = await service.GetSettingInvoiceTextByIdAsync(request);
        ReturnalModel ret = new()
        {
            Collection = response?.Data
        };
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] SettingInvoiceTextCreateViewModelFR CReq)
    {
        var type = await service.CreateSettingInvoiceTextAsync(CReq);
        ReturnalModel ret = new()
        {
            Message = $"Success create a SettingInvoiceText {CReq.Name}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed create a SettingInvoiceText {CReq.Name}";
        }

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> Update([FromForm] SettingInvoiceTextUpdateViewModelFR UReq)
    {
        var type = await service.UpdateSettingInvoiceTextAsync(UReq);
        ReturnalModel ret = new()
        {
            Message = $"Success update a SettingInvoiceText {UReq.Name}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed update a SettingInvoiceText {UReq.Name}";
        }

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromForm] SettingInvoiceTextDeleteViewModelFR DReq)
    {
        var type = await service.DeleteSettingInvoiceTextAsync(DReq);
        ReturnalModel ret = new()
        {
            Message = $"Success delete a SettingInvoiceText {DReq.Name}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed delete a SettingInvoiceText {DReq.Name}";
        }

        return StatusCode(ret.StatusCode, ret);
    }
}