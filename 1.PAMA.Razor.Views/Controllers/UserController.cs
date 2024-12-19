using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer.EnumType;
using Microsoft.AspNetCore.Mvc;

namespace _1.PAMA.Razor.Views.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetItems()
        {
            ReturnalModel ret = new();

            ret.Collection = await _service.GetItemsAsync();

            return StatusCode(ret.StatusCode, ret);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetItemById(long id)
        {
            ReturnalModel ret = new();

            ret.Collection = await _service.GetById(id);

            if (ret.Collection == null)
            {
                ret.Status = ReturnalType.Failed;
                ret.Message = "Get failed";
            }


            return StatusCode(ret.StatusCode, ret);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] UserVMCreateFR request)
        {
            ReturnalModel ret = new();

            ret.Message = "Success create a user";
            ret.Collection = await _service.CreateAsync(request);

            if (ret.Collection == null)
            {
                ret.Status = ReturnalType.Failed;
                ret.Message = "Failed create a user";
            }

            return StatusCode(ret.StatusCode, ret);
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromForm] UserVMUpdateFR request)
        {
            ReturnalModel ret = new();

            ret.Message = "Success update a user";
            ret.Collection = await _service.UpdateAsync(request);

            if (ret.Collection == null)
            {
                ret.Status = ReturnalType.Failed;
                ret.Message = "Failed update a user";
            }

            return StatusCode(ret.StatusCode, ret);
        }

        [HttpPost]
        public async Task<IActionResult> Disable([FromForm] UserVMDisableFR request)
        {
            ReturnalModel ret = new();

            ret.Message = "Success disable a user";
            ret.Collection = await _service.DisableAsync(request);


            if (ret.Collection == null)
            {
                ret.Status = ReturnalType.Failed;
                ret.Message = "Failed disable a user";
            }

            return StatusCode(ret.StatusCode, ret);
        }

        [HttpPost]
        public async Task<IActionResult> Remove([FromForm] UserVMDeleteFR request)
        {
            ReturnalModel ret = new();

            ret.Message = "Success delete a user";
            ret.Collection = await _service.DeleteAsync(request);


            if (ret.Collection == null)
            {
                ret.Status = ReturnalType.Failed;
                ret.Message = "Failed delete a user";
            }

            return StatusCode(ret.StatusCode, ret);
        }
    }
}