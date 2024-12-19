using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer.EnumType;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

/// <summary>
/// 
/// </summary>
/// <typeparam name="VM"></typeparam>
/// <remarks>
/// 
/// </remarks>
/// <param name="service"></param>
//[Authorize]
[Route("api/[controller]/[action]")]
[ApiController]
public class BaseController<VM>(IBaseService<VM> service) : ControllerBase
    where VM : BaseViewModel, new()
{

    /// <summary>
    /// Get All Entity
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public virtual async Task<ActionResult> GetAll()
    {
        ReturnalModel ret = new()
        {
            Collection = await service.GetAll()
        };
        return Ok(ret);
    }


    /// <summary>
    /// Get one Entity using ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public virtual async Task<ActionResult> GetById(string id)
    {
        var item = await service.GetById(id);
        ReturnalModel ret = new()
        {
            Collection = item
        };

        if (item == null)
        {
            ret.StatusCode = 400;
            ret.Title = ReturnalType.BadRequest;
            ret.Status = "ID not registered";
            ret.Message = $"id {id} is not found";
        }
        return StatusCode(ret.StatusCode, ret);
    }

    /// <summary>
    /// Insert Model
    /// </summary>
    /// <param name="Model"></param>
    [HttpPost]
    public virtual async Task<ActionResult> Post([FromForm] VM Model)
    {
        ReturnalModel ret = new()
        {
            Collection = await service.Create(Model)
        };
        return Ok(ret);
    }

    /// <summary>
    /// update Model
    /// </summary>
    /// <param name="Model"></param>
    [HttpPut]
    public virtual async Task<ActionResult> Put([FromForm] VM Model)
    {
        ReturnalModel ret = new()
        {
            Collection = await service.Update(Model)
        };
        return Ok(ret);
    }

    /// <summary>
    /// Update Set Status IsDeleted = True
    /// </summary>
    /// <param name="id"></param>
    [HttpDelete]
    public virtual async Task<ActionResult> Delete(string id)
    {
        ReturnalModel ret = new()
        {
            Collection = await service.SoftDelete(id)
        };
        return Ok(ret);
    }

    internal static void CleaningGarbage()
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
    }
}
