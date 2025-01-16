

namespace _3.BusinessLogic.Services.Interface
{
    public interface IBookingProcessService
    {
        Task<(BookingViewModel?, string?)> CreateBookingAsync(BookingVMCreateReserveFR request);
    }
}