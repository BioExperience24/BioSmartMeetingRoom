using System.Text;
using System.Text.Json;
using _2.BusinessLogic.Services.Interface;
using _3.BusinessLogic.Services.Implementation;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer._Common;
using _5.Helpers.Consumer._Response;
using _5.Helpers.Consumer.EnumType;
using _7.Entities.Models;
using AutoMapper;
using Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.MSIdentity.Shared;

namespace _1.PAMA.Razor.Views.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RoomController(IRoomService service, IHttpContextAccessor httpContextAccessor, IMapper _mapper, IAttachmentListService _attachmentListService) 
    : BaseLongController<RoomViewModel>(service)
    {
        private readonly _Json _jsonResponse = new(httpContextAccessor.HttpContext);

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
        public async Task<IActionResult> DeleteItemsByIds([FromQuery] string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return BadRequest("No IDs provided.");
            }

            var idList = ids.Split(',').Select(id => int.Parse(id)).ToList();

            foreach(var id in idList)
            {
                await service.SoftDelete(id);
            }

            return Ok("Items deleted successfully.");
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