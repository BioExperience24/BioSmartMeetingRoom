

namespace _6.Repositories.Repository;

public class BookingRepository : BaseLongRepository<Booking>
{

    private readonly MyDbContext _dbContext;

    public BookingRepository(MyDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<BookingChart>> GetAllItemChartAsync(int year)
    {
        var now = new DateTime(year, 1, 1);
        var startDate = now.AddMonths(-12);

        // Data untuk tabel virtual (subquery pertama)
        var dateRange = Enumerable.Range(0, 366)
                .Select(offset => now.AddDays(-offset))
                .Where(date => date <= now && date >= startDate)
                .Select(date => new
                {
                    Month = date.ToString("MMM"),
                    Md = date.ToString("MM-yyyy"),
                    Tahun = date.Year
                })
                .GroupBy(x => x.Md)
                .Select(g => new
                {
                    Month = g.First().Month,
                    Md = g.Key,
                    Amount = 0, // Jumlah awal
                    Tahun = g.First().Tahun
                });

        // Mengambil data dari Booking secara asinkron
        var bookingData = await (from booking in _dbContext.Bookings
                            where booking.Date <= DateOnly.FromDateTime(now) && booking.Date >= DateOnly.FromDateTime(startDate)
                            select new{
                                Month = booking.Date.ToString("MMM"),
                                Md = booking.Date.ToString("MM-yyyy"),
                                Tahun = booking.Date.Year,
                                Amount = (
                                    from b in _dbContext.Bookings
                                    where b.Date <= DateOnly.FromDateTime(now) && b.Date >= DateOnly.FromDateTime(startDate)
                                    select b
                                ).Count()
                            }).Distinct().ToListAsync();

        // Gabungkan dateRange dan bookingData
        var query = (from t1 in dateRange
                    join t2 in bookingData on t1.Md equals t2.Md into gj
                    from subT2 in gj.DefaultIfEmpty()
                    where t1.Tahun == year
                    orderby t1.Md
                    group new { t1, subT2 } by t1.Md into grouped
                    select new BookingChart
                    {
                        Month = grouped.First().t1.Month,
                        Md = grouped.Key,
                        Total = grouped.Sum(x => (x.subT2?.Amount ?? 0) + x.t1.Amount),
                        Tahun = grouped.First().t1.Tahun
                    });

        var result = query.ToList();

        return result;
    }

    
    public async Task<IEnumerable<object>> GetAllItemOngoingAsync(DateOnly startDate, DateOnly endDate, string? nik = null)
    {
        var query = from booking in _dbContext.Bookings
                    from room in _dbContext.Rooms
                        .Where(r => r.Radid == booking.RoomId).DefaultIfEmpty()
                    where booking.IsExpired == 0 && booking.Date >= startDate && booking.Date <= endDate
                    // where booking.IsExpired == 0
                    orderby booking.Start descending
                    select new { 
                        booking,
                        Name = room.Name,
                        Duration = 0
                        // Duration = (int)(booking.End - booking.Start).TotalMinutes
                        // Duration = (double)EF.Functions.DateDiffMinute(booking.Start, booking.End)
                    };

        if (!string.IsNullOrEmpty(nik))
        {
            query = from b in query
                    from bi in _dbContext.BookingInvitations
                        .Where(bi => b.booking.BookingId == bi.BookingId).DefaultIfEmpty()
                    where bi.Nik == nik && bi.Internal == 1
                    select b;
                    
        }

        var result = await query.ToListAsync();

        return result;
        // return new List<object>();
    }

    public async Task<int> GetCountAsync()
    {
        var count = await _dbContext.Bookings
                        .Where(q => q.IsDeleted == 0)
                        .CountAsync();

        return count;
    }
    
    public async Task<IEnumerable<Booking>> GetAllAvailableItemAsync(Booking? entity = null)
    {
        var query = from booking in _dbContext.Bookings
                    where booking.IsDeleted == 0
                    select booking;

        if (entity != null)
        {
            if (entity.RoomId != string.Empty)
            {
                query = query.Where(q => q.RoomId == entity.RoomId);
            }

            if (entity.Date != DateOnly.MinValue)
            {
                query = query.Where(q => q.Date == entity.Date);
            }

            if (entity.Start != DateTime.MinValue && entity.End != DateTime.MinValue)
            {
                /* query = query.Where(q => 
                    q.Start <= entity.Start
                    && q.End >= entity.End
                ); */

                query = query.Where(q => 
                    q.Start <= entity.End
                    && q.End >= entity.Start
                );
            }
        }

        var list = await query.ToListAsync();

        return list;
    }

    public async Task<(IEnumerable<object>, int, int)> GetAllItemWithEntityAsync(Booking? entity = null, int limit = 0, int offset = 0)
    {
        var participants = (from bookInvitation in _dbContext.BookingInvitations
                            where bookInvitation.IsDeleted == 0
                            select new {
                                BookingId = bookInvitation.BookingId,
                                Total = (
                                    from bi in _dbContext.BookingInvitations
                                    where bi.BookingId == bookInvitation.BookingId
                                    select bi
                                ).Count()
                            }).Distinct();

        var query = from booking in _dbContext.Bookings
                    from participant in participants
                        .Where(bi => bi.BookingId == booking.BookingId).DefaultIfEmpty()
                    where booking.IsExpired == 0 && booking.IsDeleted == 0
                    orderby booking.Start descending
                    select new { booking, Attendees = participant.Total };

        if (entity?.DateStart != null && entity?.DateEnd != null)
        {
            query = query.Where(q => 
                q.booking.Date >= entity.DateStart
                && q.booking.Date <= entity.DateEnd
            );
        }

        if (entity?.Pic != null)
        {
            query = query.Where(q => q.booking.Pic.Contains(entity.Pic));
        }

        if (entity?.BuildingId > 0)
        {
            query = from q in query
                    from room in _dbContext.Rooms
                        .Where(r => q.booking.RoomId == r.Radid).DefaultIfEmpty()
                    from building in _dbContext.Buildings
                        .Where(b => room.BuildingId == b.Id).DefaultIfEmpty()
                    where building.Id == entity.BuildingId
                    select q;
        }

        if (entity?.RoomId != null)
        {
            query = query.Where(q => q.booking.RoomId == entity.RoomId);
        }

        var recordsTotal = await query.CountAsync();

        if (limit > 0)
        {
            query = query
                    .Skip(offset)
                    .Take(limit);
        }

        var recordsFiltered = query.Count();

        var result = await query.ToListAsync();
        
        return (result, recordsTotal, recordsFiltered);
    }

    public async Task<IEnumerable<Booking>> GetBookingsByRoomIdsAndYearAsync(string[] roomId, int year)
    {
        var query = from booking in _dbContext.Bookings
                    where booking.IsDeleted == 0
                    && roomId.Contains(booking.RoomId)
                    && booking.Date.Year == year
                    select new Booking {
                        RoomId = booking.RoomId,
                        Date = booking.Date
                    };

        var result = await query.ToListAsync();
        return result;
    }
    
    public async Task<Booking?> GetByIdAsync(long id)
    {
        return await _context.Bookings.FindAsync(id);
    }

    public async Task<Booking?> AddAsync(Booking booking)
    {
        var result = await _context.Bookings.AddAsync(booking);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<Booking?> UpdateAsync(Booking booking)
    {
        _context.Bookings.Update(booking);
        await _context.SaveChangesAsync();
        return booking;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var booking = await _context.Bookings.FindAsync(id);
        if (booking == null) return false;

        booking.IsDeleted = 1;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Booking>> GetDataBookingAsync(DateTime start, DateTime end)
    {
        return await _context.Bookings
            .Where(b => b.Date >= DateOnly.FromDateTime(start) && b.Date <= DateOnly.FromDateTime(end) && b.IsDeleted == 0)
            .Select(b => new Booking
            {
                Id = b.Id,
                BookingId = b.BookingId,
                Title = b.Title,
                Date = b.Date,
                RoomId = b.RoomId,
                RoomName = b.RoomName,
                Start = b.Start,
                End = b.End,
                Pic = b.Pic,
                Note = b.Note,
                Participants = b.Participants
            })
            .OrderByDescending(b => b.Date)
            .ToListAsync();
    }

    public async Task<IEnumerable<Booking>> GetDataBookingByNikAsync(DateTime start, DateTime end, string nik)
    {
        return await _context.Bookings
            .Where(b => b.Date >= DateOnly.FromDateTime(start) && b.Date <= DateOnly.FromDateTime(end) && b.Pic == nik && b.IsDeleted == 0)
            .Select(b => new Booking
            {
                Id = b.Id,
                BookingId = b.BookingId,
                Title = b.Title,
                Date = b.Date,
                RoomId = b.RoomId,
                RoomName = b.RoomName,
                Start = b.Start,
                End = b.End,
                Pic = b.Pic,
                Note = b.Note,
                Participants = b.Participants
            })
            .OrderByDescending(b => b.Date)
            .ToListAsync();
    }
}

