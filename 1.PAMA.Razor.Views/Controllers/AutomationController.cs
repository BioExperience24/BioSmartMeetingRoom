using System.Text.Json;
using _3.BusinessLogic.Services.Implementation;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer._Response;
using _7.Entities.Models;
using Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.MSIdentity.Shared;

namespace _1.PAMA.Razor.Views.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AutomationController(IRoomAutomationService service, IHttpContextAccessor httpContextAccessor) 
    : BaseLongController<RoomAutomationViewModel>(service)
    {
        private readonly _Json _jsonResponse = new(httpContextAccessor.HttpContext);

    }

}