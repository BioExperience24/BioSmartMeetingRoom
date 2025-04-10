using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using _5.Helpers.Consumer.Policy;

namespace _1.PAMA.Razor.Views.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = AuthorizationWebviewPolicies.OnlyNonWebview)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LevelDescriptiionController : ControllerBase
    {
        private readonly ILevelDescriptiionService _service;

        public LevelDescriptiionController(ILevelDescriptiionService service)
        {
            _service = service;
        }

        [HttpGet("{levelId}")]
        public async Task<ActionResult> GetItemsByLevelId(int levelId)
        {
            ReturnalModel ret = new();

            ret.Collection = await _service.GetItemByLevelIdAsync(levelId);

            return StatusCode(ret.StatusCode, ret);
        }
    }
}