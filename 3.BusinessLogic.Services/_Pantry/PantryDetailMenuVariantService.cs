namespace _3.BusinessLogic.Services.Implementation;

public class PantryDetailMenuVariantService(IServiceProvider sp, IMapper mapper)
    : BaseUowService<PantryDetailMenuVariantViewModel, PantryDetailMenuVariant>(sp, mapper), IPantryDetailMenuVariantService
{


}