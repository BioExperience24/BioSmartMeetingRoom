
namespace _3.BusinessLogic.Services.Interface
{
    public interface IBookingService : IBaseLongService<BookingViewModel>
    {
        Task<IEnumerable<BookingViewModel>> GetDataBookingAsync(DateTime start, DateTime end);

        Task<IEnumerable<BookingViewModel>> GetDataBookingByNikAsync(DateTime start, DateTime end, string nik);


        // called on page load
        Task<BookingMenuDetailViewModel> GetDataBooking();
    }
}