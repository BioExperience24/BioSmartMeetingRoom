namespace _3.BusinessLogic.Services.Interface;

public interface IFacilityService : IBaseLongService<FacilityViewModel>
{

    public Task<FacilityVMResponse> GetAllFacilityAsync();
    public Task<FacilityVMResponse> GetFacilityByIdAsync(FacilityUpdateViewModelFR request);

    public Task<Facility?> CreateFacilityAsync(FacilityCreateViewModelFR request);

    public Task<Facility?> UpdateFacilityAsync(FacilityUpdateViewModelFR request);

    public Task<Facility?> DeleteFacilityAsync(FacilityDeleteViewModelFR request);

}
