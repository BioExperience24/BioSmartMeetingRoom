using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer.EnumType;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

/// <summary>
/// 
/// </summary>
/// <typeparam name="VM"></typeparam>
//[Authorize]
[Route("api/[controller]/[action]")]
[ApiController]
public class BaseControllerId<VM> : ControllerBase
    where VM : BaseViewModelId, new()
{
    private readonly IBaseServiceId<VM> _service;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="service"></param>
    public BaseControllerId(IBaseServiceId<VM> service)
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
            Collection = await _service.GetAll()
        };
        return Ok(ret);
    }


    /// <summary>
    /// Get one Entity using ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public virtual async Task<ActionResult> GetById(long id)
    {
        var item = await _service.GetById(id);
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
    public virtual async Task<ActionResult> Post(VM Model)
    {
        ReturnalModel ret = new()
        {
            Collection = await _service.Create(Model)
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
            Collection = await _service.Update(Model)
        };
        return Ok(ret);
    }

    /// <summary>
    /// Update Set Status IsDeleted = True
    /// </summary>
    /// <param name="id"></param>
    [HttpDelete]
    public virtual async Task<ActionResult> Delete(long id)
    {
        ReturnalModel ret = new()
        {
            Collection = await _service.SoftDelete(id)
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
