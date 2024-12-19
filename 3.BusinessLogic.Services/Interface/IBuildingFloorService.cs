using System;

namespace _3.BusinessLogic.Services.Interface;

public interface IBuildingFloorService : IBaseLongService<BuildingFloorViewModel>
{
    Task<IEnumerable<BuildingFloorViewModel>> GetAllItemAsync(BuildingFloorViewModel? vm = null);
    Task<BuildingFloorViewModel?> GetItemByEntityAsync(BuildingFloorViewModel vm);
    Task<BuildingFloorViewModel?> Delete(BuildingFloorViewModel vm);
    Task<BuildingFloorViewModel?> UploadAsync(BuildingFloorViewModel vm);
    Task<(string?, string?)> DoUploadAsync(IFormFile? file);
}
