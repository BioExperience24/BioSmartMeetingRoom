using System.Text.Json;
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
    public class HelpController : ControllerBase
    {
        private readonly IHelpItGaService _service;
        public HelpController(IHelpItGaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetDataTables([FromQuery] HelpItGaVMDataTable request)
        {
            ReturnalModel ret = new();

            ret.Collection = await _service.GetDataTablesAsync(request);

            return StatusCode(ret.StatusCode, ret);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatus([FromForm] HelpItGaVMChangeStatus request)
        {
            ReturnalModel ret = await _service.ChangeStatusAsync(request);

            return StatusCode(ret.StatusCode, ret);
        }
    }
}