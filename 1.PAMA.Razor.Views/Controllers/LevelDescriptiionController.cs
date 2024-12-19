using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer._Response;
using Microsoft.AspNetCore.Mvc;

namespace _1.PAMA.Razor.Views.Controllers
{
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