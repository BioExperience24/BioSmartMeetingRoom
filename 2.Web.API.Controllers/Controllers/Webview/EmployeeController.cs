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
    [Tags("employee")]
    [Route("api/webview/employee")]
    public class EmployeeController(IEmployeeService service) : ControllerBase
    {
        [HttpGet("get-employees")]
        public async Task<ActionResult> GetItems()
        {
            ReturnalModel ret = new();

            ret.Message = "Get Success";
            ret.Collection = await service.GetItemsAsync();

            return StatusCode(ret.StatusCode, ret);
        }
    }
}