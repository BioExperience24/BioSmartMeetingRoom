using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _7.Entities.Models;
using AutoMapper;

namespace _3.BusinessLogic.Services.Implementation
{
    public class SettingLogConfigService : BaseService<SettingLogConfigViewModel, SettingLogConfig>, ISettingLogConfigService
    {
        private readonly SettingLogConfigRepository _repo;
        private readonly IMapper _mapper;

        public SettingLogConfigService(SettingLogConfigRepository repo, IMapper mapper) : base(repo, mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<SettingLogConfigVMResponse> GetAllSettingLogConfigsAsync()
        {
            var (list, err) = await _repo.GetAllSettingLogConfigsAsync();
            return new SettingLogConfigVMResponse
            {
                Error = err,
                Data = _mapper.Map<List<SettingLogConfigVMProp>>(list)
            };
        }

        public async Task<SettingLogConfigVMResponse> GetSettingLogConfigByIdAsync(SettingLogConfigUpdateViewModelFR request)
        {
            SettingLogConfig? config = null;

            if (Int32.TryParse(request.Id, out Int32 result))
                config = await _repo.GetSettingLogConfigById(result);

            if (config == null)
            {
                return null;
            }

            return _mapper.Map<SettingLogConfigVMResponse>(config);
        }

        public async Task<SettingLogConfig?> CreateSettingLogConfigAsync(SettingLogConfigCreateViewModelFR request)
        {
            SettingLogConfig item = _mapper.Map<SettingLogConfig>(request);
            item.IsDeleted = 0;
            //item.CreatedAt = DateTime.Now;
            // item.CreatedBy = // uncomment if you have auth

            var result = await _repo.AddSettingLogConfigAsync(item);

            return result;
        }

        public async Task<SettingLogConfig?> UpdateSettingLogConfigAsync(SettingLogConfigUpdateViewModelFR request)
        {
            SettingLogConfig? config = null;

            if (long.TryParse(request.Id, out long result))
                config = await _repo.GetSettingLogConfigById(result);

            if (config == null)
            {
                return null;
            }

            _mapper.Map(request, config);

            //config.UpdatedAt = DateTime.Now;
            // config.UpdatedBy = // uncomment if you have auth

            if (!await _repo.UpdateSettingLogConfigAsync(config))
            {
                return null;
            }

            return config;
        }

        public async Task<SettingLogConfig?> DeleteSettingLogConfigAsync(SettingLogConfigDeleteViewModelFR request)
        {
            SettingLogConfig? config = null;

            if (long.TryParse(request.Id, out long result))
                config = await _repo.GetSettingLogConfigById(result);

            if (config == null)
            {
                return null;
            }

            _mapper.Map(request, config);

            config.IsDeleted = 1;
            //config.UpdatedAt = DateTime.Now;
            // config.UpdatedBy = // uncomment if you have auth

            if (!await _repo.UpdateSettingLogConfigAsync(config))
            {
                return null;
            }

            return config;
        }
    }
}