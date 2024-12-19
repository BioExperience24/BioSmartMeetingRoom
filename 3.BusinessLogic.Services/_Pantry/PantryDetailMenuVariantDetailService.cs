namespace _3.BusinessLogic.Services.Implementation;

public class PantryDetailMenuVariantDetailService(IServiceProvider sp, IMapper mapper)
    : BaseLongUowService<PantryDetailMenuVariantDetailViewModel, PantryDetailMenuVariantDetail>(sp, mapper), IPantryDetailMenuVariantDetailService
{
}