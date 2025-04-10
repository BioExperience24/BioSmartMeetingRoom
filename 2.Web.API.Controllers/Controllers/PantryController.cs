using _2.Web.API.Controllers;
using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _5.Helpers.Consumer.EnumType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;


[ApiController]
[ApiExplorerSettings(GroupName = "pama_smeet")]
[Route("api")]
public class PantryController(IAPIMainPantryService service, IPantryService pantryService, IPantryDetailService pantryDetailService)
    : ControllerBase
{
    [AccessIdAuthorize("2")]
    [HttpPost("display/pantry/listpantry")]
    public async Task<IActionResult> DisplayPantryListPantry()
    {
        ReturnalModel ret = new()
        {
            Collection = await pantryService.GetAllPantryAndImage(),
            Message = "Success get data to pantry "
        };
        return StatusCode(ret.StatusCode, ret);
    }

    // <summary>
    // Display Pantry Order Process API by logged in user
    // based on the request, this API will return the order process data from the pantry
    // 
    // </summary>
    [AccessIdAuthorize("2")]
    [HttpPost("display/pantry/orderentry")]
    public async Task<IActionResult> DisplayPantryOrderEntry(OrderByDateRequest request)
    {
        ReturnalModel ret = await service.DisplayPantryOrder(request, EnumPantryTransaksiOrderStatus.Entry);
        return StatusCode(ret.StatusCode, ret);
    }

    [AccessIdAuthorize("2")]
    [HttpPost("display/pantry/orderprocess")]
    public async Task<IActionResult> DisplayPantryOrderProcess(OrderByDateRequest request)
    {
        ReturnalModel ret = await service.DisplayPantryOrder(request, EnumPantryTransaksiOrderStatus.Process);
        return StatusCode(ret.StatusCode, ret);
    }

    [AccessIdAuthorize("2")]
    [HttpPost("display/pantry/ordercomplete")]
    public async Task<IActionResult> DisplayPantryOrderComplete(OrderByDateRequest request)
    {
        ReturnalModel ret = await service.DisplayPantryOrder(request);
        return StatusCode(ret.StatusCode, ret);
    }

    [AccessIdAuthorize("2")]
    [HttpPost("display/pantry/push/process")]
    public async Task<IActionResult> DisplayPantryPushProcess(PushOrderRequest request)
    {
        ReturnalModel ret = await service.DisplayPantryPush(request, EnumPantryTransaksiPushStatus.Process);
        return StatusCode(ret.StatusCode, ret);
    }

    [AccessIdAuthorize("2")]
    [HttpPost("display/pantry/push/complete")]
    public async Task<IActionResult> DisplayPantryPushComplete(PushOrderRequest request)
    {
        ReturnalModel ret = await service.DisplayPantryPush(request, EnumPantryTransaksiPushStatus.Complete);
        return StatusCode(ret.StatusCode, ret);
    }

    [AccessIdAuthorize("2")]
    [HttpPost("display/pantry/push/rejectorder")]
    public async Task<IActionResult> DisplayPantryPushReject(PushOrderRequest request)
    {
        ReturnalModel ret = await service.DisplayPantryPush(request, EnumPantryTransaksiPushStatus.Reject);
        return StatusCode(ret.StatusCode, ret);
    }

    [AccessIdAuthorize("2")]
    [HttpPost("display/pantry/push/remove")]
    public async Task<IActionResult> DisplayPantryPushRemove(PushOrderRequest request)
    {
        ReturnalModel ret = await service.SetIsTrashPantry(request);
        return StatusCode(ret.StatusCode, ret);
    }

    [Authorize]
    [HttpPost("pantry/place")]
    public async Task<IActionResult> MobileGetPlace(MobilePlaceRequest request)
    {
        var data = (await pantryService.GetAllPantryAndImage()).ToList();
        //set null to id, createby, createat, etc
        data.ForEach(x =>
        {
            x.Id = null!;
            x.employee_id = null!;
        });

        ReturnalModel ret = new()
        {
            Collection = data,
            Message = "Success get data to pantry place2"
        };
        return StatusCode(ret.StatusCode, ret);
    }

    [Authorize]
    [HttpPost("pantry/menu")]
    public async Task<IActionResult> MobileGetMenu(MobileMenuRequest request)
    {
        var data = (await pantryDetailService.GetByPantryId(request.PantryId.ToString())).ToList();
        data.ForEach(x =>
        {
            x.menu_id = x.Id!;
            x.menuId = x.Id!;
            x.Id = null!;
        });

        ReturnalModel ret = new()
        {
            Collection = data,
            Message = "Success get data to pantry place"
        };
        return StatusCode(ret.StatusCode, ret);
    }

    [Authorize]
    [HttpPost("pantry/menu/detail")]
    public async Task<IActionResult> MobileGetMenuDetail(MobileMenuDetailRequest request)
    {
        ReturnalModel ret = await service.GetPantryVariantAndVariantDetail(request);
        return StatusCode(ret.StatusCode, ret);
    }

    [Authorize]
    [HttpPost("pantry/submit-order")]
    public async Task<IActionResult> MobileSubmitOrder(MobileSubmitOrderRequest request)
    {
        ReturnalModel ret = await service.PostSubmitOrder(request);
        return StatusCode(ret.StatusCode, ret);
    }

    [Authorize]
    [HttpPost("pantry/detail-trs")]
    public async Task<IActionResult> MobileGetDetailOrder(MobileDetailOrderRequest request)
    {
        ReturnalModel ret = await service.GetPantryTransaksiDetailByTransaksiId(request);
        return StatusCode(ret.StatusCode, ret);
    }

    [AccessIdAuthorize("2")]
    [HttpPost("pantry/cancel-order")]
    public async Task<IActionResult> MobileCancelOrder(MobileCancelOrderRequest request)
    {
        var param = new PushOrderRequest()
        {
            TransaksiId = request.Id,
            NoteReject = request.Note
        };
        ReturnalModel ret = await service.DisplayPantryPush(param, EnumPantryTransaksiPushStatus.Canceled);
        return StatusCode(ret.StatusCode, ret);
    }

    [Authorize]
    [HttpPost("pantry/all")]
    public async Task<IActionResult> MobileGetHistory(MobileHistoryRequest request)
    {
        ReturnalModel ret = await service.GetAllTrsPantry(request);
        return StatusCode(ret.StatusCode, ret);
    }
}