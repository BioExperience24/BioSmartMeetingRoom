using System.Text.Json;
using _2.Web.API.Controllers;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer.EnumType;
using _5.Helpers.Consumer.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.Webview;

// [AccessIdAuthorize("2")]
[Authorize(Policy = AuthorizationWebviewPolicies.OnlyWebview)]
[ApiController]
[ApiExplorerSettings(GroupName = "webview")]
[Tags("Booking")]
[Route("api/webview/booking")]
public class BookingController(IBookingProcessService _processService) : ControllerBase
{
    [HttpGet("get-booking-datatable")]
    public async Task<IActionResult> GetDataTables([FromQuery] BookingWebViewListFR request)
    {
        ReturnalModel ret = new();
    
        int draw = (int)(DateTimeOffset.UtcNow.ToUnixTimeSeconds() % int.MaxValue);

        BookingVMDataTableFR req = new BookingVMDataTableFR
        {
            Draw = draw,
            Start = request.Start,
            Length = request.Length,
            BookingDate = request.BookingDate,
            SortColumn = "booking_date",
            SortDir = "desc",
            Status = request.Status,
        };

        var (collection, recordsTotal, recordsFiltered) = await _processService.GetAllItemDataTablesAsync(req);

        ret.Collection = new DataTableResponse
        {
            Draw = draw,
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

    [HttpPost("cancel-booking")]
    public async Task<IActionResult> CancelBooking([FromForm] BookingVMCancelFR request)
    {
        ReturnalModel ret = await _processService.CancelBookingAsync(request);
        
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost("cancel-all-booking")]
    public async Task<IActionResult> CancelAllBooking([FromForm] BookingVMCancelAllFR request)
    {
        BookingVMCancelAllFR req = new BookingVMCancelAllFR
        {
            Id = request.Id,
            BookingId = request.BookingId,
            Name = request.Name,
            Reason = request.Reason,
        };

        ReturnalModel ret = new();
        if (!request.IsAll)
        {
            ret = await _processService.CancelBookingAsync(req);
        }
        else
        {
            ret = await _processService.CancelAllBookingAsync(req);
        }
        
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost("end-meeting")]
    public async Task<IActionResult> EndMeeting([FromForm] BookingVMEndMeetingFR request)
    {
        ReturnalModel ret = await _processService.EndMeetingAsync(request);

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost("create-booking")]
    public async Task<IActionResult> CreateReserve([FromForm] BookingVMCreateReserveFR request)
    {
        ReturnalModel ret = new();

        var (collcetion, msg) = await _processService.CreateBookingAsync(request);

        if (collcetion != null)
        {
            ret.Collection = collcetion;
        } else {
            ret.Status = ReturnalType.Failed;
        }
        ret.Message = msg ?? "Get success";
        
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpGet("check-available-time/{bookingId}/{date}/{roomId}")]
    public async Task<IActionResult> CheckRescheduleDate(string bookingId, DateOnly date, string roomId)
    {
        ReturnalModel ret = await _processService.CheckRescheduleDateAsync(bookingId, date, roomId);
        
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost("reschedule")]
    public async Task<IActionResult> RescheduleBooking([FromForm] BookingVMRescheduleFR request)
    {
        ReturnalModel ret = await _processService.RescheduleBookingAsync(request);

        return StatusCode(ret.StatusCode, ret);
    }
}
