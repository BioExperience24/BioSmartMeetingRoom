

namespace _3.BusinessLogic.Services.Implementation
{
    public class UserConfigService : IUserConfigService
    {
        private readonly UserConfigRepository _repo;
    
        private readonly IMapper _mapper;

        public UserConfigService(UserConfigRepository repo, IMapper mapper)
        { 
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<UserConfigViewModel?> GetOneItem()
        {
            var userConfig = await _repo.GetOneItem();

            if (userConfig == null)
            {
                return null;
            }

            var item = _mapper.Map<UserConfigViewModel>(userConfig);

            return item;
        }
    }
}