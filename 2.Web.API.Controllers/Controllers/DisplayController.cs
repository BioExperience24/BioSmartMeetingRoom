using _2.Web.API.Controllers;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[AccessIdAuthorize("2")]
[ApiController]
[Route("api")]
public class DisplayController(IAPIMainDisplayService _service)
    : ControllerBase
{

    [HttpPost("display/schedule/fastbooked")]
    public async Task<IActionResult> DisplayFastBooked(BookingDisplayScheduleFastBookedFRViewModel request)
    {
        ReturnalModel ret = await _service.DisplayScheduleFastBooked(request);
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost("display/schedule/merge/list")]
    public async Task<IActionResult> DisplayGetRoomMerge(ListBookingByDateFR request)
    {
        ReturnalModel ret = await _service.DisplayGetRoomMerge(request);
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost("display/schedule/merge/get-time-booked")]
    public async Task<IActionResult> DisplayGetTimeBookedMerge(ListBookingByDateFR request)
    {
        ReturnalModel ret = await _service.DisplayScheduleMergeTimeBooked(request);
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost("display/schedule/get-time-booked")]
    public async Task<IActionResult> DisplayGetTimeBooked(ListBookingByDateNikFRViewModel request)
    {
        ReturnalModel ret = await _service.DisplayScheduleTimeFastBooked(request);
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost("display/room/list")]
    public async Task<IActionResult> DisplayGetRoomList()
    {
        ReturnalModel ret = await _service.GetDisplayRoomList();
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost("display/room/merge/list")]
    public async Task<IActionResult> DisplayGetRoomMergeList(ListRoomMergeFRViewModel request)
    {
        ReturnalModel ret = await _service.GetDisplayRoomMergeList(request);
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost("display/schedule/get-occupied")]
    public async Task<IActionResult> DisplayGetOccupied(ListScheduleOccupiedFRViewModel request)
    {
        ReturnalModel ret = await _service.GetDisplayScheduleOccupiedList(request);
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost("display/check-serial")]
    public async Task<IActionResult> DisplayCheckSerial(SerialRequest request)
    {
        ReturnalModel ret = new();
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost("display/schedule/get-soon/today")]
    public async Task<IActionResult> DisplayGetMeetingListToday(ListDisplayMeetingScheduleTodayFRViewModel request)
    {
        ReturnalModel ret = await _service.DisplayMeetingWithMoreRoomListDisplay(request);
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost("display/schedule/get-occupied/today")]
    public async Task<IActionResult> DisplayGetMeetingListOccupiedToday(ListDisplayMeetingScheduleTodayFRViewModel request)
    {
        ReturnalModel ret = await _service.DisplayMeetingWithMoreRoomOccupiedListDisplay(request);
        return StatusCode(ret.StatusCode, ret);
    }
}