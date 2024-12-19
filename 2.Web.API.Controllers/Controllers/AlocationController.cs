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
public class AlocationController(IAlocationService service, IHttpContextAccessor httpContextAccessor)
    : BaseController<AlocationViewModel>(service)
{
    private readonly HttpContext _httpContext = httpContextAccessor.HttpContext ?? throw new("HttpContext is not available.");
    private readonly _Json _jsonResponse = new(httpContextAccessor.HttpContext);

    [HttpGet]
    public async Task<ActionResult> GetItems()
    {
        var response = await service.GetItemsAsync();
        string status = "success";
        string message = "Get Success";
        
        /* if (response.Error != null)
        {
            status = "fail";
            message = "Get Failed";
        } */

        return _jsonResponse
                    .Set(status, message, response)
                    .Generate();
    }

    [HttpGet("{typeId}")]
    public async Task<ActionResult> GetItemsByType(string typeId)
    {
        
        var response = await service.GetItemsByTypeAsync(typeId);
            
        string status = "success";
        string message = "Get Success";
        
        // if (response.Error != null)
        // {
        //     status = "fail";
        //     message = "Get Failed";
        // }

        return _jsonResponse
                    .Set(status, message, response)
                    .Generate();
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromForm] AlocationVMCreateFR CAlocationReq)
    {
        var alocation = await service.CreateAsync(CAlocationReq);

        string status = "success";
        string message = $"Success create a department {CAlocationReq.Name}";
        if (alocation == null)
        {
            status = "fail";
            message = $"Failed create a department {CAlocationReq.Name}";
        }

        return _jsonResponse
                    .SetStatus(status)
                    .SetMessage(message)
                    .Generate();
    }

    [HttpPost]
    public async Task<IActionResult> Update([FromForm] AlocationVMUpdateFR UAlocationReq)
    {
        var alocation = await service.UpdateAsync(UAlocationReq);

        string status = "success";
        string message = $"Success update a department {UAlocationReq.Name}";
        if (alocation == null)
        {
            status = "fail";
            message = $"Failed update a department {UAlocationReq.Name}";
        }

        return _jsonResponse
                    .SetStatus(status)
                    .SetMessage(message)
                    .Generate();
    }

    [HttpPost]
    public async Task<IActionResult> Remove([FromForm] AlocationVMDefaultFR DAlocationReq)
    {
        var alocation = await service.DeleteAsync(DAlocationReq);

        string status = "success";
        string message = $"Success delete a department {DAlocationReq.Name}";
        if (alocation == null)
        {
            status = "fail";
            message = $"Failed delete a department {DAlocationReq.Name}";
        }

        return _jsonResponse
                    .SetStatus(status)
                    .SetMessage(message)
                    .Generate();
    }
}
