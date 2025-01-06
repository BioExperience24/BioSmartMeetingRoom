

using System.Text.Json;

namespace _6.Repositories.Repository;

public class RoomRepository : BaseLongRepository<Room>
{

    private readonly MyDbContext _dbContext;
        
    public RoomRepository(MyDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<object>> GetAllChartTopFiveRoom4thAltAsync(int year)
    {
        var query = await _dbContext.Bookings
            .Where(b => b.Date.Year == year && b.IsDeleted == 0)
            .GroupBy(b => b.RoomId)
            .Select(g => new
            {
                RoomName = _dbContext.Rooms.FirstOrDefault(r => r.Radid == g.Key)!.Name,
                MonthlyCounts = g.GroupBy(b => b.Date.Month)
                                .Select(m => new { Month = m.Key, Count = m.Count() })
                                .ToDictionary(m => m.Month, m => m.Count)
            })
            .Select(r => new
            {
                r.RoomName,
                January = r.MonthlyCounts.ContainsKey(1) ? r.MonthlyCounts[1] : 0,
                February = r.MonthlyCounts.ContainsKey(2) ? r.MonthlyCounts[2] : 0,
                March = r.MonthlyCounts.ContainsKey(3) ? r.MonthlyCounts[3] : 0,
                April = r.MonthlyCounts.ContainsKey(4) ? r.MonthlyCounts[4] : 0,
                May = r.MonthlyCounts.ContainsKey(5) ? r.MonthlyCounts[5] : 0,
                June = r.MonthlyCounts.ContainsKey(6) ? r.MonthlyCounts[6] : 0,
                July = r.MonthlyCounts.ContainsKey(7) ? r.MonthlyCounts[7] : 0,
                August = r.MonthlyCounts.ContainsKey(8) ? r.MonthlyCounts[8] : 0,
                September = r.MonthlyCounts.ContainsKey(9) ? r.MonthlyCounts[9] : 0,
                October = r.MonthlyCounts.ContainsKey(10) ? r.MonthlyCounts[10] : 0,
                November = r.MonthlyCounts.ContainsKey(11) ? r.MonthlyCounts[11] : 0,
                December = r.MonthlyCounts.ContainsKey(12) ? r.MonthlyCounts[12] : 0,
                Total = r.MonthlyCounts.Values.Sum()
            })
            .OrderByDescending(r => r.Total)
            .Take(5)
            .ToListAsync();
        
        return query;
    }

    public async Task<IEnumerable<object>> GetAllChartTopFiveRoom3rdAltAsync(int year)
    {
        // Mengambil data booking yang relevan dari database
        var bookingsGroupedByRoomAndMonth = await _dbContext.Bookings
            .Where(booking => booking.Date.Year == year && booking.IsDeleted == 0)
            .GroupBy(booking => new { booking.RoomId, Month = booking.Date.Month })
            .Select(group => new
            {
                group.Key.RoomId,
                group.Key.Month,
                Count = group.Count()
            })
            .ToListAsync();

        // Jika tidak ada data booking, kembalikan daftar kosong
        if (!bookingsGroupedByRoomAndMonth.Any())
        {
            return Enumerable.Empty<object>();
        }

        // Mengambil data ruangan dan mengaitkan dengan jumlah booking per bulan
        var result = await _dbContext.Rooms
            .Select(room => new
            {
                room.Name,
                January = bookingsGroupedByRoomAndMonth.FirstOrDefault(bg => bg.RoomId == room.Radid && bg.Month == 1)!.Count,
                February = bookingsGroupedByRoomAndMonth.FirstOrDefault(bg => bg.RoomId == room.Radid && bg.Month == 2)!.Count,
                Maret = bookingsGroupedByRoomAndMonth.FirstOrDefault(bg => bg.RoomId == room.Radid && bg.Month == 3)!.Count,
                April = bookingsGroupedByRoomAndMonth.FirstOrDefault(bg => bg.RoomId == room.Radid && bg.Month == 4)!.Count,
                May = bookingsGroupedByRoomAndMonth.FirstOrDefault(bg => bg.RoomId == room.Radid && bg.Month == 5)!.Count,
                June = bookingsGroupedByRoomAndMonth.FirstOrDefault(bg => bg.RoomId == room.Radid && bg.Month == 6)!.Count,
                July = bookingsGroupedByRoomAndMonth.FirstOrDefault(bg => bg.RoomId == room.Radid && bg.Month == 7)!.Count,
                August = bookingsGroupedByRoomAndMonth.FirstOrDefault(bg => bg.RoomId == room.Radid && bg.Month == 8)!.Count,
                September = bookingsGroupedByRoomAndMonth.FirstOrDefault(bg => bg.RoomId == room.Radid && bg.Month == 9)!.Count,
                October = bookingsGroupedByRoomAndMonth.FirstOrDefault(bg => bg.RoomId == room.Radid && bg.Month == 10)!.Count,
                November = bookingsGroupedByRoomAndMonth.FirstOrDefault(bg => bg.RoomId == room.Radid && bg.Month == 11)!.Count,
                December = bookingsGroupedByRoomAndMonth.FirstOrDefault(bg => bg.RoomId == room.Radid && bg.Month == 12)!.Count
            })
            .OrderByDescending(room => room.January + room.February + room.Maret + room.April + room.May + room.June + room.July + room.August + room.September + room.October + room.November + room.December)
            .Take(5)
            .ToListAsync();

        return result;
    }

    public async Task<IEnumerable<object>> GetAllChartTopFiveRoom2ndAltAsync(int year)
    {
        var bookingsGroupedByRoomAndMonth = await _dbContext.Bookings
                .Where(booking => booking.Date.Year == year && booking.IsDeleted == 0)
                .GroupBy(booking => new { booking.RoomId, Month = booking.Date.Month })
                .Select(group => new
                {
                    group.Key.RoomId,
                    group.Key.Month,
                    Count = group.Count()
                })
                .ToListAsync();
        

        var result = _dbContext.Rooms
            .Select(room => new
            {
                room.Name,
                MonthlyCounts = Enumerable.Range(1, 12)
                    .Select(month => new
                    {
                        Month = month,
                        Count = bookingsGroupedByRoomAndMonth
                                    .Where(bg => bg.RoomId == room.Radid && bg.Month == month)
                                    .Select(bg => bg.Count)
                                    .FirstOrDefault() // Perbaikan: Hindari null-propagating
                    })
                    .ToList()
            })
            .AsEnumerable() // Switch to LINQ to Objects for in-memory computation
            .Select(room => new
            {
                room.Name,
                January = room.MonthlyCounts.First(mc => mc.Month == 1).Count,
                February = room.MonthlyCounts.First(mc => mc.Month == 2).Count,
                March = room.MonthlyCounts.First(mc => mc.Month == 3).Count,
                April = room.MonthlyCounts.First(mc => mc.Month == 4).Count,
                May = room.MonthlyCounts.First(mc => mc.Month == 5).Count,
                June = room.MonthlyCounts.First(mc => mc.Month == 6).Count,
                July = room.MonthlyCounts.First(mc => mc.Month == 7).Count,
                August = room.MonthlyCounts.First(mc => mc.Month == 8).Count,
                September = room.MonthlyCounts.First(mc => mc.Month == 9).Count,
                October = room.MonthlyCounts.First(mc => mc.Month == 10).Count,
                November = room.MonthlyCounts.First(mc => mc.Month == 11).Count,
                December = room.MonthlyCounts.First(mc => mc.Month == 12).Count
            })
            .OrderByDescending(room => room.January + room.February + room.March + room.April + room.May + room.June + room.July + room.August + room.September + room.October + room.November + room.December)
            .Take(5);

        return result;
    }

    public async Task<IEnumerable<object>> GetAllChartTopFiveRoom1stAltAsync(int year)
    {
        var query = (from room in _dbContext.Rooms
                    select new {
                        room.Name,
                        January = (from booking in _dbContext.Bookings where booking.RoomId == room.Radid && booking.Date.Year == year && booking.Date.Month == 1 && booking.IsDeleted == 0 select booking).Count(),
                        February = (from booking in _dbContext.Bookings where booking.RoomId == room.Radid && booking.Date.Year == year && booking.Date.Month == 2 && booking.IsDeleted == 0 select booking).Count(),
                        Maret = (from booking in _dbContext.Bookings where booking.RoomId == room.Radid && booking.Date.Year == year && booking.Date.Month == 3 && booking.IsDeleted == 0 select booking).Count(),
                        April = (from booking in _dbContext.Bookings where booking.RoomId == room.Radid && booking.Date.Year == year && booking.Date.Month == 4 && booking.IsDeleted == 0 select booking).Count(),
                        May = (from booking in _dbContext.Bookings where booking.RoomId == room.Radid && booking.Date.Year == year && booking.Date.Month == 5 && booking.IsDeleted == 0 select booking).Count(),
                        June = (from booking in _dbContext.Bookings where booking.RoomId == room.Radid && booking.Date.Year == year && booking.Date.Month == 6 && booking.IsDeleted == 0 select booking).Count(),
                        July = (from booking in _dbContext.Bookings where booking.RoomId == room.Radid && booking.Date.Year == year && booking.Date.Month == 7 && booking.IsDeleted == 0 select booking).Count(),
                        August = (from booking in _dbContext.Bookings where booking.RoomId == room.Radid && booking.Date.Year == year && booking.Date.Month == 8 && booking.IsDeleted == 0 select booking).Count(),
                        September = (from booking in _dbContext.Bookings where booking.RoomId == room.Radid && booking.Date.Year == year && booking.Date.Month == 9 && booking.IsDeleted == 0 select booking).Count(),
                        October = (from booking in _dbContext.Bookings where booking.RoomId == room.Radid && booking.Date.Year == year && booking.Date.Month == 10 && booking.IsDeleted == 0 select booking).Count(),
                        November = (from booking in _dbContext.Bookings where booking.RoomId == room.Radid && booking.Date.Year == year && booking.Date.Month == 11 && booking.IsDeleted == 0 select booking).Count(),
                        December = (from booking in _dbContext.Bookings where booking.RoomId == room.Radid && booking.Date.Year == year && booking.Date.Month == 12 && booking.IsDeleted == 0 select booking).Count()
                    })
                    .OrderByDescending(r => r.January + r.February + r.Maret + r.April + r.May + r.June + r.July + r.August + r.September + r.October + r.November + r.December)
                    .Take(5);


        var result = await query.ToListAsync();

        return result;
    }

     // Dipakai di Master/Base - Access Door
    public async Task<IEnumerable<object>> GetAllRoomItemAsync(bool withIsDisabled = true)
    {
        var query = from room in _dbContext.Rooms
                    from roomAutomation in _dbContext.RoomAutomations
                            .Where(q => q.Id == room.AutomationId).DefaultIfEmpty()
                    from building in _dbContext.Buildings
                            .Where(q => q.Id == room.BuildingId).DefaultIfEmpty()
                    where room.IsDeleted == 0
                    orderby room.Name ascending
                    select new {
                        room,
                        roomAutomation = new { 
                            RaName = roomAutomation != null ? roomAutomation.Name : "",
                            RaId = roomAutomation != null ? roomAutomation.Id : null },
                        building = new { 
                            BuildingName = building != null ? building.Name : "", 
                            BuildingDetail = building != null ? building.DetailAddress : "",
                            BuildingGoogleMap = building != null ? building.GoogleMap : "" }
                    };

        if (!withIsDisabled)
        {
            query = query.Where(q => q.room.IsDisabled == 0);
        }

        var list = await query.ToListAsync();
        
        return list;
    }

    public async Task<int> GetCountRoomItemAsync(bool withIsDisabled = true)
    {
        var query = from room in _dbContext.Rooms
                    from roomAutomation in _dbContext.RoomAutomations
                            .Where(q => q.Id == room.AutomationId).DefaultIfEmpty()
                    from building in _dbContext.Buildings
                            .Where(q => q.Id == room.BuildingId).DefaultIfEmpty()
                    where room.IsDeleted == 0
                    orderby room.Name ascending
                    select new {
                        room,
                        roomAutomation = new { 
                            RaName = roomAutomation != null ? roomAutomation.Name : "",
                            RaId = roomAutomation != null ? roomAutomation.Id : null },
                        building = new { 
                            BuildingName = building != null ? building.Name : "", 
                            BuildingDetail = building != null ? building.DetailAddress : "",
                            BuildingGoogleMap = building != null ? building.GoogleMap : "" }
                    };

        if (!withIsDisabled)
        {
            query = query.Where(q => q.room.IsDisabled == 0);
        }

        var count = await query.CountAsync();
        
        return count;
    }

    // Dipakai di Master/Base - Display Signage
    public async Task<IEnumerable<object>> GetAllRoomRoomDisplayItemAsync()
    {
        var query = from room in _dbContext.Rooms
                    from roomDisplay in _dbContext.RoomDisplays
                            .Where(q => q.RoomId == room.Radid).DefaultIfEmpty()
                    from building in _dbContext.Buildings
                            .Where(q => q.Id == room.BuildingId).DefaultIfEmpty()
                    where room.IsDeleted == 0
                    orderby room.Name ascending
                    select new {
                        room,
                        roomDisplay = new {
                            RoomDisplayBackground = roomDisplay != null ? roomDisplay.Background : ""
                        },
                        building = new { 
                            BuildingName = building != null ? building.Name : "", 
                            BuildingDetail = building != null ? building.DetailAddress : "",
                            BuildingGoogleMap = building != null ? building.GoogleMap : "" }
                    };

        var list = await query
                            .GroupBy(q => q.room.Radid)
                            .Select(q => q.FirstOrDefault())
                            .ToListAsync();
        
        return list!;
    }

    // Dipakai di Master/Base - Display Signage
    public async Task<IEnumerable<Room>> GetAllRoomWithRadidsItemAsycn(string[] radIds)
    {
        var query = from room in _dbContext.Rooms
                    where room.IsDeleted == 0
                    // && radIds.Any(radid => room.Radid.Contains(radid))
                    && radIds.Contains(room.Radid)
                    orderby room.Name ascending
                    select room;

        var list = await query.ToListAsync();
        
        return list;
    }
    
    public async Task<Room?> UpdateRoom(Room entity)
    {
        var existingEntity = await _dbContext.Rooms.FindAsync(entity.Id);
        if (existingEntity == null) return null; // Handle if entity not found

        _dbContext.Entry(existingEntity).CurrentValues.SetValues(entity); // Update all properties
        await _dbContext.SaveChangesAsync(); // Save changes
        return existingEntity; // Return the updated entity
    }

}

