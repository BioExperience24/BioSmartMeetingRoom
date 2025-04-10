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
    [Route("api/[controller]/[action]")]
    [ApiController]
    // public class LevelController(ILevelService service, IHttpContextAccessor httpContextAccessor)
    // : BaseController<LevelViewModel>(service)
    public class LevelController : ControllerBase
    {

        private readonly ILevelService _service;

        private readonly IMapper _mapper;

        public LevelController(ILevelService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            ReturnalModel ret = new();

            ret.Collection = await _service.GetAll();

            return StatusCode(ret.StatusCode, ret);            
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromForm] LevelVMUpdateFR request)
        {
            ReturnalModel ret = new();
            ret.Message = "Success update a group";

            var viewModel = _mapper.Map<LevelViewModel>(request);

            var result = await _service.Update(viewModel);

            if (result == null)
            {
                ret.Status = ReturnalType.Failed;
                ret.Message = "Failed update a group";
            }

            return StatusCode(ret.StatusCode, ret);
        }
    }
}