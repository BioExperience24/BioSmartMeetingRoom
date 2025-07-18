using System.Text.Json;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using _5.Helpers.Consumer.Policy;

namespace Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = AuthorizationWebviewPolicies.OnlyNonWebview)]
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

    [HttpGet]
    public async Task<IActionResult> GetPantryTransactionWithApprovalDataTables([FromQuery] PantryTransaksiVMNeedApprovalDataTableFR request)
    {
        ReturnalModel ret = new();

        ret.Collection = await service.GetAllItemWithApprovalDataTablesAsync(request);

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> ProcessOrderApproval([FromForm] PantryTransaksiVMProcessApproval request)
    {
        ReturnalModel ret = await service.ProcessOrderApprovalAsync(request);

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> ProcessOrderApprovalHead([FromForm] PantryTransaksiVMProcessApproval request)
    {
        ReturnalModel ret = await service.ProcessOrderApprovalHeadAsync(request);

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpGet("{pantryTransaksiId}")]
    public async Task<IActionResult> GetPrintOrderApproval(string pantryTransaksiId)
    {
        ReturnalModel ret = await service.PrintOrderApprovakAsycn(pantryTransaksiId);

        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost]
    public async Task<IActionResult> ProcessCancelOrder([FromForm] PantryTransaksiVMProcessCancel request)
    {
        ReturnalModel ret = await service.ProcessCancelOrderAsync(request);

        return StatusCode(ret.StatusCode, ret);
    }
}