using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;
using _7.Entities.Models;
using AutoMapper;

namespace _3.BusinessLogic.Services.Implementation
{
    public class LicenseListService : BaseService<LicenseListViewModel, LicenseList>, ILicenseListService
    {
        private readonly LicenseListRepository _repo;
        private readonly IMapper _mapper;

        public LicenseListService(LicenseListRepository repo, IMapper mapper) : base(repo, mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<LicenseListVMResponse> GetAllLicenseListsAsync()
        {
            var (list, err) = await _repo.GetAllLicenseListsAsync();
            return new LicenseListVMResponse
            {
                Error = err,
                Data = _mapper.Map<List<LicenseListVMProp>>(list)
            };
        }

        public async Task<LicenseListVMResponse> GetLicenseListByIdAsync(LicenseListUpdateViewModelFR request)
        {
            LicenseList? license = null;

            if (request.Name != null)
                license = await _repo.GetOneByField("Name", request.Name);

            if (license == null)
            {
                return null;
            }

            return _mapper.Map<LicenseListVMResponse>(license);
        }

        public async Task<LicenseList?> CreateLicenseListAsync(LicenseListCreateViewModelFR request)
        {
            LicenseList item = _mapper.Map<LicenseList>(request);
            //item.IsDeleted = 0;

            var result = await _repo.AddLicenseListAsync(item);

            return result;
        }

        public async Task<LicenseList?> UpdateLicenseListAsync(LicenseListUpdateViewModelFR request)
        {
            LicenseList? license = null;

            if (request.Name != null)
                license = await _repo.GetLicenseListByName(request.Name);

            if (license == null)
            {
                return null;
            }

            _mapper.Map(request, license);

            if (!await _repo.UpdateLicenseListAsync(license))
            {
                return null;
            }

            return license;
        }

        public async Task<LicenseList?> DeleteLicenseListAsync(LicenseListDeleteViewModelFR request)
        {
            LicenseList? license = await _repo.GetLicenseListById(request.Id);

            if (license == null)
            {
                return null;
            }

            _mapper.Map(request, license);

            //license.IsDeleted = 1;

            if (!await _repo.UpdateLicenseListAsync(license))
            {
                return null;
            }

            return license;
        }
    }
}