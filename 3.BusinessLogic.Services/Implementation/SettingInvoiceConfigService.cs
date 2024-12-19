using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _7.Entities.Models;
using AutoMapper;

namespace _3.BusinessLogic.Services.Implementation
{
    public class SettingInvoiceConfigService : BaseService<SettingInvoiceConfigViewModel, SettingInvoiceConfig>, ISettingInvoiceConfigService
    {
        private readonly SettingInvoiceConfigRepository _repo;
        private readonly IMapper _mapper;

        public SettingInvoiceConfigService(SettingInvoiceConfigRepository repo, IMapper mapper) : base(repo, mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<SettingInvoiceConfigVMResponse> GetAllSettingInvoiceConfigsAsync()
        {
            var (list, err) = await _repo.GetAllSettingInvoiceConfigsAsync();
            return new SettingInvoiceConfigVMResponse
            {
                Error = err,
                Data = _mapper.Map<List<SettingInvoiceConfigVMProp>>(list)
            };
        }

        public async Task<SettingInvoiceConfigVMResponse> GetSettingInvoiceConfigByIdAsync(long id)
        {
            SettingInvoiceConfig? config = null;

            //if (Int32.TryParse(request.Id, out Int32 result))
                config = await _repo.GetSettingInvoiceConfigById(id);

            if (config == null)
            {
                return null;
            }

            return _mapper.Map<SettingInvoiceConfigVMResponse>(config);
        }

        public async Task<SettingInvoiceConfig?> CreateSettingInvoiceConfigAsync(SettingInvoiceConfigCreateViewModelFR request)
        {
            SettingInvoiceConfig item = _mapper.Map<SettingInvoiceConfig>(request);
            item.IsDeleted = 0;
            //item.CreatedAt = DateTime.Now;
            // item.CreatedBy = // uncomment if you have auth

            var result = await _repo.AddSettingInvoiceConfigAsync(item);

            return result;
        }

        public async Task<SettingInvoiceConfig?> UpdateSettingInvoiceConfigAsync(SettingInvoiceConfigUpdateViewModelFR request)
        {
            SettingInvoiceConfig? config = null;

            if (long.TryParse(request.Id, out long result))
                config = await _repo.GetSettingInvoiceConfigById(result);

            if (config == null)
            {
                return null;
            }

            _mapper.Map(request, config);

            //config.UpdatedAt = DateTime.Now;
            // config.UpdatedBy = // uncomment if you have auth

            if (!await _repo.UpdateSettingInvoiceConfigAsync(config))
            {
                return null;
            }

            return config;
        }

        public async Task<SettingInvoiceConfig?> DeleteSettingInvoiceConfigAsync(SettingInvoiceConfigDeleteViewModelFR request)
        {
            SettingInvoiceConfig? config = null;

            if (long.TryParse(request.Id, out long result))
                config = await _repo.GetSettingInvoiceConfigById(result);

            if (config == null)
            {
                return null;
            }

            _mapper.Map(request, config);

            config.IsDeleted = 1;
            //config.UpdatedAt = DateTime.Now;
            // config.UpdatedBy = // uncomment if you have auth

            if (!await _repo.UpdateSettingInvoiceConfigAsync(config))
            {
                return null;
            }

            return config;
        }
    }
}