
namespace _3.BusinessLogic.Services.Interface;

public interface IPantryDetailService
    : IBaseLongService<PantryDetailViewModel>
{
    Task<IEnumerable<PantryDetailViewModel>> GetByPantryId(string id);
    Task<FileReady> GetPantryDetailView(long id, int h = 60);
}