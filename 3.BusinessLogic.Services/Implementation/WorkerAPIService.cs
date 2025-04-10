using _7.Entities.Models;

namespace _3.BusinessLogic.Services.Implementation;

public class WorkerAPIService(
        BookingRepository bookingRepo
    )
    : IWorkerAPIService
{
    public async Task<ReturnalModel> CheckMeetingToday(DateOnly? dateNow)
    {
        ReturnalModel ret = new();
        dateNow ??= DateOnly.FromDateTime(DateTime.UtcNow);

        var getBooking = await bookingRepo.OpenDataMeetingAsync(dateNow.Value);

        ret.Collection = getBooking;
        return ret;
    }
}