using System.Text.Json;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer._Common;
using _5.Helpers.Consumer.EnumType;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _1.PAMA.Razor.Views.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _service;

        private readonly IBookingProcessService _processService;

        private readonly IDashboardService _dashboardService;

        public BookingController(IBookingService service, IBookingProcessService processService, IDashboardService dashboardService)
        {
            _service = service;
            _processService = processService;
            _dashboardService = dashboardService;
        }

        [HttpGet("{year}")]
        public async Task<IActionResult> GetTransactionChartItems(int year)
        {
            ReturnalModel ret = new();

            ret.Message = "Get success";

            // ret.Collection = await _service.GetItemChartsAsync(year);
            ret.Collection = await _dashboardService.GetAllChartBookingAsync(year);

            return StatusCode(ret.StatusCode, ret);
        }

        [HttpGet("{startDate}/{endDate}")]
        public async Task<IActionResult> GetOngoingItems(string startDate, string endDate)
        {
            DateOnly sDate = _String.ToDateOnly(startDate);
            DateOnly eDate = _String.ToDateOnly(endDate);
            
            ReturnalModel ret = new();
            
            ret.Message = "Get success";
            

            // Kondisi jika session levelid-nya == 1
            // ret.Collection = await _service.GetItemOngoingAsync(sDate, eDate);
            ret.Collection = await _dashboardService.GetAllOngoingBookingAsync(sDate, eDate);

            // NOTE: Buat ketika data auth (session) sudah ada 
            // TODO: Kondisi jika session levelid-nya == 2
            // On progress
            
            // NOTE: Buat ketika data auth (session) sudah ada 
            // TODO: Kondisi jika session levelid-nya != 1 & 2
            // Error return

            return StatusCode(ret.StatusCode, ret);
        }

        [HttpPost]
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

        [HttpGet]
        public async Task<IActionResult> GetDataTables([FromQuery] BookingVMDataTableFR request)
        {
            ReturnalModel ret = new();

            var (collection, recordsTotal, recordsFiltered) = await _processService.GetAllItemDataTablesAsync(request);

            ret.Collection = new DataTableResponse {
                Draw = request.Draw,
                RecordsTotal = recordsTotal,
                RecordsFiltered = recordsFiltered,
                Data = collection
            };

            return StatusCode(ret.StatusCode, ret);
        }

        [HttpGet("{bookingId}")]
        public async Task<IActionResult> GetPicByBookingId(string bookingId)
        {
            ReturnalModel ret = new();

            if (string.IsNullOrEmpty(bookingId))
            {
                ret.Status = ReturnalType.Failed;
                ret.Message = "BookingId is required";
                return StatusCode(ret.StatusCode, ret);
            }

            var pic = await _processService.GetPicFilteredByBookingIdAsync(bookingId);

            ret.Collection = pic;

            if (pic == null)
            {
                ret.Status = ReturnalType.Failed;
                ret.Message = "Data not found";
                ret.Collection = null;
            }

            return StatusCode(ret.StatusCode, ret);
        }

        [HttpGet("{bookingId}/{date}/{roomId}")]
        public async Task<IActionResult> CheckRescheduleDate(string bookingId, DateOnly date, string roomId)
        {
            ReturnalModel ret = await _processService.CheckRescheduleDateAsync(bookingId, date, roomId);
            
            return StatusCode(ret.StatusCode, ret);
        }

        [HttpPost]
        public async Task<IActionResult> RescheduleBooking([FromForm] BookingVMRescheduleFR request)
        {
            ReturnalModel ret = await _processService.RescheduleBookingAsync(request);

            return StatusCode(ret.StatusCode, ret);
        }

        [HttpPost]
        public async Task<IActionResult> CancelBooking([FromForm] BookingVMCancelFR request)
        {
            ReturnalModel ret = await _processService.CancelBookingAsync(request);
            
            return StatusCode(ret.StatusCode, ret);
        }

        [HttpPost]
        public async Task<IActionResult> EndMeeting([FromForm] BookingVMEndMeetingFR request)
        {
            ReturnalModel ret = await _processService.EndMeetingAsync(request);

            return StatusCode(ret.StatusCode, ret);
        }

        [HttpGet]
        public async Task<IActionResult> CheckExtendMeetingTime([FromQuery] BookingVMCheckExtendMeetingFR request)
        {
            ReturnalModel ret = await _processService.CheckExtendMeetingTimeAsync(request);

            return StatusCode(ret.StatusCode, ret);
        }

        [HttpPost]
        public async Task<IActionResult> SetExtendMeeting([FromForm] BookingVMExtendMeetingFR request)
        {
            ReturnalModel ret = await _processService.SetExtendMeetingAsync(request);

            return StatusCode(ret.StatusCode, ret);
        }
    }
}