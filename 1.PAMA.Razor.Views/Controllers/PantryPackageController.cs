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
public class PantryPackageController(IPantryMenuPaketService service)
    : BaseController<PantryMenuPaketViewModel>(service)
{
    [HttpGet]
    public async Task<IActionResult> GetPackageAndDetail(string id)
    {
        var response = await service.GetPackageAndDetail(id);
        ReturnalModel ret = new()
        {
            Collection = response
        };
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> UpdatePackage([FromForm] PantryMenuPaketViewModel UReq)
    {
        var type = await service.UpdatePackage(UReq);
        ReturnalModel ret = new()
        {
            Message = $"Success update a Pantry Detail {UReq.name}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed update a Pantry Detail {UReq.name}";
        }

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> DeletePackage([FromForm] PantryMenuPaketViewModel DReq)
    {
        var type = await service.DeletePackage(DReq.Id);
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
}