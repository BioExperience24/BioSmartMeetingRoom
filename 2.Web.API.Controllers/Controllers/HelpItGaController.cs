using System.Text.Json;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _2.Web.API.Controllers.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "pama_smeet")]
    [Route("api")]
    public class HelpItGaController(IHelpItGaService service) : ControllerBase
    {
        [Authorize]
        [HttpPost("help/request")]
        public async Task<IActionResult> SubmitRequest(HelpItGaVMRequest request)
        {
            ReturnalModel ret = await service.SubmitRequestAsync(request);

            return StatusCode(ret.StatusCode, ret);
        }

        [Authorize]
        [HttpPost("help/list")]
        public async Task<IActionResult> ListRequest(HelpItGaVMFilterList request)
        {
            ReturnalModel ret = await service.ListRequestAsync(request);

            return StatusCode(ret.StatusCode, ret);
        }
    }
}