using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer._Response;
using _5.Helpers.Consumer.EnumType;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

/// <summary>
/// Controller for managing email templates.
/// </summary>
/// <remarks>
/// Provides endpoints for CRUD operations on email templates.
/// </remarks>
[Route("api/[controller]/[action]")]
[ApiController]
public class SettingEmailTemplateController(ISettingEmailTemplateService service)
    : BaseController<SettingEmailTemplateViewModel>(service)
{
    [HttpGet]
    public async Task<IActionResult> GetAllEmailTemplates()
    {
        //var response = await service.GetAllEmailTemplatesAsync();
        var response = await service.GetAllSettingEmailTemplatesAsync();
        ReturnalModel ret = new()
        {
            Collection = response?.Data
        };
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpGet]
    public async Task<IActionResult> GetEmailTemplate(long id)
    {
        var response = await service.GetSettingEmailTemplateByIdAsync(id);
        ReturnalModel ret = new()
        {
            Collection = response?.Data
        };
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] SettingEmailTemplateCreateViewModelFR CReq)
    {
        var type = await service.CreateSettingEmailTemplateAsync(CReq);
        ReturnalModel ret = new()
        {
            Message = $"Success create an Email Template {CReq.TitleOfText}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed to create an Email Template {CReq.TitleOfText}";
        }

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> Update([FromForm] SettingEmailTemplateUpdateViewModelFR UReq)
    {
        var type = await service.UpdateSettingEmailTemplateAsync(UReq);
        ReturnalModel ret = new()
        {
            Message = $"Success update an Email Template {UReq.TitleOfText}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed to update an Email Template {UReq.TitleOfText}";
        }

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromForm] SettingEmailTemplateDeleteViewModelFR DReq)
    {
        var type = await service.DeleteSettingEmailTemplateAsync(DReq);
        ReturnalModel ret = new()
        {
            Message = $"Success delete an Email Template {DReq.Name}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed to delete an Email Template {DReq.Name}";
        }

        return StatusCode(ret.StatusCode, ret);
    }
}