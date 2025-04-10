using _2.Web.API.Controllers;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controllers.Webview;

[ApiController]
[ApiExplorerSettings(GroupName = "webview")]
[Route("api/webview/pantry-transaksi")]
[AccessIdAuthorize("2")]
[Authorize(Policy = AuthorizationWebviewPolicies.OnlyWebview)]
public class PantryTransaksiController(
    IPantryTransaksiService service
) : ControllerBase
{
    [HttpGet("get-pantry-transactions")]
    public async Task<IActionResult> GetPantryTransaction(DateTime? start = null, DateTime? end = null, long? pantryId = null, long? orderSt = null)
    {
        var response = await service.GetPantryTransaction(start, end, pantryId, orderSt);
        ReturnalModel ret = new()
        {
            Collection = response
        };
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpGet("print-order-approval/{pantryTransaksiId}")]
    public async Task<IActionResult> GetPrintOrderApproval(string pantryTransaksiId)
    {
        ReturnalModel ret = await service.PrintOrderApprovakAsycn(pantryTransaksiId);
        return StatusCode(ret.StatusCode, ret);
    }

    [HttpPost("cancel-pantry-order")]
    public async Task<IActionResult> ProcessCancelOrder([FromForm] PantryTransaksiVMProcessCancel request)
    {
        ReturnalModel ret = await service.ProcessCancelOrderAsync(request);
        return StatusCode(ret.StatusCode, ret);
    }
}
