
namespace _3.BusinessLogic.Services.Interface
{
    public interface IBookingService : IBaseLongService<BookingViewModel>
    {
        Task<int> GetCountAsync();
        Task<(IEnumerable<BookingViewModel>, int, int)> GetAllItemDataTablesAsync(BookingVMDataTableFR request);
    }
}