using _2.Web.API.Controllers;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer.Policy;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[AccessIdAuthorize("2")]
[ApiExplorerSettings(GroupName = "pama_smeet")]
[Authorize(Policy = AuthorizationWebviewPolicies.OnlyNonWebview)]
[ApiController]
[Route("api")]
public class DisplayController(
    IAPIMainDisplayService _service, 
    IRoomDisplayService _roomDisplayService, 
    IMapper _mapper,
    IBookingProcessService _processService
    )
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
        ReturnalModel ret = await _service.CheckSerialIsAlready(request.Serial);
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost("display/update-serial-sync")]
    public async Task<IActionResult> DisplayUpdateSerialSync(DisplayUpdateSerialSyncFRViewModel request)
    {

        ReturnalModel ret = await _roomDisplayService.DisplayUpdateSerialSync(request);
        return StatusCode(ret.StatusCode, ret);
    }


    [HttpPost("display-information/list")]
    public async Task<IActionResult> DisplayScheduled(DisplayScheduledFRViewModel request)
    {
        ReturnalModel ret = await _service.GetScheduledDisplay(request);
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

    [HttpPost("display/schedule/set-extendtime")]
    public async Task<IActionResult> SetExtendBookingDisplay(BookingVMExtendMeetingFR request)
    {
        ReturnalModel ret = await _processService.SetExtendMeetingAsync(request);

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost("display/schedule/get-extendtime")]
    public async Task<IActionResult> GetExtendBookingDisplay(BookingVMCheckExtendMeetingFR request)
    {
        ReturnalModel ret = await _processService.CheckExtendMeetingTimeAsync(request);

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost("display/schedule/post/end-meeting")]
    public async Task<IActionResult> SetExtendBookingDisplay(BookingVMEndMeetingFR request)
    {
        ReturnalModel ret = await _processService.EndMeetingAsync(request, true);

        return StatusCode(ret.StatusCode, ret);
    }

}