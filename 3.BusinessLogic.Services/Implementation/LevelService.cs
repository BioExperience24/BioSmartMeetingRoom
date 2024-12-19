

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

        /* public override async Task<IEnumerable<LevelViewModel>> GetAll()
        {
            var levels = await _repo.GetAllAsync();
            var result = __mapper.Map<List<LevelViewModel>>(levels);
            return result;
        } */
    }
}