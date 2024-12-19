using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer._Response;
using _5.Helpers.Consumer.EnumType;
using _7.Entities.Models;
using Microsoft.AspNetCore.Mvc;

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
[Route("api/[controller]/[action]")]
[ApiController]
public class SettingPantryConfigController(ISettingPantryConfigService service)
    : BaseControllerId<SettingPantryConfigViewModel>(service)
{

    [HttpGet]
    public async Task<IActionResult> GetAllSettingPantryConfigs()
    {
        var response = await service.GetAllSettingPantryConfigsAsync();
        ReturnalModel ret = new()
        {
            Collection = response?.Data
        };
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpGet]
    public async Task<IActionResult> GetSettingPantryConfig()
    {
        var response = await service.GetAllSettingPantryConfigsAsync();
        ReturnalModel ret = new()
        {
            Collection = response?.Data
        };
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpGet]
    public async Task<IActionResult> GetSettingPantryConfigMenu(string id)
    {
        var response = await service.GetAllSettingPantryConfigsAsync();
        ReturnalModel ret = new()
        {
            Collection = response?.Data
        };
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] SettingPantryConfigCreateViewModelFR CReq)
    {
        var type = await service.CreateSettingPantryConfigAsync(CReq);
        ReturnalModel ret = new()
        {
            Message = $"Success create a SettingPantryConfig {CReq.Status}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed create a SettingPantryConfig {CReq.Status}";
        }

        return StatusCode(ret.StatusCode, ret);
    }
    [HttpPost]
    public async Task<IActionResult?> UpdateOrCreatePantryConfigAsync([FromForm]  SettingPantryConfigCreateViewModelFR request)
    {
        var type = await service.UpdateOrCreateSettingPantryConfigAsync(request);
        ReturnalModel ret = new()
        {
            Message = $"Success update a SettingPantryConfig {request.Status}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed update a SettingPantryConfig {request.Status}";
        }

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> Update([FromForm] SettingPantryConfigCreateViewModelFR UReq)
    {
        var type = await service.UpdateSettingPantryConfigAsync(UReq);
        ReturnalModel ret = new()
        {
            Message = $"Success update a SettingPantryConfig {UReq.Status}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed update a SettingPantryConfig {UReq.Status}";
        }

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromForm] SettingPantryConfigDeleteViewModelFR DReq)
    {
        var type = await service.DeleteSettingPantryConfigAsync(DReq);
        ReturnalModel ret = new()
        {
            Message = $"Success delete a SettingPantryConfig {DReq.Name}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed delete a SettingPantryConfig {DReq.Name}";
        }

        return StatusCode(ret.StatusCode, ret);
    }
}