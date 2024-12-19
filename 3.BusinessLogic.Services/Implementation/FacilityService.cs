namespace _3.BusinessLogic.Services.Implementation;

public class FacilityService : BaseLongService<FacilityViewModel, Facility>, IFacilityService
{
    private readonly FacilityRepository _repo;
    private readonly IMapper __mapper;

    public FacilityService(FacilityRepository repo, IMapper mapper) : base(repo, mapper)
    {
        _repo = repo;
        __mapper = mapper;
    }

    public async Task<FacilityVMResponse> GetAllFacilityAsync()
    {
        var (list, err) = await _repo.GetAllFacilityAsync();
        return new FacilityVMResponse
        {
            Error = err,
            Data = __mapper.Map<List<FacilityVMProp>>(list)
        };
    }

    public async Task<FacilityVMResponse> GetFacilityByIdAsync(FacilityUpdateViewModelFR request)
    {

        var facility = await _repo.GetFacilityById(request.Id);

        if (facility == null)
        {
            return null;
        }

        return _mapper.Map<FacilityVMResponse>(facility);
    }

    public async Task<Facility?> CreateFacilityAsync(FacilityCreateViewModelFR request)
    {
        Facility item = __mapper.Map<Facility>(request);
        item.IsDeleted = 0;
        item.CreatedAt = DateTime.Now;
        // item.CreatedBy = // uncomment jika sudah memiliki auth

        var result = await _repo.AddFacilityAsync(item);

        return result;
    }

    public async Task<Facility?> UpdateFacilityAsync(FacilityUpdateViewModelFR request)
    {
        var facility = await _repo.GetFacilityById(request.Id);

        if (facility == null)
        {
            return null;
        }

        __mapper.Map(request, facility);

        // FacilityType.InvoiceStatus = request.InvoiceStatus;
        facility.UpdatedAt = DateTime.Now;
        // FacilityType.UpdatedBy = // uncomment jika sudah memiliki auth

        if (!await _repo.UpdateFacilityAsync(facility))
        {
            return null;
        }

        return facility;
    }

    public async Task<Facility?> DeleteFacilityAsync(FacilityDeleteViewModelFR request)
    {
        var facility = await _repo.GetFacilityById(request.Id);

        if (facility == null)
        {
            return null;
        }

        __mapper.Map(request, facility);

        facility.IsDeleted = 1;
        facility.UpdatedAt = DateTime.Now;
        // FacilityType.UpdatedBy = // uncomment jika sudah memiliki auth

        if (!await _repo.UpdateFacilityAsync(facility))
        {
            return null;
        }

        return facility;
    }


}

