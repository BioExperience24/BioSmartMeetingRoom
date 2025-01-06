
namespace _3.BusinessLogic.Services.Interface
{
    public interface IBookingService : IBaseLongService<BookingViewModel>
    {
        Task<IEnumerable<BookingVMChart>> GetItemChartsAsync(int year);
        Task<IEnumerable<BookingViewModel>> GetItemOngoingAsync(DateOnly startDate, DateOnly endDate, string? nik = null);
        Task<int> GetCountAsync();

        Task<IEnumerable<BookingViewModel>> GetDataBookingAsync(DateTime start, DateTime end);

        Task<IEnumerable<BookingViewModel>> GetDataBookingByNikAsync(DateTime start, DateTime end, string nik);


        // called on page load
        Task<BookingMenuDetailViewModel> GetDataBooking();
    }
}