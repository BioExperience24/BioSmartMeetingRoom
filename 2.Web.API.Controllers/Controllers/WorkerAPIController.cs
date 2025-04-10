using _2.Web.API.Controllers;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[AccessIdAuthorize("35")]
[ApiController]
[Route("worker/[controller]/[action]")]
public class WorkerAPIController(IWorkerAPIService service)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> CheckMeetingToday(DateOnly? dateNow = null)
    {
        ReturnalModel ret = await service.CheckMeetingToday(dateNow);
        return StatusCode(ret.StatusCode, ret);
    }
}