using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _7.Entities.Models;
using AutoMapper;

namespace _3.BusinessLogic.Services.Implementation
{
    public class VariableTimeExtendService : BaseServiceId<VariableTimeExtendViewModel, VariableTimeExtend>, IVariableTimeExtendService
    {
        private readonly VariableTimeExtendRepository _repo;
        private readonly IMapper _mapper;

        public VariableTimeExtendService(VariableTimeExtendRepository repo, IMapper mapper) : base(repo, mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<VariableTimeExtendVMResponse> GetAllVariableTimeExtendsAsync()
        {
            var (list, err) = await _repo.GetAllVariableTimeExtendsAsync();
            return new VariableTimeExtendVMResponse
            {
                Error = err,
                Data = _mapper.Map<List<VariableTimeExtendVMProp>>(list)
            };
        }

        public async Task<VariableTimeExtendVMResponse> GetVariableTimeExtendByIdAsync(VariableTimeExtendUpdateViewModelFR request)
        {
            VariableTimeExtend? config = null;

            if (long.TryParse(request.Id.ToString(), out long result))
                config = await _repo.GetVariableTimeExtendById(result);

            if (config == null)
            {
                return null;
            }

            return _mapper.Map<VariableTimeExtendVMResponse>(config);
        }

        public async Task<VariableTimeExtend?> CreateVariableTimeExtendAsync(VariableTimeExtendCreateViewModelFR request)
        {
            VariableTimeExtend item = _mapper.Map<VariableTimeExtend>(request);
            // item.CreatedAt = DateTime.Now; // Uncomment if you have a CreatedAt field

            var result = await _repo.AddVariableTimeExtendAsync(item);

            return result;
        }

        public async Task<VariableTimeExtend?> UpdateVariableTimeExtendAsync(VariableTimeExtendUpdateViewModelFR request)
        {
            VariableTimeExtend? config = null;

            if (long.TryParse(request.Id.ToString(), out long result))
                config = await _repo.GetVariableTimeExtendById(result);

            if (config == null)
            {
                return null;
            }

            _mapper.Map(request, config);

            // config.UpdatedAt = DateTime.Now; // Uncomment if you have an UpdatedAt field

            if (!await _repo.UpdateVariableTimeExtendAsync(config))
            {
                return null;
            }

            return config;
        }

        public async Task<VariableTimeExtend?> DeleteVariableTimeExtendAsync(VariableTimeExtendDeleteViewModelFR request)
        {
            VariableTimeExtend? config = null;

            if (long.TryParse(request.Id.ToString(), out long result))
                config = await _repo.GetVariableTimeExtendById(result);

            if (config == null)
            {
                return null;
            }

            // config.IsDeleted = 1; // Uncomment if you have an IsDeleted field
            // config.UpdatedAt = DateTime.Now; // Uncomment if you have an UpdatedAt field

            if (!await _repo.UpdateVariableTimeExtendAsync(config))
            {
                return null;
            }

            return config;
        }
    }
}