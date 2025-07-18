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
    [Tags("room")]
    [Route("api/webview/room")]
    public class RoomController(IRoomService service) : ControllerBase
    {
        [HttpGet("get-available-rooms")]
        public async Task<IActionResult> GetAvailableItems([FromQuery] RoomVMFindAvailable request)
        {
            ReturnalModel ret = new();

            ret.Message = "Get success";
            ret.Collection = await service.GetAllRoomAvailableAsync(request, false);

            return StatusCode(ret.StatusCode, ret);
        }
    }
}