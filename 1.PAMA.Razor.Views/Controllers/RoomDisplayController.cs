using System.Text.Json;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer.EnumType;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using _5.Helpers.Consumer.Policy;

namespace _1.PAMA.Razor.Views.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = AuthorizationWebviewPolicies.OnlyNonWebview)]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RoomDisplayController(IMapper mapper, IRoomDisplayService service) 
        : ControllerBase
    {
        private readonly IMapper _mapper = mapper;
        private readonly IRoomDisplayService _service = service;

        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            ReturnalModel ret = new();
            ret.Message = "Get success";

            ret.Collection = await _service.GetAllItemAsync();

            return StatusCode(ret.StatusCode, ret);
        }
        
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Save([FromForm] RoomDisplayVMCreateFR request)
        {
            ReturnalModel ret = await _service.SaveAsync(request);

            return StatusCode(ret.StatusCode, ret);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromForm] RoomDisplayVMUpdateFR request)
        {
            ReturnalModel ret = new();

            ret.Message = "Success update a display";
            
            var viewModel = _mapper.Map<RoomDisplayViewModel>(request);

            if (request.FileBackground != null)
            {
                var (fileName, errMsg) = await _service.DoUploadAsync(request.FileBackground);

                if (errMsg != null)
                {
                    ret.Status = ReturnalType.Failed;
                    ret.Message = errMsg;
                    return StatusCode(ret.StatusCode, ret);
                }


                if (fileName != null)
                {
                    viewModel.Background = fileName;
                }
            }

            if (request.RoomSelectArr.Any())
            {
                viewModel.RoomSelect = String.Join(",", request.RoomSelectArr);
            }

            var result = await _service.Update(viewModel);

            if (result == null)
            {
                ret.Status = ReturnalType.Failed;
                ret.Message = "Failed update a display";
            }

            return StatusCode(ret.StatusCode, ret);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatusDisplay([FromForm] RoomDisplayVMChangeStatusFR request)
        {
            ReturnalModel ret = new();

            ret.Message = "Success update a display";

            var viewModel = _mapper.Map<RoomDisplayViewModel>(request);

            viewModel.Enabled = request.Action;
            viewModel.DisableMsg = request.DisableMsg;

            var result = await _service.ChangeStatusEnabledAsync(viewModel);

            if (result == null)
            {
                ret.Status = ReturnalType.Failed;
                ret.Message = "Failed update a display";
            }
            
            return StatusCode(ret.StatusCode, ret);
        }

        [HttpPost]
        public async Task<IActionResult> Remove([FromForm] RoomDisplayVMDeleteFR request)
        {
            ReturnalModel ret = new();

            ret.Message = "Success delete a display";

            var result = await _service.SoftDelete(request.Id);

            if (result == null)
            {
                ret.Status = ReturnalType.Failed;
                ret.Message = "Failed delete a display";
            }

            return StatusCode(ret.StatusCode, ret);
        }
    }
}