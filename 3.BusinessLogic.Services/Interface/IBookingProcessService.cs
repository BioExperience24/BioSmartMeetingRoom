

namespace _3.BusinessLogic.Services.Interface
{
    public interface IBookingProcessService
    {
        Task<(BookingViewModel?, string?)> CreateBookingAsync(BookingVMCreateReserveFR request);
        Task<(IEnumerable<BookingViewModel>, int, int)> GetAllItemDataTablesAsync(BookingVMDataTableFR request);
        Task<EmployeeViewModel?> GetPicFilteredByBookingIdAsync(string bookingId);
        Task<ReturnalModel> CheckRescheduleDateAsync(string bookingId, DateOnly date, string roomId, int? defaultDuration = null);
        Task<ReturnalModel> RescheduleBookingAsync(BookingVMRescheduleFR request);
        Task<ReturnalModel> CancelBookingAsync(BookingVMCancelFR request);
        Task<ReturnalModel> CancelAllBookingAsync(BookingVMCancelFR request);
        Task<ReturnalModel> EndMeetingAsync(BookingVMEndMeetingFR request, bool fromApi = false);
        Task<ReturnalModel> CheckExtendMeetingTimeAsync(BookingVMCheckExtendMeetingFR request);
        Task<ReturnalModel> SetExtendMeetingAsync(BookingVMExtendMeetingFR request);
        Task<DataTableResponse> GetAllItemWithApprovalDataTablesAsync(BookingVMNeedApprovalDataTableFR request);
        Task<ReturnalModel> ProcessMeetingApprovalAsync(BookingVMApprovalFR request);
        Task<ReturnalModel> ConfirmAttendanceAsync(BookingVMConfirmAttendanceFR request);
        Task<ReturnalModel> AdditionalAttendeesAsync(BookingVMAdditionalAttendeesFR request);
        Task<ReturnalModel> GetOngoingBookingAsync();
        Task<ReturnalModel> CreateNewOrderAsync(BookingVMCreateNewOrderFR request);
    }
}
