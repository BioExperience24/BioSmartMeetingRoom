using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _7.Entities.Models;
using AutoMapper;

namespace _3.BusinessLogic.Services.Implementation
{
    public class SettingRuleBookingService : BaseServiceId<SettingRuleBookingViewModel, SettingRuleBooking>, ISettingRuleBookingService
    {
        private readonly SettingRuleBookingRepository _repo;
        private readonly IMapper _mapper;

        public SettingRuleBookingService(SettingRuleBookingRepository repo, IMapper mapper) : base(repo, mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<SettingRuleBookingVMResponse> GetAllSettingRuleBookingsAsync()
        {
            var (list, err) = await _repo.GetAllSettingRuleBookingsAsync();
            return new SettingRuleBookingVMResponse
            {
                Error = err,
                Data = _mapper.Map<List<SettingRuleBookingVMProp>>(list)
            };
        }

        public async Task<SettingRuleBookingVMResponse> GetSettingRuleBookingByIdAsync(SettingRuleBookingDeleteViewModelFR request)
        {
            SettingRuleBooking? config = null;

            if (long.TryParse(request.Id.ToString(), out long result))
                config = await _repo.GetSettingRuleBookingById(result);

            if (config == null)
            {
                return null;
            }

            return _mapper.Map<SettingRuleBookingVMResponse>(config);
        }

        public async Task<SettingRuleBooking?> CreateSettingRuleBookingAsync(SettingRuleBookingCreateViewModelFR request)
        {
            SettingRuleBooking item = _mapper.Map<SettingRuleBooking>(request);
            // item.CreatedAt = DateTime.Now; // Uncomment if you have a CreatedAt field

            var result = await _repo.AddSettingRuleBookingAsync(item);

            return result;
        }

        public async Task<int?> UpdateSettingRuleBookingAsync(SettingRuleBookingUpdateViewModelFR request)
        {
            SettingRuleBooking? config = null;

            //if (long.TryParse(request.Id.ToString(), out long result))
            config = await _repo.GetSettingRuleBookingTopOne();

            if (config == null)
            {
                return null;
            }

            _mapper.Map(request, config);

            // config.UpdatedAt = DateTime.Now; // Uncomment if you have an UpdatedAt field

            if (await _repo.UpdateAsync(config) == 1)
            {
                return 1;
            }

            return null;
        }

        //Task<SettingRuleBooking?> DeleteSettingRuleBookingAsync(SettingRuleBookingDeleteViewModelFR request);
        public async Task<int?> DeleteSettingRuleBookingAsync(SettingRuleBookingDeleteViewModelFR request)
        {
            SettingRuleBooking? config = null;

            if (long.TryParse(request.Id.ToString(), out long result))
                config = await _repo.GetSettingRuleBookingById(result);

            if (config == null)
            {
                return null;
            }

            // config.IsDeleted = 1; // Uncomment if you have an IsDeleted field
            // config.UpdatedAt = DateTime.Now; // Uncomment if you have an UpdatedAt field

            if (await _repo.UpdateAsync(config)==1)
            {
                return null;
            }

            return 1;
        }
    }
}