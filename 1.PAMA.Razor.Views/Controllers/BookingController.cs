using System.Text.Json;
using _3.BusinessLogic.Services.Implementation;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer._Common;
using _5.Helpers.Consumer._Response;
using _7.Entities.Models;
using Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _1.PAMA.Razor.Views.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BookingController(IBookingService service, IHttpContextAccessor httpContextAccessor) 
        : BaseLongController<BookingViewModel>(service)
    {
        private readonly _Json _jsonResponse = new(httpContextAccessor.HttpContext);

        [HttpGet("{year}")]
        public async Task<IActionResult> GetTransactionChartItems(int year)
        {
            ReturnalModel ret = new();

            ret.Message = "Get success";

            ret.Collection = await service.GetItemChartsAsync(year);

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
            ret.Collection = await service.GetItemOngoingAsync(sDate, eDate);

            // NOTE: Buat ketika data auth (session) sudah ada 
            // TODO: Kondisi jika session levelid-nya == 2
            // On progress
            
            // NOTE: Buat ketika data auth (session) sudah ada 
            // TODO: Kondisi jika session levelid-nya != 1 & 2
            // Error return

            return StatusCode(ret.StatusCode, ret);
        }

        [HttpGet]
        public async Task<ActionResult> GetDataByDate(DateTime start, DateTime end)
        {
            var userLevel = httpContextAccessor.HttpContext?.User.FindFirst("levelid-nya")?.Value;
            var nik = httpContextAccessor.HttpContext?.User.FindFirst("user-nya")?.Value;
            
            //sebelum ada autentikasi, pakai ini dulu
            var data = await service.GetDataBookingAsync(start, end);
            return _jsonResponse.Set("success", "Get success", data).Generate();

            //if (userLevel == "1")
            //{
            //    var data = await service.GetDataBookingAsync(start, end);
            //    return _jsonResponse.Set("success", "Get success", data).Generate();
            //}
            //else if (userLevel == "2" && nik != null)
            //{
            //    var data = await service.GetDataBookingByNikAsync(start, end, nik);
            //    return _jsonResponse.Set("success", "Get success", data).Generate();
            //}
            //else
            //{
            //    return _jsonResponse.Set("fail", "You don't have any access", new List<BookingViewModel>()).Generate();
            //}
        }
    }
}