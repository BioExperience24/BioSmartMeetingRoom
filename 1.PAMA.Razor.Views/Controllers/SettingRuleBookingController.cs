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
/// Controller for managing setting rule bookings.
/// </summary>
/// <remarks>
/// Provides endpoints for CRUD operations on setting rule bookings.
/// </remarks>
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = AuthorizationWebviewPolicies.OnlyNonWebview)]
[Route("api/[controller]/[action]")]
[ApiController]
public class SettingRuleBookingController(ISettingRuleBookingService service)
    : BaseControllerId<SettingRuleBookingViewModel>(service)
{
    [HttpGet]
    public async Task<IActionResult> GetAllSettingRuleBookings()
    {
        var response = await service.GetAllSettingRuleBookingsAsync();
        ReturnalModel ret = new()
        {
            Collection = response?.Data
        };
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpGet]
    public async Task<IActionResult> GetSettingRuleBooking(string id)
    {
        //var request = new SettingRuleBookingUpdateViewModelFR ();
        var response = await service.GetAllSettingRuleBookingsAsync();
        ReturnalModel ret = new()
        {
            Collection = response?.Data
        };
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] SettingRuleBookingCreateViewModelFR CReq)
    {
        var type = await service.CreateSettingRuleBookingAsync(CReq);
        ReturnalModel ret = new()
        {
            Message = $"Success create a SettingRuleBooking {CReq.Duration}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed create a SettingRuleBooking {CReq.Duration}";
        }

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> Update([FromForm] SettingRuleBookingUpdateViewModelFR UReq)
    {
        var type = await service.UpdateSettingRuleBookingAsync(UReq);
        ReturnalModel ret = new()
        {
            Message = $"Success update a SettingRuleBooking {UReq.Duration}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed update a SettingRuleBooking {UReq.Duration}";
        }

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromForm] SettingRuleBookingDeleteViewModelFR DReq)
    {
        var type = await service.DeleteSettingRuleBookingAsync(DReq);
        ReturnalModel ret = new()
        {
            Message = $"Success delete a SettingRuleBooking {DReq.Id}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed delete a SettingRuleBooking {DReq.Id}";
        }

        return StatusCode(ret.StatusCode, ret);
    }
}