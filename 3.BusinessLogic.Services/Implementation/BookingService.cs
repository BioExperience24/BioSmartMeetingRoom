using System.Transactions;
using _5.Helpers.Consumer.EnumType;

namespace _3.BusinessLogic.Services.Implementation;

public class BookingService : BaseLongService<BookingViewModel, Booking>, IBookingService
{
    private readonly IMapper __mapper;
    
    private readonly BookingRepository _repo;

    public BookingService( IMapper mapper, BookingRepository repo) 
        : base(repo, mapper)
    { 
        __mapper = mapper;
        _repo = repo;
    }

    public async Task<int> GetCountAsync()
    {
        return await _repo.GetCountAsync();
    }
    
    public async Task<(IEnumerable<BookingViewModel>, int, int)> GetAllItemDataTablesAsync(BookingVMDataTableFR request) 
    {
        var entity = new Booking{};

        if (request.BookingDate != null)
        {
            string[] dates = request.BookingDate.Split(" - ");
            entity.DateStart = _String.ToDateOnly(_String.RemoveAllSpace(dates[0]), "MM/dd/yyyy");
            entity.DateEnd = _String.ToDateOnly(_String.RemoveAllSpace(dates[1]), "MM/dd/yyyy");
        }

        if (request.BookingOrganizer != null)
        {
            entity.Pic = request.BookingOrganizer;
        }

        if (request.BookingRoom != null)
        {
            entity.RoomId = request.BookingRoom;
        }

        if (request.BookingBuilding > 0)
        {
            entity.BuildingId = request.BookingBuilding;
        }

        var (items, recordsTotal, recordsFiltered) = await _repo.GetAllItemWithEntityAsync(entity, request.Length, request.Start);

        var result = _mapper.Map<List<BookingViewModel>>(items);

        var no = request.Start + 1;

        foreach (var item in result)
        {
            item.No = no;
            item.BookingDate = item.Date.ToString("dd MMM yyyy");

            var timeStart = item.Start.ToString("HH:mm");
            var timeEnd = item.End.ToString("HH:mm");
            item.Time = $"{timeStart} - {timeEnd}";
            
            no++;
        }

        return (result, recordsTotal, recordsFiltered);
    }
}

