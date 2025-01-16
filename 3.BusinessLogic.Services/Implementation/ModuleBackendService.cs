

namespace _3.BusinessLogic.Services.Implementation
{
    public class ModuleBackendService(IMapper mapper, ModuleBackendRepository repo) : IModuleBackendService
    {
        private readonly IMapper _mapper = mapper;
        private readonly ModuleBackendRepository _repo = repo;

        public async Task<ModuleBackendViewModel?> GetItemAsync(ModuleBackendViewModel? vm = null)
        {
            var entity = _mapper.Map<ModuleBackend>(vm);

            var item = await _repo.GetItemByEntityAsync(entity);

            return item != null ?
                    _mapper.Map<ModuleBackendViewModel>(item) : null;
        }

        public async Task<IEnumerable<ModuleBackendViewModel>> GetItemsByModuleTextAsync(string[] moduleTexts)
        {
            var items = await _repo.GetItemsByModuleTextAsync(moduleTexts);

            return _mapper.Map<List<ModuleBackendViewModel>>(items);
        }
    }
}