namespace _3.BusinessLogic.Services.Implementation;

public class PantrySatuanService(PantrySatuanRepository repo, IMapper mapper, IConfiguration config)
    : BaseLongService<PantrySatuanViewModel, PantrySatuan>(repo, mapper), IPantrySatuanService
{
}

