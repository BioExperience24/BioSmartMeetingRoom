namespace _3.BusinessLogic.Services.Interface;

public interface IAPIMainPantryService
{
    Task<ReturnalModel> DisplayPantryOrder(OrderByDateRequest request, int? orderSt = null);
    Task<ReturnalModel> DisplayPantryPush(PushOrderRequest request, int status);
    Task<ReturnalModel> GetPantryTransaksiDetailByTransaksiId(MobileDetailOrderRequest request);
    Task<ReturnalModel> GetPantryVariantAndVariantDetail(MobileMenuDetailRequest request);
    Task<ReturnalModel> SetIsTrashPantry(PushOrderRequest request);
    Task<ReturnalModel> GetAllTrsPantry(MobileHistoryRequest request);
    Task<ReturnalModel> PostSubmitOrder(MobileSubmitOrderRequest request);
}
