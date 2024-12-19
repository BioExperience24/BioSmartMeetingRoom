using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer._Response;
using _5.Helpers.Consumer.EnumType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

/// <summary>
/// 
/// </summary>
/// <remarks>
/// 
/// </remarks>
/// <param name="service"></param>
//[Authorize]
[Route("api/[controller]/[action]")]
[ApiController]
public class PantryController(IPantryService service)
    : BaseLongController<PantryViewModel>(service)
{

    [HttpGet]
    public async Task<IActionResult> GetAllPantryAndImage()
    {
        var response = await service.GetAllPantryAndImage();
        ReturnalModel ret = new()
        {
            Collection = response
        };
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePantry([FromForm] PantryViewModel CReq)
    {
        var type = await service.CreatePantry(CReq);
        ReturnalModel ret = new()
        {
            Message = $"Success create a Pantry {CReq.name}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed create a Pantry {CReq.name}";
        }

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> PostUpdate([FromForm] PantryViewModel UReq)
    {
        var type = await service.Update(UReq);
        ReturnalModel ret = new()
        {
            Message = $"Success update a Pantry {UReq.name}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed update a Pantry {UReq.name}";
        }

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> PostDelete([FromForm] PantryViewModel DReq)
    {
        var type = await service.SoftDelete(DReq.Id.Value);
        ReturnalModel ret = new()
        {
            Message = $"Success delete a Pantry {DReq.name}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed delete a Pantry {DReq.name}";
        }

        return StatusCode(ret.StatusCode, ret);
    }

    [AllowAnonymous]
    [HttpGet("{id?}")]
    public virtual async Task<IActionResult> GetPantryView(long id, int h = 80, bool noCache = false)
    {
        var result = await service.GetPantryView(id, h);

        Response.Headers.Append("Content-Disposition", "inline; filename=" + result.FileName);
        if (noCache)
        {
            Response.Headers.Append("Cache-Control", "no-store, no-cache, must-revalidate, max-age=0");
            Response.Headers.Append("Pragma", "no-cache");
        }
        return File(result.FileStream, "image/PNG"); // Ganti "image/jpeg" dengan tipe MIME yang sesuai
    }
}
