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
/// 
/// </summary>
/// <remarks>
/// 
/// </remarks>
/// <param name="service"></param>
/// <param name="httpCont"></param>
//[Authorize]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = AuthorizationWebviewPolicies.OnlyNonWebview)]
[Route("api/[controller]/[action]")]
[ApiController]
public class FacilityController(IFacilityService service)
    : BaseLongController<FacilityViewModel>(service)
{

    [HttpGet]
    public async Task<IActionResult> GetAllFacility()
    {
        var response = await service.GetAllFacilityAsync();
        ReturnalModel ret = new()
        {
            Collection = response?.Data
        };
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpGet]
    public async Task<IActionResult> GetFacility()
    {
        var response = await service.GetAllFacilityAsync();
        ReturnalModel ret = new()
        {
            Collection = response?.Data
        };
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] FacilityCreateViewModelFR CReq)
    {
        var type = await service.CreateFacilityAsync(CReq);
        ReturnalModel ret = new()
        {
            Message = $"Success create a facility {CReq.Name}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed create a facility {CReq.Name}";
        }

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost("{id}")]
    public async Task<IActionResult> Update(long id, [FromForm] FacilityUpdateViewModelFR UReq)
    {
        UReq.Id = id;
        var type = await service.UpdateFacilityAsync(UReq);
        ReturnalModel ret = new()
        {
            Message = $"Success update a facility {UReq.Name}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed update a facility {UReq.Name}";
        }

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromForm] FacilityDeleteViewModelFR DReq)
    {
        var type = await service.DeleteFacilityAsync(DReq);
        ReturnalModel ret = new()
        {
            Message = $"Success delete a facility {DReq.Name}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed delete a facility {DReq.Name}";
        }

        return StatusCode(ret.StatusCode, ret);
    }
}
