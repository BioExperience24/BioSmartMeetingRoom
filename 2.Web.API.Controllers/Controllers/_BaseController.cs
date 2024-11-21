using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer.EnumType;
using Microsoft.AspNetCore.Mvc;

namespace _1.API.Controllers.Controllers;

/// <summary>
/// 
/// </summary>
/// <typeparam name="VM"></typeparam>
//[Authorize]
[Route("api/[controller]/[action]")]
[ApiController]
public class BaseController<VM> : ControllerBase
    where VM : BaseViewModel, new()
{
    private readonly IBaseService<VM> _service;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="service"></param>
    public BaseController(IBaseService<VM> service)
    {
        _service = service;
    }

    /// <summary>
    /// Get All Entity
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public virtual async Task<ActionResult> GetAll()
    {
        ReturnalModel ret = new()
        {
            Data = await _service.GetAll()
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
        var item = await _service.GetById(id);
        ReturnalModel ret = new()
        {
            Data = item
        };

        if (item == null)
        {
            ret.Status = 400;
            ret.Title = ReturnalType.BadRequest;
            ret.Type = "ID not registered";
            ret.Detail = $"id {id} is not found";
        }
        return StatusCode(ret.Status, ret);
    }

    /// <summary>
    /// Insert Model
    /// </summary>
    /// <param name="Model"></param>
    [HttpPost]
    public virtual async Task<ActionResult> Post(VM Model)
    {
        ReturnalModel ret = new()
        {
            Data = await _service.Create(Model)
        };
        return Ok(ret);
    }

    /// <summary>
    /// update Model
    /// </summary>
    /// <param name="Model"></param>
    [HttpPut]
    public virtual async Task<ActionResult> Put(VM Model)
    {
        ReturnalModel ret = new()
        {
            Data = await _service.Update(Model)
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
            Data = await _service.SoftDelete(id)
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
