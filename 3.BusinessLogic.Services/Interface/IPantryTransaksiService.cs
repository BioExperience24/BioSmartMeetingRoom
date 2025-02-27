

using _5.Helpers.Consumer.Custom;

namespace _3.BusinessLogic.Services.Interface;

public interface IPantryTransaksiService : IBaseService<PantryTransaksiViewModel>
{
    Task<List<PantryTransactionDetail>> GetPantryTransaction(DateTime? start = null, DateTime? end = null, long? pantryId = null, long? orderSt = null);
    Task<IEnumerable<PantryTransaksiStatusViewModel>> GetAllPantryTransaksiStatus();
    Task<IEnumerable<PantryTransaksiAndMenuViewModel>> GetPantryTransaksiDetailByTransaksiId(string transaksiId);
    Task<string> GetNextOrderNumber(long pantryId, DateTime? dateTime = null);
    Task CreatePantryOrderAsync(FastBookBookingViewModel databook, string id = "");
}