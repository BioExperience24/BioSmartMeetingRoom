using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class PantryTransaksiController(IPantryTransaksiService service)
    : BaseController<PantryTransaksiViewModel>(service)
{
    [HttpGet]
    public async Task<IActionResult> GetPantryTransaction(DateTime? start = null, DateTime? end = null, long? pantryId = null, long? orderSt = null)
    {
        var response = await service.GetPantryTransaction(start, end, pantryId, orderSt);
        ReturnalModel ret = new()
        {
            Collection = response
        };
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPantryTransaksiStatus()
    {
        var response = await service.GetAllPantryTransaksiStatus();
        ReturnalModel ret = new()
        {
            Collection = response
        };
        return StatusCode(ret.StatusCode, ret);
    }
}