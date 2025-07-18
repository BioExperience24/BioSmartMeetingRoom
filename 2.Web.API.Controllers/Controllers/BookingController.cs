using _2.Web.API.Controllers;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[AccessIdAuthorize("2")]
[ApiExplorerSettings(GroupName = "pama_smeet")]
[Authorize(Policy = AuthorizationWebviewPolicies.OnlyNonWebview)]
[ApiController]
[Route("api")]
public class BookingController(IAPIMainDisplayService _service)
    : ControllerBase
{

    [HttpPost("access/open/room/pin")]
    public async Task<IActionResult> CheckDoorOpenMeetingPin(CheckDoorOpenMeetingPinFRViewModel request)
    {
        ReturnalModel ret = await _service.CheckDoorOpenMeetingPin(request);
        return StatusCode(ret.StatusCode, ret);
    }

}