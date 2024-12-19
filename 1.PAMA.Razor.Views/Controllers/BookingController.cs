using System.Text.Json;
using _3.BusinessLogic.Services.Implementation;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
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