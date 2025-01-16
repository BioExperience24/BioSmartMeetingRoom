using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer._Common;
using Microsoft.AspNetCore.Mvc;

namespace _1.PAMA.Razor.Views.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ModuleBackendController(
        IModuleBackendService service
    ) 
        : ControllerBase
    {
        private readonly IModuleBackendService _service = service;

        [HttpGet]
        public async Task<IActionResult> GetItems([FromQuery] ModuleBackendVMList request)
        {
            ReturnalModel ret = new();

            // var moduleText = request.ModuleText.Trim().Split(",");
            var moduleText = _String.RemoveAllSpace(request.ModuleText).Split(",");

            var modules = await _service.GetItemsByModuleTextAsync(moduleText);

            var dictModules = new Dictionary<string, object>();

            foreach (var item in modules)
            {
                dictModules.Add(_Dictionary.ModuleAliasDictionary[item.ModuleText], item);
            }

            ret.Collection = dictModules;

            return StatusCode(ret.StatusCode, ret);
        }
    }
}