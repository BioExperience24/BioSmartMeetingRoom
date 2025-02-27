using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace _1.PAMA.Razor.Views.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ReportController(IReportService service) 
        : ControllerBase
    {
        private readonly IReportService _service = service;

        [HttpGet]
        public async Task<IActionResult> GetRoomUsageDataTables([FromQuery] BookingVMRoomUsageFR request)
        {
            ReturnalModel ret = new();

            var data = await _service.GetAllRoomUsageReportItemAsync(request);

            ret.Collection = new DataTableResponse {
                Draw = request.Draw,
                RecordsTotal = data.RecordsTotal,
                RecordsFiltered = data.RecordsFiltered,
                Data = data.Collection
            };

            return StatusCode(ret.StatusCode, ret);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrganizerUsageDataTables([FromQuery] EmployeeVMOrganizerFR request)
        {
            ReturnalModel ret = new();

            var data = await _service.GetAllOrganizerUsageReportItemAsync(request);

            ret.Collection = new DataTableResponse {
                Draw = request.Draw,
                RecordsTotal = data.RecordsTotal,
                RecordsFiltered = data.RecordsFiltered,
                Data = data.Collections
            };

            return StatusCode(ret.StatusCode, ret);
        }

        [HttpGet]
        public async Task<IActionResult> GetAttendeesDataTables([FromQuery] EmployeeVMOrganizerFR request)
        {
            ReturnalModel ret = new();

            var data = await _service.GetAllAttendeesReportItemAsync(request);

            ret.Collection = new DataTableResponse {
                Draw = request.Draw,
                RecordsTotal = data.RecordsTotal,
                RecordsFiltered = data.RecordsFiltered,
                Data = data.Collections
            };

            return StatusCode(ret.StatusCode, ret);
        }
    }
}