using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer._Response;
using _5.Helpers.Consumer.EnumType;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

/// <summary>
/// Controller for managing variable time durations.
/// </summary>
/// <remarks>
/// Provides endpoints for CRUD operations on variable time durations.
/// </remarks>
[Route("api/[controller]/[action]")]
[ApiController]
public class VariableTimeDurationController(IVariableTimeDurationService service)
    : BaseControllerId<VariableTimeDurationViewModel>(service)
{

    //VariableTimeDurations dan VariableTimeExtend
    [HttpGet]
    public async Task<IActionResult> GetAllVariablesAsync() 
    {
        var response = await service.GetAllVariablesAsync();
        ReturnalModel ret = new()
        {
            Collection = response?.Data
        };
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllVariableTimeDurations()
    {
        var response = await service.GetAllVariableTimeDurationsAsync();
        ReturnalModel ret = new()
        {
            Collection = response?.Data
        };
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpGet]
    public async Task<IActionResult> GetVariableTimeDuration(string id)
    {
        var request = new VariableTimeDurationUpdateViewModelFR { Id = long.Parse(id) };
        var response = await service.GetVariableTimeDurationByIdAsync(request);
        ReturnalModel ret = new()
        {
            Collection = response?.Data
        };
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] VariableTimeDurationCreateViewModelFR CReq)
    {
        var type = await service.CreateVariableTimeDurationAsync(CReq);
        ReturnalModel ret = new()
        {
            Message = $"Success create a VariableTimeDuration {CReq.Time}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed create a VariableTimeDuration {CReq.Time}";
        }

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> Update([FromForm] VariableTimeDurationUpdateViewModelFR UReq)
    {
        var type = await service.UpdateVariableTimeDurationAsync(UReq);
        ReturnalModel ret = new()
        {
            Message = $"Success update a VariableTimeDuration {UReq.Time}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed update a VariableTimeDuration {UReq.Time}";
        }

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromForm] VariableTimeDurationDeleteViewModelFR DReq)
    {
        var type = await service.DeleteVariableTimeDurationAsync(DReq);
        ReturnalModel ret = new()
        {
            Message = $"Success delete a VariableTimeDuration {DReq.Id}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed delete a VariableTimeDuration {DReq.Id}";
        }

        return StatusCode(ret.StatusCode, ret);
    }
}