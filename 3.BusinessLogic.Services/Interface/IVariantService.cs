namespace _3.BusinessLogic.Services.Interface;
public interface IVariantService
{
    Task<IEnumerable<PantryDetailMenuVariantViewModel>> GetVariantByMenuId(long PantryDetailId);

    Task<PantryDetailMenuVariantViewModel?> CreateMenuAndVariant(PantryDetailMenuVariantViewModel request);
    Task<PantryVariantDataAndDetail?> GetVariantId(string id);
    Task<PantryDetailMenuVariantViewModel?> DeleteVariantAndDetails(PantryDetailMenuVariantViewModel dReq);
    Task<PantryDetailMenuVariantViewModel?> UpdateVariantAndDetail(PantryDetailMenuVariantViewModel cReq);
    Task<IEnumerable<PantryDetailMenuVariantDetailViewModel>> GetVariantDetailByMenuId(long PantryDetailId);
}