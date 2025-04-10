using _2.Web.API.Controllers;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.Webview;

[AccessIdAuthorize("2")]
[Authorize(Policy = AuthorizationWebviewPolicies.OnlyWebview)]
[ApiController]
[ApiExplorerSettings(GroupName = "webview")]
[Tags("Booking")]
[Route("api/webview/booking")]
public class BookingController(IBookingProcessService _processService) : ControllerBase
{
    [HttpGet("get-booking-datatable")]
    public async Task<IActionResult> GetDataTables([FromQuery] BookingVMDataTableFR request)
    {
        ReturnalModel ret = new();

        var (collection, recordsTotal, recordsFiltered) = await _processService.GetAllItemDataTablesAsync(request);

        ret.Collection = new DataTableResponse
        {
            Draw = request.Draw,
            RecordsTotal = recordsTotal,
            RecordsFiltered = recordsFiltered,
            Data = collection
        };

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpGet("get-in-progress-booking")]
    public async Task<IActionResult> GetInProgressBooking()
    {
        ReturnalModel ret = await _processService.GetOngoingBookingAsync();
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost("create-new-order-booking")]
    public async Task<IActionResult> CreateNewOrder([FromForm] BookingVMCreateNewOrderFR request)
    {
        ReturnalModel ret = await _processService.CreateNewOrderAsync(request);
        return StatusCode(ret.StatusCode, ret);
    }
}
