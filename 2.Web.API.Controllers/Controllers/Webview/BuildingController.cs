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
    [Tags("Building")]
    [Route("api/webview/building")]
    public class BuildingController(IBuildingService _service) : ControllerBase
    {
        
        [HttpGet("get-buildings")]
        public async Task<IActionResult> GetItems()
        {
            var response = await _service.GetAllItemAsync();
            ReturnalModel ret = new()
            {
                Collection = response
            };
            return StatusCode(ret.StatusCode, ret);
        }
    }
}