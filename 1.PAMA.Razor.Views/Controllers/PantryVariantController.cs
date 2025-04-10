using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer.EnumType;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using _5.Helpers.Consumer.Policy;

namespace Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = AuthorizationWebviewPolicies.OnlyNonWebview)]
[Route("api/[controller]/[action]")]
[ApiController]
public class PantryVariantController(IVariantService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetVariantByPatryDetailId(long id)
    {
        var response = await service.GetVariantByMenuId(id);
        ReturnalModel ret = new()
        {
            Collection = response
        };
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpGet]
    public async Task<IActionResult> GetVariantId(string id)
    {
        var response = await service.GetVariantId(id);
        ReturnalModel ret = new()
        {
            Collection = response
        };
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMenuAndVariant([FromForm] PantryDetailMenuVariantViewModel CReq)
    {
        var type = await service.CreateMenuAndVariant(CReq);
        ReturnalModel ret = new()
        {
            Message = $"Success create a Pantry Detail Menu Variant {CReq.name}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed create a Pantry Detail Menu Variant {CReq.name}";
        }

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteVariant([FromForm] PantryDetailMenuVariantViewModel DReq)
    {
        var type = await service.DeleteVariantAndDetails(DReq);
        ReturnalModel ret = new()
        {
            Message = $"Success delete a Pantry Detail {DReq.name}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed delete a Pantry Detail {DReq.name}";
        }

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateVariant([FromForm] PantryDetailMenuVariantViewModel CReq)
    {
        var type = await service.UpdateVariantAndDetail(CReq);
        ReturnalModel ret = new()
        {
            Message = $"Success update a Pantry Detail Menu Variant {CReq.name}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed update a Pantry Detail Menu Variant {CReq.name}";
        }

        return StatusCode(ret.StatusCode, ret);
    }
}