using _2.Web.API.Controllers;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.Webview;

[ApiController]
[ApiExplorerSettings(GroupName = "webview")]
[Route("api/webview/pantry-package")]
[AccessIdAuthorize("2")]
[Authorize(Policy = AuthorizationWebviewPolicies.OnlyWebview)]
public class PantryPackageController(
    IPantryMenuPaketService _pantryMenuPaketService
) : ControllerBase
{

    [HttpGet("get-pantry-package-detail")]
    public async Task<IActionResult> GetPackageAndDetail(string id)
    {
        var response = await _pantryMenuPaketService.GetPackageAndDetail(id);
        ReturnalModel ret = new()
        {
            Collection = response
        };
        return StatusCode(ret.StatusCode, ret);
    }


    [HttpGet("get-all-pantry-package")]
    public virtual async Task<ActionResult> PantryPackageGetAll()
    {
        ReturnalModel ret = new()
        {
            Collection = await _pantryMenuPaketService.GetAll()
        };
        return Ok(ret);
    }
}
