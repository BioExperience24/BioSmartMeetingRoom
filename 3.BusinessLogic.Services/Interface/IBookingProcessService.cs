

namespace _3.BusinessLogic.Services.Interface
{
    public interface IBookingProcessService
    {
        Task<(BookingViewModel?, string?)> CreateBookingAsync(BookingVMCreateReserveFR request);
        Task<(IEnumerable<BookingViewModel>, int, int)> GetAllItemDataTablesAsync(BookingVMDataTableFR request);
        Task<EmployeeViewModel?> GetPicFilteredByBookingIdAsync(string bookingId);
        Task<ReturnalModel> CheckRescheduleDateAsync(string bookingId, DateOnly date, string roomId);
        Task<ReturnalModel> RescheduleBookingAsync(BookingVMRescheduleFR request);
        Task<ReturnalModel> CancelBookingAsync(BookingVMCancelFR request);
        Task<ReturnalModel> EndMeetingAsync(BookingVMEndMeetingFR request);
        Task<ReturnalModel> CheckExtendMeetingTimeAsync(BookingVMCheckExtendMeetingFR request);
        Task<ReturnalModel> SetExtendMeetingAsync(BookingVMExtendMeetingFR request);
    }
}