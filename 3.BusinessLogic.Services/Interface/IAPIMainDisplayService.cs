namespace _3.BusinessLogic.Services.Interface;

public interface IAPIMainDisplayService
{
    Task<ReturnalModel> DisplayScheduleFastBooked(BookingDisplayScheduleFastBookedFRViewModel request);

    Task<ReturnalModel> DisplayGetRoomMerge(ListBookingByDateFR request);
    Task<ReturnalModel> DisplayScheduleMergeTimeBooked(ListBookingByDateFR request);
    Task<ReturnalModel> DisplayScheduleTimeFastBooked(ListBookingByDateNikFRViewModel request);
    // jangan dihapus, belum selesai
    // Task<ReturnalModel> DisplayScheduleFastBooked(BookingDisplayScheduleFastBookedFRViewModel request);
    Task<ReturnalModel> GetDisplayRoomList();
    Task<ReturnalModel> GetDisplayRoomMergeList(ListRoomMergeFRViewModel request);
    Task<ReturnalModel> GetDisplayScheduleOccupiedList(ListScheduleOccupiedFRViewModel request);
    Task<ReturnalModel> GetDisplayBySerial(ListDisplaySerialFRViewModel request);
    Task<ReturnalModel> DisplayMeetingWithMoreRoomListDisplay(ListDisplayMeetingScheduleTodayFRViewModel request);
    Task<ReturnalModel> DisplayMeetingWithMoreRoomOccupiedListDisplay(ListDisplayMeetingScheduleTodayFRViewModel request);
    Task<FileReady> GetQrCodeDetailView(string id, int h = 60);
    Task<ReturnalModel> CheckDoorOpenMeetingPin(CheckDoorOpenMeetingPinFRViewModel request);
    Task<ReturnalModel> CheckSerialIsAlready(string serial);
    Task<ReturnalModel> GetScheduledDisplay(DisplayScheduledFRViewModel request);
    Task<ReturnalModel> GetDisplayRoomAvailable(DisplayRoomAvailableViewModel request);
}