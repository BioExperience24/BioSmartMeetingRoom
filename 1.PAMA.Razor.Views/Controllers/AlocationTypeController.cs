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
public class AlocationTypeController(IAlocationTypeService service)
    : BaseController<AlocationTypeViewModel>(service)
{
    [HttpGet]
    public async Task<IActionResult> GetItems()
    {
        var response = await service.GetAll();
        ReturnalModel ret = new()
        {
            Collection = response
        };
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] AlocationTypeVMDefaultFR CTypeReq)
    {
        var type = await service.CreateAsync(CTypeReq);
        ReturnalModel ret = new()
        {
            Message = $"Success create a alocation type {CTypeReq.Name}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed create a alocation type {CTypeReq.Name}";
        }

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> Update([FromForm] AlocationTypeVMUpdateFR UTypeReq)
    {
        var type = await service.UpdateAsync(UTypeReq);
        ReturnalModel ret = new()
        {
            Message = $"Success update a alocation type {UTypeReq.Name}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed update a alocation type {UTypeReq.Name}";
        }

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> Remove([FromForm] AlocationTypeVMDefaultFR DTypeReq)
    {
        var type = await service.DeleteAsync(DTypeReq);
        ReturnalModel ret = new()
        {
            Message = $"Success delete a alocation type {DTypeReq.Name}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed delete a alocation type {DTypeReq.Name}";
        }

        return StatusCode(ret.StatusCode, ret);
    }
}
