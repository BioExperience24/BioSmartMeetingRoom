

namespace _3.BusinessLogic.Services.Interface
{
    public interface IReportService
    {
        Task<BookingVMRoomUsageData> GetAllRoomUsageReportItemAsync(BookingVMRoomUsageFR request);
        Task<EmployeeVMOrganizerData> GetAllOrganizerUsageReportItemAsync(EmployeeVMOrganizerFR request);
        Task<EmployeeVMAttendeesData> GetAllAttendeesReportItemAsync(EmployeeVMOrganizerFR request);
    }
}