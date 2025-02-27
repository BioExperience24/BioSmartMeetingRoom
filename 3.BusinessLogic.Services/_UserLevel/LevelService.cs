

namespace _3.BusinessLogic.Services.Implementation
{
    public class LevelService : BaseLongService<LevelViewModel, Level>, ILevelService
    {
        private readonly LevelRepository _repo;

        private readonly IMapper __mapper;

        // public LevelService(LevelRepository repo, IMapper mapper) : base(repo, mapper)
        public LevelService(LevelRepository repo, IMapper mapper) : base(repo, mapper)
        {
            _repo = repo;
            __mapper = mapper;
        }
        public async Task<List<MenuHeaderLevel>> GetLevel(int levelId)
        {
            var level = await _repo.GetLevel(levelId);
            return level;
        }

        public async Task<List<MenuHeaderLevel>> GetMenuHeader(int levelId)
        {
            var level = await _repo.GetMenuHeader(levelId);
            return level;
        }
    }
}