using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _2.Web.API.Controllers.Controllers.Webview
{
    // [AccessIdAuthorize("2")]
    [Authorize(Policy = AuthorizationWebviewPolicies.OnlyWebview)]
    [ApiController]
    [ApiExplorerSettings(GroupName = "webview")]
    [Tags("facility")]
    [Route("api/webview/facility")]
    public class FacilityController(IFacilityService service) : ControllerBase
    {
        [HttpGet("get-facilities")]
        public async Task<IActionResult> GetAllFacility()
        {
            var response = await service.GetAllFacilityAsync();
            ReturnalModel ret = new()
            {
                Collection = response?.Data
            };
            return StatusCode(ret.StatusCode, ret);
        }
    }
}