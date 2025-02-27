using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3.BusinessLogic.Services.Implementation
{
    public class LevelDescriptiionService : BaseLongService<LevelDescriptiionViewModel, LevelDescriptiion>, ILevelDescriptiionService
    {
        private readonly LevelDescriptionRepository _repo;

        private readonly IMapper __mapper;

        public LevelDescriptiionService(LevelDescriptionRepository repo, IMapper mapper) : base(repo, mapper)
        { 
            _repo = repo;
            __mapper = mapper;
        }

        public async Task<LevelDescriptiionViewModel> GetItemByLevelIdAsync(int levelId)
        {
            var levelDescriptiions = await _repo.GetItemByLevelIdAsync(levelId);
            var result = __mapper.Map<LevelDescriptiionViewModel>(levelDescriptiions);
            return result;
        }
    }
}