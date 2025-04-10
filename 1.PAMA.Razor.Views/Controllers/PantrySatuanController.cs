using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
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
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = AuthorizationWebviewPolicies.OnlyNonWebview)]
[Route("api/[controller]/[action]")]
[ApiController]
public class PantrySatuanController(IPantrySatuanService service)
    : BaseLongController<PantrySatuanViewModel>(service)
{
    [HttpPost("{id?}")]
    public async Task<IActionResult> UpdatePantrySatuan(long id, [FromForm] PantrySatuanViewModel payload)
    {
        payload.Id = id;
        var type = await service.Update(payload);
        ReturnalModel ret = new()
        {
            Message = $"Success update a Pantry Satuan {payload.name}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed update a Pantry Satuan {payload.name}";
        }

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> DeletePantrySatuan([FromForm] PantrySatuanViewModel payload)
    {
        ReturnalModel ret = new()
        {
            Message = $"Success Delete a Pantry Satuan {payload.name}"
        };
        if (!payload.Id.HasValue)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Please provide ID";
            return StatusCode(ret.StatusCode, ret);
        }

        var type = await service.SoftDelete(payload.Id.Value);

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed Delete a Pantry Satuan {payload.name}";
        }

        return StatusCode(ret.StatusCode, ret);
    }

}
