

namespace _3.BusinessLogic.Services.Interface;

public interface ILevelService : IBaseLongService<LevelViewModel>
{
    Task<Level?> GetLevel(int levelId);
    // Task<List<MenuHeaderLevel>> GetMenuHeader(int levelId);
    Task<List<LevelMenu>> GetMenu(int levelId);
}