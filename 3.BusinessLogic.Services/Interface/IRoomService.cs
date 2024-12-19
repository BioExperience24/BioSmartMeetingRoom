using _4.Data.ViewModels;

namespace _3.BusinessLogic.Services.Interface
{
    public interface IRoomService : IBaseLongService<RoomViewModel>
    {
        public Task<RoomDetailsViewModel> GetRoomDetailsAsync();
        public Task<IEnumerable<RoomDataViewModel>> GetRoomData();
        public Task<List<RoomViewModel>> GetSingleRoomData();
        public Task<ReturnalModel> CreateRoom(RoomVMDefaultFR vm);
        public Task<ReturnalModel> UpdateRoom(RoomVMUpdateFRViewModel vm, long id);
        Task<(string?, string?)> DoUploadAsync(IFormFile? file);

        Task<FileReady> GetRoomDetailView(string id, int h = 60);
        public Task<List<RoomMergeDetailViewModel>> GetAllRoomMerge();
        public Task<List<RoomMergeDetailViewModel>> GetRoomMerge(long id);
        public Task<RoomVMUResponseFRViewModel> GetRoomById(long id);
        public Task<List<RoomForUsageDetailViewModel>> GetConfigRoomForUsageByIdRoom(long id);
    }
}