using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
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
public class DepartmentController(IDepartmentService service) : BaseController<DepartmentViewModel>(service)
{
    [HttpGet]
    public async Task<ActionResult> GetDepartmentAndCompany(string id)
    {
        var item = await service.GetDepartmentAndCompany(id);
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
}
