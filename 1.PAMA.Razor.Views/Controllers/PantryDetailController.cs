using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
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
/// <param name="httpCont"></param>
//[Authorize]
[Route("api/[controller]/[action]")]
[ApiController]
public class PantryDetailController(IPantryDetailService service)
    : BaseLongController<PantryDetailViewModel>(service)
{

    [HttpGet]
    public async Task<IActionResult> GetByPantryId(string id)
    {
        if (long.TryParse(id, out long result))
        {
            var response = await service.GetByPantryId(id);
            ReturnalModel ret = new()
            {
                Collection = response
            };
            return StatusCode(ret.StatusCode, ret);
        }
        else
        {
            return BadRequest("Invalid ID format.");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreatePantry([FromForm] PantryDetailViewModel CReq)
    {
        var type = await service.Create(CReq);
        ReturnalModel ret = new()
        {
            Message = $"Success create a Pantry Detail {CReq.name}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed create a Pantry Detail {CReq.name}";
        }

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> UpdatePantry([FromForm] PantryDetailViewModel UReq)
    {
        var type = await service.Update(UReq);
        ReturnalModel ret = new()
        {
            Message = $"Success update a Pantry Detail {UReq.name}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed update a Pantry Detail {UReq.name}";
        }

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> DeletePantry([FromForm] PantryDetailViewModel DReq)
    {
        var type = await service.SoftDelete(DReq.Id.Value);
        ReturnalModel ret = new()
        {
            Message = $"Success delete a Pantry Detail {DReq.name}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed delete a Pantry Detail {DReq.name}";
        }

        return StatusCode(ret.StatusCode, ret);
    }

    [AllowAnonymous]
    [HttpGet("{id?}")]
    public virtual async Task<IActionResult> GetPantryDetailView(long id, int h = 80, bool noCache = false)
    {
        var result = await service.GetPantryDetailView(id, h);

        Response.Headers.Append("Content-Disposition", "inline; filename=" + result.FileName);
        if (noCache)
        {
            Response.Headers.Append("Cache-Control", "no-store, no-cache, must-revalidate, max-age=0");
            Response.Headers.Append("Pragma", "no-cache");
        }

        return File(result.FileStream, "image/PNG"); // Ganti "image/jpeg" dengan tipe MIME yang sesuai
    }
}
