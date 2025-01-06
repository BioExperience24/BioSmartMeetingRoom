using _4.Data.ViewModels;

namespace _3.BusinessLogic.Services.Interface
{
    public interface IRoomService : IBaseLongService<RoomViewModel>
    {
        Task<IEnumerable<RoomVMChartTopRoom>> GetAllChartTopFiveRoomAsync(int year);
        Task<IEnumerable<RoomViewModel>> GetAllRoomItemAsync(bool withIsDisabled = true);
        Task<int> GetCountRoomItemAsync(bool withIsDisabled = true);
        Task<IEnumerable<RoomViewModel>> GetAllRoomRoomDisplayItemAsync();
        Task<IEnumerable<RoomViewModel>> GetAllRoomWithRadidsItemAsycn(string[] radIds);
        Task<IEnumerable<RoomDataViewModel>> GetRoomData();
        Task<List<RoomViewModel>> GetSingleRoomData();
        Task<ReturnalModel> CreateRoom(RoomVMDefaultFR vm);
        Task<ReturnalModel> UpdateRoom(RoomVMUpdateFRViewModel vm, long id);
        Task<(string?, string?)> DoUploadAsync(IFormFile? file);
        Task<RoomDetailsViewModel> GetRoomDetailsAsync(string? pagename = "Room");
        Task<FileReady> GetRoomDetailView(string id, int h = 60);
        Task<List<RoomMergeDetailViewModel>> GetAllRoomMerge();
        Task<List<RoomMergeDetailViewModel>> GetRoomMerge(long id);
        Task<RoomVMUResponseFRViewModel> GetRoomById(long id);
        Task<List<RoomForUsageDetailListViewModel>> GetConfigRoomForUsageByIdRoom(long id);
    }
}