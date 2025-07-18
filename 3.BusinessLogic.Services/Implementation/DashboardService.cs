

using System.Security.Claims;
using _5.Helpers.Consumer.EnumType;
using Microsoft.AspNetCore.Http.Features;

namespace _3.BusinessLogic.Services.Implementation
{
    public class DashboardService : IDashboardService
    {
        private readonly IMapper _mapper;
        private readonly RoomRepository _roomRepo;
        private readonly BookingRepository _bookingRepo;
        IHttpContextAccessor _context;

        public DashboardService(
            IMapper mapper, 
            RoomRepository roomRepo,
            BookingRepository bookingRepo,
            IHttpContextAccessor context
        )
        {
            _mapper = mapper;
            _roomRepo = roomRepo;
            _bookingRepo = bookingRepo;
            _context = context;
        }
    
    
        public async Task<IEnumerable<RoomVMChartTopRoom>> GetAllChartTopFiveRoomAsync(int year)
        {
            var items = await _roomRepo.GetAllChartTopFiveRoomAsync(year);

            if (!items.Any())
            {
                return Enumerable.Empty<RoomVMChartTopRoom>();
            }

            string[] roomIds = items.Select(r => r.Radid).ToArray();

            // Fetch bookings in a single query
            var bookings = await _bookingRepo.GetBookingsByRoomIdsAndYearAsync(roomIds, year);

            // Group bookings by RoomId, Year, and Month
            var groupedBookings = bookings
                .GroupBy(b => new { b.RoomId, b.Date.Year, b.Date.Month })
                .ToDictionary(
                    g => (g.Key.RoomId, g.Key.Month),
                    g => g.Count()
                );

            // Map data to view models
            var result = items.Select(item =>
            {
                var roomVM = _mapper.Map<RoomVMChartTopRoom>(item);
                roomVM.Radid = null;

                // Initialize months dynamically
                var monthlyCounts = new int[12];
                for (int i = 0; i < 12; i++)
                {
                    monthlyCounts[i] = groupedBookings.TryGetValue((item.Radid!, i + 1), out var count) ? count : 0;
                }

                roomVM.January = monthlyCounts[0];
                roomVM.February = monthlyCounts[1];
                roomVM.Maret = monthlyCounts[2];
                roomVM.April = monthlyCounts[3];
                roomVM.May = monthlyCounts[4];
                roomVM.June = monthlyCounts[5];
                roomVM.July = monthlyCounts[6];
                roomVM.August = monthlyCounts[7];
                roomVM.September = monthlyCounts[8];
                roomVM.October = monthlyCounts[9];
                roomVM.November = monthlyCounts[10];
                roomVM.December = monthlyCounts[11];

                return roomVM;
            }).ToList();

            return result;
        }   
    
        public async Task<IEnumerable<BookingVMChart>> GetAllChartBookingAsync(int year)
        {
            var items = await _bookingRepo.GetAllItemChartAsync(year);

            var result = _mapper.Map<List<BookingVMChart>>(items);

            return result;
        }

        public async Task<IEnumerable<BookingViewModel>> GetAllOngoingBookingAsync(DateOnly startDate, DateOnly endDate, string? nik = null)
        {
            var role = _context?.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;

            if ( role != EnumLevelRole.Administrator.ToString() && role != EnumLevelRole.SOHelpdesk.ToString() && role != EnumLevelRole.SuperAdmin.ToString())
            {

                var userNik = _context?.HttpContext?.User?.FindFirst(ClaimTypes.UserData)?.Value;
                nik ??= userNik;
            }

            var items = await _bookingRepo.GetAllItemOngoingAsync(startDate, endDate, nik);

            var result = _mapper.Map<List<BookingViewModel>>(items);
            if (result.Any())
            {
                foreach (var item in result)
                {
                    if (item.Start != DateTime.MinValue && item.End != DateTime.MinValue)
                    {
                        // Menghitung selisih waktu
                        TimeSpan difference = item.End.Subtract(item.Start);
                        // Mendapatkan total menit
                        item.Duration = difference.TotalMinutes;
                    }
                }
            }

            return result;
        }
    }
}