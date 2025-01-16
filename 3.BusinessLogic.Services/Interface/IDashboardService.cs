

namespace _3.BusinessLogic.Services.Interface
{
    public interface IDashboardService
    {
        Task<IEnumerable<RoomVMChartTopRoom>> GetAllChartTopFiveRoomAsync(int year);
        Task<IEnumerable<BookingVMChart>> GetAllChartBookingAsync(int year);
        Task<IEnumerable<BookingViewModel>> GetAllOngoingBookingAsync(DateOnly startDate, DateOnly endDate, string? nik = null);
    }
}