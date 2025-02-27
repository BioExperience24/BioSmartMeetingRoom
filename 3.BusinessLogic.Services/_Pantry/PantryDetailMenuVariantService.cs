namespace _3.BusinessLogic.Services.Implementation;

public class PantryDetailMenuVariantService(PantryDetailMenuVariantRepository sp, IMapper mapper)
    : BaseService<PantryDetailMenuVariantViewModel, PantryDetailMenuVariant>(sp, mapper), IPantryDetailMenuVariantService
{


}