using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer.EnumType;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace _1.PAMA.Razor.Views.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class KioskDisplayController(IMapper mapper, IKioskDisplayService service) 
        : ControllerBase
    {
        private readonly IMapper _mapper = mapper;
        private readonly IKioskDisplayService _service = service;

        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            ReturnalModel ret = new();
            ret.Message = "Get success";

            ret.Collection = await _service.GetAll();

            return StatusCode(ret.StatusCode, ret);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] KioskDisplayVMCreateFR request)
        {
            ReturnalModel ret = new();

            ret.Message = "Success create a kiosk/display";

            var viewModel = _mapper.Map<KioskDisplayViewModel>(request);

            ret.Collection = await _service.Create(viewModel);

            if (ret.Collection == null)
            {
                ret.Status = ReturnalType.Failed;
                ret.Message = "Failed create a kiosk/display";
            }

            return StatusCode(ret.StatusCode, ret);
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromForm] KioskDisplayVMUpdateFR request)
        {
            ReturnalModel ret = new();

            ret.Message = "Success update a kiosk/display";

            var viewModel = _mapper.Map<KioskDisplayViewModel>(request);

            var result = await _service.Update(viewModel);

            if (result == null)
            {
                ret.Status = ReturnalType.Failed;
                ret.Message = "Failed update a kiosk/display";
            }

            return StatusCode(ret.StatusCode, ret);
        }
    }
}