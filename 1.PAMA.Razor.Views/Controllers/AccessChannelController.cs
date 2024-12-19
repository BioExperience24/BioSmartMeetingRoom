using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace _1.PAMA.Razor.Views.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccessChannelController : ControllerBase
    {
        private readonly IAccessChannelService _service;

        public AccessChannelController(IAccessChannelService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            ReturnalModel ret = new();

            ret.Message = "Get success";

            ret.Collection = await _service.GetAll();

            return StatusCode(ret.StatusCode, ret);
        }
    }
}