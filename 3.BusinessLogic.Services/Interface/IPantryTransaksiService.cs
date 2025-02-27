

namespace _3.BusinessLogic.Services.Interface;

public interface IPantryTransaksiService : IBaseService<PantryTransaksiViewModel>
{
    Task<List<PantryTransactionDetail>> GetPantryTransaction(DateTime? start = null, DateTime? end = null, long? pantryId = null, long? orderSt = null);
    Task<IEnumerable<PantryTransaksiStatusViewModel>> GetAllPantryTransaksiStatus();
}