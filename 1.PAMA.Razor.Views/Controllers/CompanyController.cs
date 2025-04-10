using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer.EnumType;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using _5.Helpers.Consumer.Policy;

namespace _1.PAMA.Razor.Views.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = AuthorizationWebviewPolicies.OnlyNonWebview)]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _service;

        public CompanyController(ICompanyService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromForm] CompanyViewModel request)
        {
            ReturnalModel ret = new() {};

            ret.Status = ReturnalType.Failed;
            ret.Message = "Failed update a company"; 

            var result = await _service.Update(request);

            if (result != null)
            {
                ret.Status = ReturnalType.Success;
                ret.Message = "Success update a company"; 
            }

            return StatusCode(ret.StatusCode, ret);
        }


        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadMedia([FromForm] CompanyVMMediaFR request)
        {   
            var result = await _service.UploadMediaAsync(request);
            
            return StatusCode(result.StatusCode, result);
        }
    }
}