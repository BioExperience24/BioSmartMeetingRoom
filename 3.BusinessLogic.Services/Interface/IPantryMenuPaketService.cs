
namespace _3.BusinessLogic.Services.Interface;

public interface IPantryMenuPaketService : IBaseService<PantryMenuPaketViewModel>
{
    Task<PantryPackageDataAndDetail> GetPackageAndDetail(string id);
    Task<PantryMenuPaketViewModel?> UpdatePackage(PantryMenuPaketViewModel uReq);
    Task<PantryMenuPaketViewModel?> DeletePackage(string? id);
}