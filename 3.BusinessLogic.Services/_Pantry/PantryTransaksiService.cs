namespace _3.BusinessLogic.Services.Implementation;

public class PantryTransaksiService(PantryTransaksiRepository repo, IMapper mapper)
    : BaseService<PantryTransaksiViewModel, PantryTransaksi>(repo, mapper), IPantryTransaksiService
{
}