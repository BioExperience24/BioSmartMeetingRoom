using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer.EnumType;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using _5.Helpers.Consumer.Policy;

namespace Controllers;

/// <summary>
/// Controller for managing variable time extensions.
/// </summary>
/// <remarks>
/// Provides endpoints for CRUD operations on variable time extensions.
/// </remarks>
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = AuthorizationWebviewPolicies.OnlyNonWebview)]
[Route("api/[controller]/[action]")]
[ApiController]
public class VariableTimeExtendController(IVariableTimeExtendService service)
    : BaseControllerId<VariableTimeExtendViewModel>(service)
{
    [HttpGet]
    public async Task<IActionResult> GetAllVariableTimeExtends()
    {
        var response = await service.GetAllVariableTimeExtendsAsync();
        ReturnalModel ret = new()
        {
            Collection = response?.Data
        };
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpGet]
    public async Task<IActionResult> GetVariableTimeExtend(string id)
    {
        var request = new VariableTimeExtendUpdateViewModelFR { Id = long.Parse(id) };
        var response = await service.GetVariableTimeExtendByIdAsync(request);
        ReturnalModel ret = new()
        {
            Collection = response?.Data
        };
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] VariableTimeExtendCreateViewModelFR CReq)
    {
        var type = await service.CreateVariableTimeExtendAsync(CReq);
        ReturnalModel ret = new()
        {
            Message = $"Success create a VariableTimeExtend {CReq.Time}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed create a VariableTimeExtend {CReq.Time}";
        }

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> Update([FromForm] VariableTimeExtendUpdateViewModelFR UReq)
    {
        var type = await service.UpdateVariableTimeExtendAsync(UReq);
        ReturnalModel ret = new()
        {
            Message = $"Success update a VariableTimeExtend {UReq.Time}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed update a VariableTimeExtend {UReq.Time}";
        }

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromForm] VariableTimeExtendDeleteViewModelFR DReq)
    {
        var type = await service.DeleteVariableTimeExtendAsync(DReq);
        ReturnalModel ret = new()
        {
            Message = $"Success delete a VariableTimeExtend {DReq.Id}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed delete a VariableTimeExtend {DReq.Id}";
        }

        return StatusCode(ret.StatusCode, ret);
    }
}