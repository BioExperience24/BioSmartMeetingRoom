using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer._Response;
using _5.Helpers.Consumer.EnumType;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

/// <summary>
/// Controller for managing LicenseSettings.
/// </summary>
/// <remarks>
/// Provides endpoints for CRUD operations on LicenseSettings.
/// </remarks>
[Route("api/[controller]/[action]")]
[ApiController]
public class LicenseSettingController(ILicenseSettingService service)
    : BaseController<LicenseSettingViewModel>(service)
{
    [HttpGet]
    public async Task<IActionResult> GetAllLicenseSettings()
    {
        var response = await service.GetAllLicenseSettingsAsync();
        ReturnalModel ret = new()
        {
            Collection = response?.Data
        };
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpGet]
    public async Task<IActionResult> GetLicenseSetting()
    {
        var response = await service.GetAllLicenseSettingsAsync();
        ReturnalModel ret = new()
        {
            Collection = response?.Data
        };
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpGet]
    public async Task<IActionResult> GetLicenseSettingMenu(string id)
    {
        var response = await service.GetAllLicenseSettingsAsync();
        ReturnalModel ret = new()
        {
            Collection = response?.Data
        };
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] LicenseSettingCreateViewModelFR CReq)
    {
        var type = await service.CreateLicenseSettingAsync(CReq);
        ReturnalModel ret = new()
        {
            Message = $"Success create a LicenseSetting {CReq.Serial}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed create a LicenseSetting {CReq.Serial}";
        }

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> Update([FromForm] LicenseSettingUpdateViewModelFR UReq)
    {
        var type = await service.UpdateLicenseSettingAsync(UReq);
        ReturnalModel ret = new()
        {
            Message = $"Success update a LicenseSetting {UReq.Serial}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed update a LicenseSetting {UReq.Serial}";
        }

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromForm] LicenseSettingDeleteViewModelFR DReq)
    {
        var type = await service.DeleteLicenseSettingAsync(DReq);
        ReturnalModel ret = new()
        {
            Message = $"Success delete a LicenseSetting {DReq.Serial}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed delete a LicenseSetting {DReq.Serial}";
        }

        return StatusCode(ret.StatusCode, ret);
    }
}