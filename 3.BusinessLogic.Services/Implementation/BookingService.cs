
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using _5.Helpers.Consumer.EnumType;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using SkiaSharp;
using System.Drawing;
using System.Net.Http.Headers;

namespace _3.BusinessLogic.Services.Implementation;

public class BookingService : BaseLongService<BookingViewModel, Booking>, IBookingService
{
    private readonly IMapper __mapper;
    private readonly IHttpContextAccessor _context;
    private readonly BookingRepository _repo;
    private readonly BookingInvitationRepository _repoBI;
    private readonly PantryDetailRepository _repoPD;
    private readonly EmployeeRepository _repoEmp;

    public BookingService(
        IMapper mapper,
        IHttpContextAccessor context,
        BookingRepository repo,
        BookingInvitationRepository repoBI,
        PantryDetailRepository repoPD
    )
        : base(repo, mapper)
    {
        _context = context;
        __mapper = mapper;
        _repo = repo;
        _repoBI = repoBI;
        _repoPD = repoPD;

    }

    public async Task<int> GetCountAsync()
    {
        return await _repo.GetCountAsync();
    }

    // public async Task<(IEnumerable<BookingViewModel>, int, int)> GetAllItemDataTablesAsync(BookingVMDataTableFR request)
    // {
    //     var entity = new Booking { };

    //     if (request.BookingDate != null)
    //     {
    //         string[] dates = request.BookingDate.Split(" - ");
    //         entity.DateStart = _String.ToDateOnly(_String.RemoveAllSpace(dates[0]), "MM/dd/yyyy");
    //         entity.DateEnd = _String.ToDateOnly(_String.RemoveAllSpace(dates[1]), "MM/dd/yyyy");
    //     }

    //     if (request.BookingOrganizer != null)
    //     {
    //         entity.Pic = request.BookingOrganizer;
    //     }

    //     if (request.BookingRoom != null)
    //     {
    //         entity.RoomId = request.BookingRoom;
    //     }

    //     if (request.BookingBuilding > 0)
    //     {
    //         entity.BuildingId = request.BookingBuilding;
    //     }

    //     var (items, recordsTotal, recordsFiltered) = await _repo.GetAllItemWithEntityAsync(entity, request.Length, request.Start);

    //     var result = _mapper.Map<List<BookingViewModel>>(items);

    //     var bookingIds = result.Select(q => q.BookingId).ToArray();

    //     var bookingInvitations = await _repoBI.GetAllFilteredByBookingIds(bookingIds);

    //     var BookingMenus = await _repoPD.GetAllFilteredByBookingIds(bookingIds);

    //     var no = request.Start + 1;

    //     foreach (var item in result)
    //     {
    //         item.No = no;
    //         item.BookingDate = item.Date.ToString("dd MMM yyyy");

    //         var timeStart = item.Start.ToString("HH:mm");
    //         var timeEnd = item.End.ToString("HH:mm");
    //         item.Time = $"{timeStart} - {timeEnd}";

    //         var externalAttendees = bookingInvitations
    //                                         .Where(q => q.BookingId == item.BookingId && q.Internal == 0)
    //                                         .ToList();
    //         List<BookingInvitationVMCategory> MapExternalAttendees = new();
    //         _mapper.Map(externalAttendees, MapExternalAttendees);

    //         var internalAttendees = bookingInvitations
    //                                         .Where(q => q.BookingId == item.BookingId && q.Internal == 1)
    //                                         .ToList();

    //         List<BookingInvitationVMCategory> MapInternalAttendees = new();
    //         _mapper.Map(internalAttendees, MapInternalAttendees);

    //         item.AttendeesList = new BookingInvitationVMList
    //         {
    //             ExternalAttendess = MapExternalAttendees,
    //             InternalAttendess = MapInternalAttendees
    //         };

    //         var menus = BookingMenus.Where(q => item.BookingId == q.BookingId).ToList();

    //         item.PackageId = menus.Select(q => q.PackageId).FirstOrDefault() ?? string.Empty;

    //         item.PackageMenus = _mapper.Map<List<PantryDetailVMMenus>>(menus);

    //         no++;
    //     }

    //     return (result, recordsTotal, recordsFiltered);
    // }



}

