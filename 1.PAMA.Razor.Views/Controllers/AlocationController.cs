using System.IO.Pipelines;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer._Response;
using _5.Helpers.Consumer.EnumType;
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
public class AlocationController(IAlocationService service)
    : BaseController<AlocationViewModel>(service)
{

    [HttpGet]
    public async Task<ActionResult> GetItems()
    {
        ReturnalModel ret = new();

        ret.Collection = await service.GetItemsAsync();
        
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpGet("{typeId}")]
    public async Task<ActionResult> GetItemsByType(string typeId)
    {
        ReturnalModel ret = new();

        ret.Collection = await service.GetItemsByTypeAsync(typeId);

        return StatusCode(ret.StatusCode, ret);
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromForm] AlocationVMCreateFR CAlocationReq)
    {
        ReturnalModel ret = new();

        ret.Message = $"Success create a department {CAlocationReq.Name}";
        ret.Collection = await service.CreateAsync(CAlocationReq);

        if (ret.Collection == null)
        {
            ret.Status = ReturnalType.Failed;
            ret.Message = $"Failed create a department {CAlocationReq.Name}";
        }

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> Update([FromForm] AlocationVMUpdateFR UAlocationReq)
    {
        ReturnalModel ret = new();

        ret.Message = $"Success update a department {UAlocationReq.Name}";
        ret.Collection = await service.UpdateAsync(UAlocationReq);

        if (ret.Collection == null)
        {
            ret.Status = ReturnalType.Failed;
            ret.Message = $"Failed update a department {UAlocationReq.Name}";
        }

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> Remove([FromForm] AlocationVMDefaultFR DAlocationReq)
    {
        ReturnalModel ret = new();

        ret.Message = $"Success delete a department {DAlocationReq.Name}";
        ret.Collection = await service.DeleteAsync(DAlocationReq);

        if (ret.Collection == null)
        {
            ret.Status = ReturnalType.Failed;
            ret.Message = $"Failed delete a department {DAlocationReq.Name}";
        }

        return StatusCode(ret.StatusCode, ret);
    }
}
