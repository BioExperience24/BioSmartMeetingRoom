namespace _3.BusinessLogic.Services.Implementation;

public class PantryDetailMenuVariantDetailService(PantryDetailMenuVariantDetailRepository repo, IMapper mapper)
    : BaseLongService<PantryDetailMenuVariantDetailViewModel, PantryDetailMenuVariantDetail>(repo, mapper), IPantryDetailMenuVariantDetailService
{
}