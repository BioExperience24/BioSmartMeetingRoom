namespace _3.BusinessLogic.Services.Implementation;

public class PantryTransaksiService(PantryTransaksiRepository repo, IMapper mapper)
    : BaseService<PantryTransaksiViewModel, PantryTransaksi>(repo, mapper), IPantryTransaksiService
{
    public async Task<List<PantryTransactionDetail>> GetPantryTransaction(DateTime? start = null, DateTime? end = null, long? pantryId = null, long? orderSt = null)
    {
        var entities = await repo.GetPantryTransactionsAsync(start, end, pantryId, orderSt);
        return _mapper.Map<List<PantryTransactionDetail>>(entities); ;
    }

    public async Task<IEnumerable<PantryTransaksiStatusViewModel>> GetAllPantryTransaksiStatus()
    {
        var entities = await repo.GetAllPantryTransaksiStatus();
        var result = _mapper.Map<List<PantryTransaksiStatusViewModel>>(entities);
        return result;
    }
}