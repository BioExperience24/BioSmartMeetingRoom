using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _7.Entities.Models;
using AutoMapper;

namespace _3.BusinessLogic.Services.Implementation
{
    public class LicenseSettingService : BaseService<LicenseSettingViewModel, LicenseSetting>, ILicenseSettingService
    {
        private readonly LicenseSettingRepository _repo;
        private readonly IMapper _mapper;

        public LicenseSettingService(LicenseSettingRepository repo, IMapper mapper) : base(repo, mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<LicenseSettingVMResponse> GetAllLicenseSettingsAsync()
        {
            var (list, err) = await _repo.GetAllLicenseSettingsAsync();
            return new LicenseSettingVMResponse
            {
                Error = err,
                Data = _mapper.Map<List<LicenseSettingVMProp>>(list)
            };
        }

        public async Task<LicenseSettingVMResponse> GetLicenseSettingByIdAsync(LicenseSettingUpdateViewModelFR request)
        {
            LicenseSetting? setting = null;

            if (request.Serial != null)
                setting = await _repo.GetOneByField("Serial", request.Serial);

            if (setting == null)
            {
                return null;
            }

            return _mapper.Map<LicenseSettingVMResponse>(setting);
        }

        public async Task<LicenseSetting?> CreateLicenseSettingAsync(LicenseSettingCreateViewModelFR request)
        {
            LicenseSetting item = _mapper.Map<LicenseSetting>(request);
            item.IsDeleted = 0;

            var result = await _repo.AddLicenseSettingAsync(item);

            return result;
        }

        public async Task<LicenseSetting?> UpdateLicenseSettingAsync(LicenseSettingUpdateViewModelFR request)
        {
            LicenseSetting? setting = null;

            if (request.Serial != null)
                setting = await _repo.GetLicenseSettingBySerial(request.Serial);

            if (setting == null)
            {
                return null;
            }

            _mapper.Map(request, setting);

            if (!await _repo.UpdateLicenseSettingAsync(setting))
            {
                return null;
            }

            return setting;
        }

        public async Task<LicenseSetting?> DeleteLicenseSettingAsync(LicenseSettingDeleteViewModelFR request)
        {
            LicenseSetting? setting = await _repo.GetLicenseSettingById(request.Id);

            if (setting == null)
            {
                return null;
            }

            _mapper.Map(request, setting);

            setting.IsDeleted = 1;

            if (!await _repo.UpdateLicenseSettingAsync(setting))
            {
                return null;
            }

            return setting;
        }
    }
}