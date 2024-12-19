using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _7.Entities.Models;
using AutoMapper;

namespace _3.BusinessLogic.Services.Implementation
{
    public class SettingSmtpService : BaseService<SettingSmtpViewModel, SettingSmtp>, ISettingSmtpService
    {
        private readonly SettingSmtpRepository _repo;
        private readonly IMapper _mapper;

        public SettingSmtpService(SettingSmtpRepository repo, IMapper mapper) : base(repo, mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<SettingSmtpVMResponse> GetAllSettingSmtpsAsync()
        {
            var (list, err) = await _repo.GetAllSettingSmtpsAsync();
            return new SettingSmtpVMResponse
            {
                Error = err,
                Data = _mapper.Map<List<SettingSmtpVMProp>>(list)
            };
        }

        public async Task<SettingSmtpVMResponse> GetSettingSmtpByIdAsync(SettingSmtpUpdateViewModelFR request)
        {
            SettingSmtp? config = null;

            //if (int.TryParse(request.Id, out int result))
            //    config = await _repo.GetSettingSmtpById(request.Id);
           if (request.Name!=null)
                config = await _repo.GetOneByField("Name", request.Name);

            if (config == null)
            {
                return null;
            }

            return _mapper.Map<SettingSmtpVMResponse>(config);
        }

        public async Task<SettingSmtp?> CreateSettingSmtpAsync(SettingSmtpCreateViewModelFR request)
        {
            SettingSmtp item = _mapper.Map<SettingSmtp>(request);
            item.IsDeleted = 0;
            //item.CreatedAt = DateTime.Now;
            // item.CreatedBy = // uncomment if you have auth

            var result = await _repo.AddSettingSmtpAsync(item);

            return result;
        }

        public async Task<SettingSmtp?> UpdateSettingSmtpAsync(SettingSmtpUpdateViewModelFR request)
        {
            SettingSmtp? config = null;

            if (request.Name!=null)
                config = await _repo.GetSettingSmtpByName(request.Name);

            if (config == null)
            {
                return null;
            }

            _mapper.Map(request, config);

            //config.UpdatedAt = DateTime.Now;
            // config.UpdatedBy = // uncomment if you have auth

            if (!await _repo.UpdateSettingSmtpAsync(config))
            {
                return null;
            }

            return config;
        }

        public async Task<SettingSmtp?> DeleteSettingSmtpAsync(SettingSmtpDeleteViewModelFR request)
        {
            SettingSmtp? config = null;

            //if (int.TryParse(request.Id, out int result))
                config = await _repo.GetSettingSmtpById(request.Id);

            if (config == null)
            {
                return null;
            }

            _mapper.Map(request, config);

            config.IsDeleted = 1;
            //config.UpdatedAt = DateTime.Now;
            // config.UpdatedBy = // uncomment if you have auth

            if (!await _repo.UpdateSettingSmtpAsync(config))
            {
                return null;
            }

            return config;
        }
    }
}