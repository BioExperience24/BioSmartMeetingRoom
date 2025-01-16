using _4.Data.ViewModels;

namespace _3.BusinessLogic.Services.Interface
{
    public interface IRoomService : IBaseLongService<RoomViewModel>
    {
        Task<IEnumerable<RoomViewModelAlt>> GetAllRoomItemAsync(bool withIsDisabled = true);
        Task<int> GetCountRoomItemAsync(bool withIsDisabled = true);
        Task<IEnumerable<RoomViewModelAlt>> GetAllRoomRoomDisplayItemAsync();
        Task<IEnumerable<RoomViewModelAlt>> GetAllRoomWithRadidsItemAsycn(string[] radIds);
        Task<IEnumerable<RoomViewModelAlt>> GetAllRoomAvailableAsync(RoomVMFindAvailable request);
        Task<IEnumerable<RoomDataViewModel>> GetRoomData();
        //Task<List<SearchCriteriaViewModel>> GetReportusage(SearchCriteriaViewModel viewModel);
        Task<List<RoomViewModel>> GetSingleRoomData();
        Task<ReturnalModel> CreateRoom(RoomVMDefaultFR vm);
        Task<List<SettingInvoiceTextViewModel>> GetInvoiceStatus();
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
