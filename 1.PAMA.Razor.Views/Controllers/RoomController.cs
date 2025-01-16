using System.Text.Json;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer.EnumType;
using Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _1.PAMA.Razor.Views.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RoomController(IRoomService service, IDashboardService dashboardService) 
        : BaseLongController<RoomViewModel>(service)
    {
        private readonly IDashboardService _dashboardService = dashboardService;
        
        [HttpGet("{year}")]
        public async Task<IActionResult> GetChartTopRoom(int year)
        {
            ReturnalModel ret = new();

            ret.Message = "Get success";
            ret.Collection = await _dashboardService.GetAllChartTopFiveRoomAsync(year);

            return StatusCode(ret.StatusCode, ret);
        }

        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            ReturnalModel ret = new();
            ret.Message = "Get success";

            ret.Collection = await service.GetAllRoomItemAsync(false);

            return StatusCode(ret.StatusCode, ret);
        }

        [HttpGet]
        public async Task<IActionResult> GetItemsWithRoomDisplays()
        {
            ReturnalModel ret = new();
            ret.Message = "Get success";

            ret.Collection = await service.GetAllRoomRoomDisplayItemAsync();

            return StatusCode(ret.StatusCode, ret);
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailableItems([FromQuery] RoomVMFindAvailable request)
        {
            ReturnalModel ret = new();

            ret.Message = "Get success";
            ret.Collection = await service.GetAllRoomAvailableAsync(request);

            return StatusCode(ret.StatusCode, ret);
        }

        [HttpGet]
        public async Task<ActionResult> GetRoomData()
        {
            ReturnalModel ret = new()
            {
                Collection = await service.GetRoomData()
            };
            return Ok(ret);
        }

        [HttpGet]
        public async Task<ActionResult> GetSingle()
        {
            ReturnalModel ret = new()
            {
                Collection = await service.GetSingleRoomData()
            };
            return Ok(ret);
        }

        [HttpGet]
        public async Task<ActionResult> GetOnLoadModules(string pagename)
        {
            ReturnalModel ret = new()
            {
                Collection = await service.GetRoomDetailsAsync(pagename)
            };
            return Ok(ret);
        }

        [HttpGet]
        public async Task<ActionResult> GetInvoiceStatus()
        {
            ReturnalModel ret = new()
            {
                Collection = await service.GetInvoiceStatus()
            };
            return Ok(ret);
        }

        //[HttpPost]
        //public async Task<ActionResult> GetReportusage(SearchCriteriaViewModel viewModel)
        //{
        //    ReturnalModel ret = new()
        //    {
        //        Collection = await service.GetReportusage(viewModel)
        //    };
        //    return Ok(ret);
        //}

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] RoomVMDefaultFR request)
        {
            ReturnalModel ret = new();

            // Call the service to create the entry
            ret.Collection = await service.CreateRoom(request);
            ret.Message = "Successfully created a room";

            return StatusCode(ret.StatusCode, ret);
        }


        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<ReturnalModel> UpdateRoom( long id, [FromForm] RoomVMUpdateFRViewModel request)
        {


            ReturnalModel ret = new();

            // Call the service to create the entry
            ret.Collection = await service.UpdateRoom(request, id);
            ret.Message = "Successfully update a room";

            return ret;
        }


        [HttpDelete]
        public async Task<ReturnalModel> DeleteItemsByIds([FromQuery] string ids)
        {
            ReturnalModel ret = new();
            if (string.IsNullOrEmpty(ids))
            {
                ret.Message = "No IDs provided";
                ret.Status = ReturnalType.BadRequest;
            }

            var idList = ids.Split(',').Select(id => int.Parse(id)).ToList();

            foreach(var id in idList)
            {
                await service.SoftDelete(id);
            }

            return ret;
        }




        [AllowAnonymous]
        [HttpGet("{id?}")]
        public virtual async Task<IActionResult> GetRoomDetailView(string id, int h = 80, bool noCache = false)
        {
            var result = await service.GetRoomDetailView(id, h);

            Response.Headers.Append("Content-Disposition", "inline; filename=" + result.FileName);
            if (noCache)
            {
                Response.Headers.Append("Cache-Control", "no-store, no-cache, must-revalidate, max-age=0");
                Response.Headers.Append("Pragma", "no-cache");
            }

            return File(result.FileStream, "image/PNG"); // Ganti "image/jpeg" dengan tipe MIME yang sesuai
        }

        [HttpGet]
        public virtual async Task<ReturnalModel> GetAllRoomMerge()
        {
            ReturnalModel ret = new();

            // Call the service to create the entry
            ret.Collection = await service.GetAllRoomMerge();
            ret.Message = "Successfully get room merge";

            return ret;
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetRoomMerge(long id)
        {
            ReturnalModel ret = new();

            // Call the service to create the entry
            ret.Collection = await service.GetRoomMerge(id);
            ret.Message = "Successfully get room merge";

            return StatusCode(ret.StatusCode, ret);
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetRoomById(long id)
        {
            ReturnalModel ret = new();

            // Call the service to create the entry
            ret.Collection = await service.GetRoomById(id);
            ret.Message = "Successfully get room";

            return StatusCode(ret.StatusCode, ret);
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetConfigRoomForUsageByIdRoom(long id)
        {
            ReturnalModel ret = new();

            // Call the service to create the entry
            ret.Collection = await service.GetConfigRoomForUsageByIdRoom(id);
            ret.Message = "Successfully get room for usage";

            return StatusCode(ret.StatusCode, ret);
        }

    }

}