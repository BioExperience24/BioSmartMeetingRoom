namespace _3.BusinessLogic.Services.Interface;

public interface IPantryService : IBaseLongService<PantryViewModel>
{
    Task<IEnumerable<PantryViewModel>> GetAllPantryAndImage();

    Task<PantryViewModel?> CreatePantry(PantryViewModel cReq);
    Task<FileReady> GetPantryView(long id, int h);
}
