
namespace _3.BusinessLogic.Services.Interface
{
    public interface IBuildingService : IBaseLongService<BuildingViewModel>
    {
        Task<IEnumerable<BuildingViewModel>> GetAllItemAsync(long? excludeId = null);
        Task<BuildingViewModel?> GetItemByIdAsync(long id);
        Task<BuildingViewModel?> CreateAsync(BuildingVMDefaultFR request);
        Task<BuildingViewModel?> UpdateAsync(long id, BuildingVMDefaultFR request);
        Task<BuildingViewModel?> DeleteAsync(BuildingVMDeleteFR request);
        Task<(string?, string?)> DoUploadAsync(IFormFile? file);
    }
}