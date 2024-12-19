using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _7.Entities.Models;
using AutoMapper;

namespace _3.BusinessLogic.Services.Implementation
{
    public class SettingPantryConfigService : BaseServiceId<SettingPantryConfigViewModel, SettingPantryConfig>, ISettingPantryConfigService
    {
        private readonly SettingPantryConfigRepository _repo;
        private readonly IMapper _mapper;

        public SettingPantryConfigService(SettingPantryConfigRepository repo, IMapper mapper) : base(repo, mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<SettingPantryConfigVMResponse> GetAllSettingPantryConfigsAsync()
        {
            var (list, err) = await _repo.GetAllSettingPantryConfigsAsync();
            return new SettingPantryConfigVMResponse
            {
                Error = err,
                Data = _mapper.Map<List<SettingPantryConfigVMProp>>(list)
            };
        }

        public async Task<SettingPantryConfigVMResponse> GetSettingPantryConfigByIdAsync(SettingPantryConfigUpdateViewModelFR request)
        {
            SettingPantryConfig? config = null;

            // if (Int32.TryParse(request.Id, out Int32 result))
            config = await _repo.GetSettingPantryConfigById(request.Id);

            if (config == null)
            {
                return null;
            }

            return _mapper.Map<SettingPantryConfigVMResponse>(config);
        }

        public async Task<SettingPantryConfig?> CreateSettingPantryConfigAsync(SettingPantryConfigCreateViewModelFR request)
        {
            SettingPantryConfig item = _mapper.Map<SettingPantryConfig>(request);
            //item.IsDeleted = 0;
            //item.CreatedAt = DateTime.Now;
            // item.CreatedBy = // uncomment if you have auth

            var result = await _repo.AddSettingPantryConfigAsync(item);

            return result;
        }

        public async Task<SettingPantryConfig?> UpdateSettingPantryConfigAsync(SettingPantryConfigCreateViewModelFR request)
        {
            SettingPantryConfig? config = null;

            //if (int.TryParse(request.Id, out int result))
            //config = await _repo.GetSettingPantryConfigById(request.Id
            config = await _repo.GetSettingPantryConfigTopOne();

            if (config == null)
            {
                return null;
            }

            _mapper.Map(request, config);

            //config.UpdatedAt = DateTime.Now;
            // config.UpdatedBy = // uncomment if you have auth

            if (!await _repo.UpdateSettingPantryConfigAsync(config))
            {
                return null;
            }

            return config;
        }

        public async Task<SettingPantryConfig?> UpdateOrCreateSettingPantryConfigAsync(SettingPantryConfigCreateViewModelFR request)
        {
            SettingPantryConfig? config = null;

            //if (int.TryParse(request.Id, out int result))
            //config = await _repo.GetSettingPantryConfigById(request.Id
            config = await _repo.GetSettingPantryConfigTopOne();

            if (config == null)
            {
                config = await this.CreateSettingPantryConfigAsync(request);
                if (config == null)
                    return null;
            }

            _mapper.Map(request, config);

            //config.UpdatedAt = DateTime.Now;
            // config.UpdatedBy = // uncomment if you have auth

            if (!await _repo.UpdateSettingPantryConfigAsync(config))
            {
                return null;
            }

            return config;
        }
        public async Task<SettingPantryConfig?> DeleteSettingPantryConfigAsync(SettingPantryConfigDeleteViewModelFR request)
        {
            SettingPantryConfig? config = null;

            //if (int.TryParse(request.Id, out int result))
            config = await _repo.GetSettingPantryConfigById(request.Id);

            if (config == null)
            {
                return null;
            }

            _mapper.Map(request, config);

            //config.IsDeleted = 1;
            //config.UpdatedAt = DateTime.Now;
            // config.UpdatedBy = // uncomment if you have auth

            if (!await _repo.UpdateSettingPantryConfigAsync(config))
            {
                return null;
            }

            return config;
        }
    }
}