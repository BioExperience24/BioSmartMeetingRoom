

namespace _3.BusinessLogic.Services.Interface;

public interface ILevelService : IBaseLongService<LevelViewModel>
{
    Task<List<MenuHeaderLevel>> GetLevel(int levelId);
    Task<List<MenuHeaderLevel>> GetMenuHeader(int levelId);
}