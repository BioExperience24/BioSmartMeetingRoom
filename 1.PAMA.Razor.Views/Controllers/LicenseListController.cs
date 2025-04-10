using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer.EnumType;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using _5.Helpers.Consumer.Policy;

namespace Controllers;

/// <summary>
/// Controller for managing LicenseList settings.
/// </summary>
/// <remarks>
/// Provides endpoints for CRUD operations on LicenseList settings.
/// </remarks>
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = AuthorizationWebviewPolicies.OnlyNonWebview)]
[Route("api/[controller]/[action]")]
[ApiController]
public class LicenseListController(ILicenseListService service)
    : BaseController<LicenseListViewModel>(service)
{
    [HttpGet]
    public async Task<IActionResult> GetAllLicenseLists()
    {
        var response = await service.GetAllLicenseListsAsync();
        ReturnalModel ret = new()
        {
            Collection = response?.Data
        };
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpGet]
    public async Task<IActionResult> GetLicenseList()
    {
        var response = await service.GetAllLicenseListsAsync();
        ReturnalModel ret = new()
        {
            Collection = response?.Data
        };
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpGet]
    public async Task<IActionResult> GetLicenseListMenu(string id)
    {
        var response = await service.GetAllLicenseListsAsync();
        ReturnalModel ret = new()
        {
            Collection = response?.Data
        };
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] LicenseListCreateViewModelFR CReq)
    {
        var type = await service.CreateLicenseListAsync(CReq);
        ReturnalModel ret = new()
        {
            Message = $"Success create a LicenseList {CReq.Name}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed create a LicenseList {CReq.Name}";
        }

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> Update([FromForm] LicenseListUpdateViewModelFR UReq)
    {
        var type = await service.UpdateLicenseListAsync(UReq);
        ReturnalModel ret = new()
        {
            Message = $"Success update a LicenseList {UReq.Name}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed update a LicenseList {UReq.Name}";
        }

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromForm] LicenseListDeleteViewModelFR DReq)
    {
        var type = await service.DeleteLicenseListAsync(DReq);
        ReturnalModel ret = new()
        {
            Message = $"Success delete a LicenseList {DReq.Name}"
        };

        if (type == null)
        {
            ret.StatusCode = 400;
            ret.Status = ReturnalType.Failed;
            ret.Title = ReturnalType.Failed;
            ret.Message = $"Failed delete a LicenseList {DReq.Name}";
        }

        return StatusCode(ret.StatusCode, ret);
    }
}