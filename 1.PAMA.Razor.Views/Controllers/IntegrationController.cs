using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer.EnumType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _1.PAMA.Razor.Views.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class IntegrationController : ControllerBase
{
    private readonly IIntegrationService _service;

    public IntegrationController(IIntegrationService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetIntegrationData()
    {
        ReturnalModel ret = await _service.GetIntegrationData();

        return StatusCode(ret.StatusCode, ret);
    }


}