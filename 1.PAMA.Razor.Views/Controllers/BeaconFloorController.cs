using System.Text.Json;
using _2.BusinessLogic.Services.Interface;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer._Common;
using _5.Helpers.Consumer.EnumType;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace _1.PAMA.Razor.Views.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BeaconFloorController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBeaconFloorService _service;

        public BeaconFloorController(IMapper mapper, IBeaconFloorService service)
        {
            _mapper = mapper;
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            ReturnalModel ret = new();
            ret.Message = "Get success";

            var results = await _service.GetAllItemAsync();
            ret.Collection = results;

            return StatusCode(ret.StatusCode, ret);
        }

        [HttpPost]
        public async Task<IActionResult> GetItemById([FromForm] BeaconFloorVMDetailFR request)
        {
            ReturnalModel ret = new();
            ret.Message = "Get success";

            var result = await _service.GetItemByIdAsync(request.Id);

            if (result == null)
            {
                ret.Status = ReturnalType.Failed;
                ret.Message = "Floor not exist, please try refresh page again!";
            }

            ret.Collection = result;

            return StatusCode(ret.StatusCode, ret);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] BeaconFloorVMDefaultFR request)
        {
            ReturnalModel ret = new();
            
            var viewModel = _mapper.Map<BeaconFloorViewModel>(request);

            var (fileName, errMsg) = await _service.DoUploadAsync(request.FileImage);
            if (errMsg != null)
            {
                ret.Status = ReturnalType.Failed;
                // ret.Message = "Failed to upload file";
                ret.Message = errMsg;
                return StatusCode(ret.StatusCode, ret);
            }

            if (fileName != null)
            {
                viewModel.Image = fileName;
            }

            ret.Collection = await _service.Create(viewModel);
            ret.Message = "Success create a beacon floor";

            return StatusCode(ret.StatusCode, ret);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromForm] BeaconFloorVMUpdateFR request)
        {
            ReturnalModel ret = new();

            var viewModel = _mapper.Map<BeaconFloorViewModel>(request);

            var (fileName, errMsg) = await _service.DoUploadAsync(request.FileImage);

            if (errMsg != null)
            {
                ret.Status = ReturnalType.Failed;
                // ret.Message = "Failed to upload file";
                ret.Message = errMsg;
                return StatusCode(ret.StatusCode, ret);
            }

            if (fileName != null)
            {
                viewModel.Image = fileName;
            }
            
            var result = await _service.Update(viewModel);
            if (result == null)
            {
                ret.Status = ReturnalType.Failed;
                ret.Message = "Failed update a beacon floor";
                return StatusCode(ret.StatusCode, ret);
            }
            ret.Message = "Success update a beacon floor";

            return StatusCode(ret.StatusCode, ret);
        }

        [HttpPost]
        public async Task<IActionResult> Remove([FromForm] BeaconFloorVMDetailFR request)
        {
            ReturnalModel ret = new();

            var result = await _service.SoftDelete(request.Id);
            if (result == null)
            {
                ret.Status = ReturnalType.Failed;
                ret.Message = "Failed delete a beacon floor";
                return StatusCode(ret.StatusCode, ret);
            }
            
            ret.Message = "Success delete a beacon floor";

            return StatusCode(ret.StatusCode, ret);
        }
    }

}