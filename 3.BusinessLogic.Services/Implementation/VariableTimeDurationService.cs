using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _7.Entities.Models;
using AutoMapper;

namespace _3.BusinessLogic.Services.Implementation
{
    public class VariableTimeDurationService : BaseServiceId<VariableTimeDurationViewModel, VariableTimeDuration>, IVariableTimeDurationService
    {
        private readonly VariableTimeDurationRepository _repo;
        private readonly IMapper _mapper;

        public VariableTimeDurationService(VariableTimeDurationRepository repo, IMapper mapper) : base(repo, mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<VariableSettingVMResponse> GetAllVariablesAsync()
        {
            var (list, err) = await _repo.GetAllVariablesAsync();
            return new VariableSettingVMResponse
            {
                Error = err,
                Data = _mapper.Map<Dictionary<string, object>>(list)
            };
        }

        public async Task<VariableTimeDurationVMResponse> GetAllVariableTimeDurationsAsync()
        {
            var (list, err) = await _repo.GetAllVariableTimeDurationsAsync();
            return new VariableTimeDurationVMResponse
            {
                Error = err,
                Data = _mapper.Map<List<VariableTimeDurationVMProp>>(list)
            };
        }

        public async Task<VariableTimeDurationVMResponse> GetVariableTimeDurationByIdAsync(VariableTimeDurationUpdateViewModelFR request)
        {
            VariableTimeDuration? config = null;

            if (long.TryParse(request.Id.ToString(), out long result))
                config = await _repo.GetVariableTimeDurationById(result);

            if (config == null)
            {
                return null;
            }

            return _mapper.Map<VariableTimeDurationVMResponse>(config);
        }

        public async Task<VariableTimeDuration?> CreateVariableTimeDurationAsync(VariableTimeDurationCreateViewModelFR request)
        {
            VariableTimeDuration item = _mapper.Map<VariableTimeDuration>(request);
            // item.CreatedAt = DateTime.Now; // Uncomment if you have a CreatedAt field

            var result = await _repo.AddVariableTimeDurationAsync(item);

            return result;
        }

        public async Task<VariableTimeDuration?> UpdateVariableTimeDurationAsync(VariableTimeDurationUpdateViewModelFR request)
        {
            VariableTimeDuration? config = null;

            if (long.TryParse(request.Id.ToString(), out long result))
                config = await _repo.GetVariableTimeDurationById(result);

            if (config == null)
            {
                return null;
            }

            _mapper.Map(request, config);

            // config.UpdatedAt = DateTime.Now; // Uncomment if you have an UpdatedAt field

            if (!await _repo.UpdateVariableTimeDurationAsync(config))
            {
                return null;
            }

            return config;
        }

        public async Task<VariableTimeDuration?> DeleteVariableTimeDurationAsync(VariableTimeDurationDeleteViewModelFR request)
        {
            VariableTimeDuration? config = null;

            if (long.TryParse(request.Id.ToString(), out long result))
                config = await _repo.GetVariableTimeDurationById(result);

            if (config == null)
            {
                return null;
            }

            // config.IsDeleted = 1; // Uncomment if you have an IsDeleted field
            // config.UpdatedAt = DateTime.Now; // Uncomment if you have an UpdatedAt field

            if (!await _repo.UpdateVariableTimeDurationAsync(config))
            {
                return null;
            }

            return config;
        }
    }
}