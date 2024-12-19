using _3.BusinessLogic.Services.Interface;
using _4.Data.ViewModels;

namespace _3.BusinessLogic.Services.Interface;

public interface IBeaconFloorService : IBaseLongService<BeaconFloorViewModel>
{
    Task<IEnumerable<BeaconFloorViewModel>> GetAllItemAsync();
    Task<BeaconFloorViewModel?> GetItemByIdAsync(long id);
    Task<(string?, string?)> DoUploadAsync(IFormFile? file);
}
