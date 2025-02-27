

using System.Globalization;

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
                                 select new
                                 {
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
                    select new
                    {
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
                    && booking.IsCanceled == 0
                    && booking.EndEarlyMeeting == 0
                    select booking;

        if (entity != null)
        {
            if (entity.BookingId != string.Empty)
            {
                query = query.Where(q => q.BookingId != entity.BookingId);
            }

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
                            select new
                            {
                                BookingId = bookInvitation.BookingId,
                                Total = (bookInvitation.BookingId == null)
                                ? (int?)null
                                : (
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
                    select new
                    {
                        booking,
                        Attendees = participant.Total != null ? participant.Total : 0
                    };

        if (entity?.DateStart != null && entity?.DateEnd != null)
        {
            query = query.Where(q =>
                q.booking.Date >= entity.DateStart
                && q.booking.Date <= entity.DateEnd
            );
        }

        if (entity?.AuthUserNIK != null)
        {
            query = from q in query
                    from bi in _dbContext.BookingInvitations
                        .Where(bi => q.booking.BookingId == bi.BookingId && bi.Nik == entity.AuthUserNIK).DefaultIfEmpty()
                    where bi.Nik == entity.AuthUserNIK || q.booking.CreatedBy == entity.AuthUserNIK
                    select q;
        }

        var recordsTotal = await query.CountAsync();

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

        var recordsFiltered = await query.CountAsync();

        if (limit > 0)
        {
            query = query
                    .Skip(offset)
                    .Take(limit);
        }

        var result = await query.ToListAsync();

        return (result, recordsTotal, recordsFiltered);
    }

    public async Task<BookingDataTable> GetAllItemReportUsageAsync(Booking? entity = null, int limit = 0, int offset = 0)
    {
        var participants = (from bookInvitation in _dbContext.BookingInvitations
                            where bookInvitation.IsDeleted == 0
                            select new
                            {
                                BookingId = bookInvitation.BookingId,
                                Total = (int?)(
                                    from bi in _dbContext.BookingInvitations
                                    where bi.BookingId == bookInvitation.BookingId
                                    select bi
                                ).Count()
                            }).Distinct();

        var query = from booking in _dbContext.Bookings
                    from participant in participants
                        .Where(p => booking.BookingId == p.BookingId).DefaultIfEmpty()
                    from bookingInvoice in _dbContext.BookingInvoices
                        .Where(binv => booking.BookingId == binv.BookingId).DefaultIfEmpty()
                    from room in _dbContext.Rooms
                        .Where(r => booking.RoomId == r.Radid)
                    from building in _dbContext.Buildings
                        .Where(bu => room.BuildingId == bu.Id)
                    from bookingInvitation in _dbContext.BookingInvitations
                        .Where(bi => booking.BookingId == bi.BookingId)
                    from alocation in _dbContext.Alocations
                        .Where(a => booking.AlocationId == a.Id).DefaultIfEmpty()
                    from alocationType in _dbContext.AlocationTypes
                        .Where(at => alocation.Type == at.Id).DefaultIfEmpty()
                    from employee in _dbContext.Employees
                        .Where(e => bookingInvitation.Nik == e.Nik).DefaultIfEmpty()
                    orderby booking.Start descending
                    select new BookingReportUsage
                    {
                        Booking = booking,
                        Building = building,
                        Attendees = (participant.Total != null) ? participant.Total : 0,
                        RoomName = room.Name,
                        RoomLocation = room.Location,
                        MemoNo = bookingInvoice.MemoNo,
                        ReferensiNo = bookingInvoice.ReferensiNo,
                        InvoiceStatus = bookingInvoice.InvoiceStatus,
                        AlocationName = alocation.Name,
                        AlocationType = alocation.Type,
                        AlocationInvoiceStatus = alocation.InvoiceStatus,
                        AlocationTypeInvoiceStatus = alocationType.InvoiceStatus,
                        NameEmployee = employee.Name,
                        EmailEmployee = employee.Email,
                        PhoneEmployee = employee.NoPhone,
                        ExtEmployee = employee.NoExt
                    };

        if (entity?.DateStart != null && entity?.DateEnd != null)
        {
            query = query.Where(q =>
                q.Booking!.Date >= entity.DateStart
                && q.Booking!.Date <= entity.DateEnd
            );
        }

        var recordsTotal = await query.CountAsync();


        if (entity?.BuildingId > 0)
        {
            query = query.Where(q => q.Building!.Id == entity.BuildingId);
        }

        if (entity?.RoomId != null)
        {
            query = query.Where(q => q.Booking!.RoomId == entity.RoomId);
        }

        if (entity?.AlocationId != null)
        {
            query = query.Where(q => q.Booking!.AlocationId == entity.AlocationId);
        }

        var recordsFiltered = await query.CountAsync();

        if (limit > 0)
        {
            query = query
                    .Skip(offset)
                    .Take(limit);
        }

        var result = await query.ToListAsync();

        return new BookingDataTable
        {
            Collection = result,
            RecordsTotal = recordsTotal,
            RecordsFiltered = recordsFiltered
        };
    }

    public async Task<IEnumerable<Booking>> GetBookingsByRoomIdsAndYearAsync(string[] roomId, int year)
    {
        var query = from booking in _dbContext.Bookings
                    where booking.IsDeleted == 0
                    && roomId.Contains(booking.RoomId)
                    && booking.Date.Year == year
                    select new Booking
                    {
                        RoomId = booking.RoomId,
                        Date = booking.Date
                    };

        var result = await query.ToListAsync();
        return result;
    }

    public async Task<IEnumerable<Booking>> GetBookingsByRoomIdsAndDateAsync(string[] roomId, DateOnly date, bool withIsAlive = false)
    {
        var query = from booking in _dbContext.Bookings
                    where booking.IsDeleted == 0
                    && booking.IsCanceled == 0
                    && booking.EndEarlyMeeting == 0
                    && roomId.Contains(booking.RoomId)
                    && booking.Date == date
                    select booking;
        // select new Booking {
        //     RoomId = booking.RoomId,
        //     Date = booking.Date,
        //     Start = booking.Start,
        //     End = booking.End,
        // };

        if (withIsAlive)
        {
            query = query.Where(q => q.IsAlive == 1);
        }

        query = query.Select(booking => new Booking
        {
            RoomId = booking.RoomId,
            Date = booking.Date,
            Start = booking.Start,
            End = booking.End,
            ExtendedDuration = booking.ExtendedDuration,
        });

        var result = await query.ToListAsync();
        return result;
    }

    public async Task<int?> GetCountTotalDurationByPicAsync(BookingFilter? filter = null)
    {
        if (filter?.Nik == null)
        {
            return 0;
        }

        var invitationBookingIds = from bi in _dbContext.BookingInvitations
                                   from b in _dbContext.Bookings.Where(b => bi.BookingId == b.BookingId)
                                   from r in _dbContext.Rooms.Where(r => b.RoomId == r.Radid)
                                   from bu in _dbContext.Buildings.Where(bu => r.BuildingId == bu.Id)
                                   where bi.IsPic == 1
                                   && bi.Internal == 1
                                   && b.IsAlive != 0
                                   select new { bi, b, r, bu };

        // NIK PIC
        invitationBookingIds = invitationBookingIds.Where(q => q.bi!.Nik == filter.Nik);

        if (filter?.DateStart != null && filter?.DateEnd != null)
        {
            invitationBookingIds = invitationBookingIds.Where(q =>
                q.b!.Date >= filter.DateStart
                && q.b!.Date <= filter.DateEnd
            );
        }

        if (filter?.BuildingId > 0)
        {
            invitationBookingIds = invitationBookingIds.Where(q => q.bu!.Id == filter.BuildingId);
        }

        if (filter?.RoomId != null)
        {
            invitationBookingIds = invitationBookingIds.Where(q => q.b!.RoomId == filter.RoomId);
        }

        var query = from b in _dbContext.Bookings
                    where (
                        from bi in invitationBookingIds
                        select bi.b.BookingId
                    ).Contains(b.BookingId)
                    select b;

        return await query.SumAsync(b => b.TotalDuration + b.ExtendedDuration);

        // alternatif jika nilai tidak sesuai
        /* var sum = await _dbContext.Bookings
                .Where(b => (
                        from bi in invitationBookingIds
                        select bi.b.BookingId
                    ).Contains(b.BookingId))
                .SumAsync(b => b.TotalDuration + b.ExtendedDuration);

        return sum; */
    }

    public async Task<BookingWithRoom?> GetItemFilteredByBookingIdAsync(string bookingId)
    {
        var query = from b in _dbContext.Bookings
                    from r in _dbContext.Rooms
                        .Where(r => b.RoomId == r.Radid).DefaultIfEmpty()
                    where b.BookingId == bookingId
                    select new BookingWithRoom
                    {
                        Id = b.Id,
                        BookingId = b.BookingId,
                        BookingId365 = b.BookingId365,
                        BookingIdGoogle = b.BookingIdGoogle,
                        BookingDevices = b.BookingDevices,
                        NoOrder = b.NoOrder,
                        Title = b.Title,
                        Date = b.Date,
                        RoomId = b.RoomId,
                        RoomName = b.RoomName,
                        IsMerge = b.IsMerge,
                        MergeRoom = b.MergeRoom,
                        MergeRoomId = b.MergeRoomId,
                        MergeRoomName = b.MergeRoomName,
                        Start = b.Start,
                        End = b.End,
                        CostTotalBooking = b.CostTotalBooking,
                        DurationPerMeeting = b.DurationPerMeeting,
                        TotalDuration = b.TotalDuration,
                        ExtendedDuration = b.ExtendedDuration,
                        Pic = b.Pic,
                        AlocationId = b.AlocationId,
                        AlocationName = b.AlocationName,
                        Note = b.Note,
                        CanceledNote = b.CanceledNote,
                        Participants = b.Participants,
                        ExternalLink = b.ExternalLink,
                        ExternalLink365 = b.ExternalLink365,
                        ExternalLinkGoogle = b.ExternalLinkGoogle,
                        EndEarlyMeeting = b.EndEarlyMeeting,
                        TextEarly = b.TextEarly,
                        IsDevice = b.IsDevice,
                        IsMeal = b.IsMeal,
                        IsEar = b.IsEar,
                        IsRescheduled = b.IsRescheduled,
                        IsCanceled = b.IsCanceled,
                        IsExpired = b.IsExpired,
                        CanceledBy = b.CanceledBy,
                        CanceledAt = b.CanceledAt,
                        ExpiredBy = b.ExpiredBy,
                        ExpiredAt = b.ExpiredAt,
                        RescheduledBy = b.RescheduledBy,
                        RescheduledAt = b.RescheduledAt,
                        EarlyEndedBy = b.EarlyEndedBy,
                        EarlyEndedAt = b.EarlyEndedAt,
                        IsAlive = b.IsAlive,
                        Timezone = b.Timezone,
                        Comment = b.Comment,
                        CreatedAt = b.CreatedAt,
                        CreatedBy = b.CreatedBy,
                        UpdatedAt = b.UpdatedAt,
                        UpdatedBy = b.UpdatedBy,
                        IsNotifEndMeeting = b.IsNotifEndMeeting,
                        IsNotifBeforeEndMeeting = b.IsNotifBeforeEndMeeting,
                        IsAccessTrigger = b.IsAccessTrigger,
                        IsConfigSettingEnable = b.IsConfigSettingEnable,
                        IsEnableApproval = b.IsEnableApproval,
                        IsEnablePermission = b.IsEnablePermission,
                        IsEnableRecurring = b.IsEnableRecurring,
                        IsEnableCheckin = b.IsEnableCheckin,
                        IsRealeaseCheckinTimeout = b.IsRealeaseCheckinTimeout,
                        IsReleased = b.IsReleased,
                        IsEnableCheckinCount = b.IsEnableCheckinCount,
                        Category = b.Category,
                        LastModifiedDateTime365 = b.LastModifiedDateTime365,
                        PermissionEnd = b.PermissionEnd,
                        PermissionCheckin = b.PermissionCheckin,
                        ReleaseRoomCheckinTime = b.ReleaseRoomCheckinTime,
                        CheckinCount = b.CheckinCount,
                        IsVip = b.IsVip,
                        IsApprove = b.IsApprove,
                        VipUser = b.VipUser,
                        UserEndMeeting = b.UserEndMeeting,
                        UserCheckin = b.UserCheckin,
                        UserApproval = b.UserApproval,
                        UserApprovalDatetime = b.UserApprovalDatetime,
                        RoomMeetingMove = b.RoomMeetingMove,
                        RoomMeetingOld = b.RoomMeetingOld,
                        IsMoved = b.IsMoved,
                        IsMovedAgree = b.IsMovedAgree,
                        MovedDuration = b.MovedDuration,
                        MeetingEndNote = b.MeetingEndNote,
                        VipApproveBypass = b.VipApproveBypass,
                        VipLimitCapBypass = b.VipLimitCapBypass,
                        VipLockRoom = b.VipLockRoom,
                        VipForceMoved = b.VipForceMoved,
                        DurationSavedRelease = b.DurationSavedRelease,
                        IsCleaningNeed = b.IsCleaningNeed,
                        CleaningTime = b.CleaningTime,
                        CleaningStart = b.CleaningStart,
                        CleaningEnd = b.CleaningEnd,
                        UserCleaning = b.UserCleaning,
                        ServerDate = b.ServerDate,
                        ServerStart = b.ServerStart,
                        ServerEnd = b.ServerEnd,
                        BookingType = b.BookingType,
                        IsPrivate = b.IsPrivate,
                        RoomName2 = r.Name,
                        RoomPrice = r.Price,
                        RoomWorkStart = r.WorkStart,
                        RoomWorkEnd = r.WorkEnd,
                        RoomWorkDay = r.WorkDay,
                    };

        return await query.FirstOrDefaultAsync();
    }

    public async Task<List<Booking>> GetBookingsByDateRoomAsync(
        string[] roomIds,
        DateOnly date)
    {
        // var startTime = timeData.TimeOfDay;
        // var endTime = timeData.AddMinutes(-1).TimeOfDay;

        return await _context.Bookings
            .Where(b => b.Date == date
                        && roomIds.Contains(b.RoomId)
                        && b.IsAlive == 1
            // && b.BookingId != bookingId
            // && startTime >= b.Start.TimeOfDay
            // && endTime <= b.End.AddMinutes((double)(b.ExtendedDuration ?? 0)).TimeOfDay
            )
            .ToListAsync();
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
    public async Task<IEnumerable<Booking>> GetAllFilteredByDate(string startDate, string? radId)
    {
        bool isValidDate = DateOnly.TryParse(startDate, out var parsedDate);

        var query = from booking in _context.Bookings
                    join room in _context.Rooms on booking.RoomId equals room.Radid
                    where (!isValidDate || booking.Date == parsedDate) // If date is invalid, return all bookings
                        && booking.IsDeleted == 0
                            && booking.IsExpired == 0
                            && booking.IsCanceled == 0
                            && booking.EndEarlyMeeting == 0
                            && booking.IsMerge == 1
                            && booking.MergeRoomId == radId
                    select new Booking
                    {  // kalau terlalu banyak, bisa dihilangkan beberapa field
                        Id = booking.Id,
                        BookingId = booking.BookingId,
                        Title = booking.Title,
                        Date = booking.Date,
                        RoomId = booking.RoomId,
                        RoomName = room.Name,
                        Start = booking.Start,
                        End = booking.End,
                        Pic = booking.Pic,
                        Note = booking.Note,
                        Participants = booking.Participants,

                        IsMerge = booking.IsMerge,
                        BookingId365 = booking.BookingId365,
                        BookingIdGoogle = booking.BookingIdGoogle,
                        BookingDevices = booking.BookingDevices,
                        NoOrder = booking.NoOrder,
                        CostTotalBooking = booking.CostTotalBooking,
                        DurationPerMeeting = booking.DurationPerMeeting,
                        TotalDuration = booking.TotalDuration,
                        ExtendedDuration = booking.ExtendedDuration,
                        AlocationId = booking.AlocationId,
                        AlocationName = booking.AlocationName,
                        CanceledNote = booking.CanceledNote,
                        ExternalLink = booking.ExternalLink,
                        ExternalLink365 = booking.ExternalLink365,
                        ExternalLinkGoogle = booking.ExternalLinkGoogle,
                        EndEarlyMeeting = booking.EndEarlyMeeting,
                        TextEarly = booking.TextEarly,
                        IsDevice = booking.IsDevice,
                        IsMeal = booking.IsMeal,
                        IsEar = booking.IsEar,
                        IsRescheduled = booking.IsRescheduled,
                        IsCanceled = booking.IsCanceled,
                        IsExpired = booking.IsExpired,
                        CanceledBy = booking.CanceledBy,
                        CanceledAt = booking.CanceledAt,
                        ExpiredBy = booking.ExpiredBy,
                        ExpiredAt = booking.ExpiredAt,
                        RescheduledBy = booking.RescheduledBy,
                        RescheduledAt = booking.RescheduledAt,
                        EarlyEndedBy = booking.EarlyEndedBy,
                        EarlyEndedAt = booking.EarlyEndedAt,
                        IsAlive = booking.IsAlive,
                        Timezone = booking.Timezone,
                        Comment = booking.Comment,
                        CreatedAt = booking.CreatedAt,
                        CreatedBy = booking.CreatedBy,
                        UpdatedAt = booking.UpdatedAt,
                        UpdatedBy = booking.UpdatedBy,
                        IsNotifEndMeeting = booking.IsNotifEndMeeting,
                        IsNotifBeforeEndMeeting = booking.IsNotifBeforeEndMeeting,
                        IsAccessTrigger = booking.IsAccessTrigger,
                        IsConfigSettingEnable = booking.IsConfigSettingEnable,
                        IsEnableApproval = booking.IsEnableApproval,
                        IsEnablePermission = booking.IsEnablePermission,
                        IsEnableRecurring = booking.IsEnableRecurring,
                        IsEnableCheckin = booking.IsEnableCheckin,
                        IsRealeaseCheckinTimeout = booking.IsRealeaseCheckinTimeout,
                        IsReleased = booking.IsReleased,
                        IsEnableCheckinCount = booking.IsEnableCheckinCount,
                        Category = booking.Category,
                        LastModifiedDateTime365 = booking.LastModifiedDateTime365,
                        PermissionEnd = booking.PermissionEnd,
                        PermissionCheckin = booking.PermissionCheckin,
                        ReleaseRoomCheckinTime = booking.ReleaseRoomCheckinTime,
                        CheckinCount = booking.CheckinCount,
                        IsVip = booking.IsVip,
                        IsApprove = booking.IsApprove,
                        VipUser = booking.VipUser,
                        UserEndMeeting = booking.UserEndMeeting,
                        UserCheckin = booking.UserCheckin,
                        UserApproval = booking.UserApproval,
                        UserApprovalDatetime = booking.UserApprovalDatetime,
                        RoomMeetingMove = booking.RoomMeetingMove,
                        RoomMeetingOld = booking.RoomMeetingOld,
                        IsMoved = booking.IsMoved,
                        IsMovedAgree = booking.IsMovedAgree,
                        MovedDuration = booking.MovedDuration,
                        MeetingEndNote = booking.MeetingEndNote,
                        VipApproveBypass = booking.VipApproveBypass,
                        VipLimitCapBypass = booking.VipLimitCapBypass,
                        VipLockRoom = booking.VipLockRoom,
                        VipForceMoved = booking.VipForceMoved,
                        DurationSavedRelease = booking.DurationSavedRelease,
                        IsCleaningNeed = booking.IsCleaningNeed,
                        CleaningTime = booking.CleaningTime,
                        CleaningStart = booking.CleaningStart,
                        CleaningEnd = booking.CleaningEnd,
                        UserCleaning = booking.UserCleaning,
                        ServerDate = booking.ServerDate,
                        ServerStart = booking.ServerStart,
                        ServerEnd = booking.ServerEnd,
                        BookingType = booking.BookingType,
                        IsPrivate = booking.IsPrivate
                    };

        return await query.OrderByDescending(b => b.Date).ToListAsync();
    }



    public async Task<List<Booking>> GetMeetingOccupiedByDisplay(string radId, DateTime dateTime)
    {
        try
        {
            // Ensure only the date part is compared
            DateTime parsedDate = dateTime.Date;

            var query = from b in _context.Bookings
                        join r in _context.Rooms on b.RoomId equals r.Radid
                        where b.IsAlive == 1
                            && b.IsDeleted == 0
                            && b.IsExpired == 0
                            && b.IsCanceled == 0
                            && b.EndEarlyMeeting == 0
                            && b.ServerDate.HasValue && b.ServerDate.Value.Date == parsedDate
                            && b.RoomId == radId
                            && b.ServerStart < dateTime  // ✅ Compare directly (Jakarta time)
                            && b.ServerEnd.HasValue && b.ServerEnd.Value.AddMinutes(b.ExtendedDuration ?? 0) > dateTime
                        select b;

            return await query.ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching occupied meetings: {ex.Message}");
            return new List<Booking>();
        }
    }


    public async Task<List<Booking>> GetMeetingListByDisplay(DateTime dateTime, List<string>? roomSelect)
    {
        // Extract the date part
        DateOnly parsedDate = DateOnly.FromDateTime(dateTime);

        var query = from b in _context.Bookings
                    join r in _context.Rooms on b.RoomId equals r.Radid
                    where b.IsAlive == 1
                        && b.IsDeleted == 0
                        && b.IsExpired == 0
                        && b.IsCanceled == 0
                        && b.EndEarlyMeeting == 0
                        && b.Date == parsedDate // Compare DateOnly
                        && b.ServerStart.HasValue // Ensure ServerStart is not null
                        && b.ServerStart.Value > dateTime
                        && (roomSelect == null || roomSelect.Count == 0 || roomSelect.Contains(b.RoomId!))
                    select b;

        return await query.ToListAsync();
    }
    public async Task<List<Booking>> GetMeetingListOccupiedByDisplay(DateTime dateTime, List<string>? roomSelect)
    {
        DateOnly parsedDate = DateOnly.FromDateTime(dateTime);
        TimeSpan parsedTime = dateTime.TimeOfDay; // ✅ Use TimeSpan instead of TimeOnly

        var query = from b in _context.Bookings
                    join r in _context.Rooms on b.RoomId equals r.Radid
                    where b.IsAlive == 1
                          && b.IsDeleted == 0
                          && b.IsExpired == 0
                          && b.IsCanceled == 0
                          && b.EndEarlyMeeting == 0
                          && b.Date == parsedDate
                          && b.ServerStart.HasValue
                          && b.ServerStart.Value.TimeOfDay < parsedTime
                          && b.ServerEnd.HasValue
                          && b.ServerEnd.Value.AddMinutes(b.ExtendedDuration ?? 0) > dateTime
                          && (roomSelect == null || roomSelect.Count == 0 || roomSelect.Contains(b.RoomId!))
                    select b;

        return await query.ToListAsync();
    }

    public async Task<Booking?> GetMaxOrderNumberAsync(string year)
    {
        var result = await _context.Bookings
            .Where(b => b.Date.Year.ToString() == year) // Convert Year to String
            .OrderByDescending(b => b.NoOrder)
            .FirstOrDefaultAsync();

        return result;
    }


    public async Task<List<Booking>> ChecBookingConditionPerRoomAsync(string roomId, DateTime date, TimeSpan start, TimeSpan end, bool isVip = false)
    {
        var checkBookingByTime = await CheckBookingByTimeAsync(roomId, date, start, end, "start", isVip);
        var checkBookingByTimeEnd = await CheckBookingByTimeAsync(roomId, date, start, end, "end", isVip);

        var cColectionExisting = new HashSet<string>();
        var cColectionExistingMeetingMoved = new List<Booking>();

        // Process Start Time Overlaps
        foreach (var booking in checkBookingByTime)
        {
            if (booking.IsAlive == 0 || booking.IsCanceled == 1 || booking.IsExpired == 1 || booking.EndEarlyMeeting == 1)
            {
                continue;
            }

            if (!isVip)
            {
                throw new Exception("The schedule has been created by another booking.");
            }

            cColectionExisting.Add(booking.BookingId);
            cColectionExistingMeetingMoved.Add(booking);
        }

        // Process End Time Overlaps
        foreach (var booking in checkBookingByTimeEnd)
        {
            if (booking.IsAlive == 0 || booking.IsCanceled == 1 || booking.IsExpired == 1 || booking.EndEarlyMeeting == 1)
            {
                continue;
            }

            if (!isVip)
            {
                throw new Exception("The schedule has been created by another booking.");
            }

            if (!cColectionExisting.Contains(booking.BookingId))
            {
                cColectionExisting.Add(booking.BookingId);
                cColectionExistingMeetingMoved.Add(booking);
            }
        }

        return cColectionExistingMeetingMoved;
    }

    public async Task<List<BookingDataDto>> CheckBookingByTimeAsync(
        string RadId, DateTime date, TimeSpan start, TimeSpan end, string type = "", bool isApproval = false)
    {
        var query = from b in _dbContext.Bookings
                    join r in _dbContext.Rooms on b.RoomId equals r.Radid
                    join bi in _dbContext.BookingInvitations on b.BookingId equals bi.BookingId
                    join bui in _dbContext.Buildings on r.BuildingId equals bui.Id
                    where b.RoomId == RadId
                          && b.Date == DateOnly.FromDateTime(date)
                          && b.IsDeleted == 0
                          && bi.IsPic == 1
                    select new BookingDataDto
                    {
                        Id = b.Id,
                        BookingId = b.BookingId,
                        RoomId = b.RoomId,
                        RoomName = r.Name,
                        Price = r.Price,
                        PicEmail = bi.Email,
                        PicName = bi.Name,
                        PicNik = bi.Nik,
                        PicVip = bi.IsVip,
                        RoomDescription = r.Description,
                        RoomLocation = r.Location,
                        RoomCapacity = r.Capacity,
                        RoomGoogleMap = r.GoogleMap,
                        BuildingName = bui.Name,
                        BuildingDetailAddress = bui.DetailAddress,
                        BuildingGoogleMap = bui.GoogleMap,
                        ServerStart = b.ServerStart,
                        ServerEnd = b.ServerEnd,
                        ExtendedDuration = b.ExtendedDuration
                    };

        if (type == "start")
        {
            query = query.Where(b => b.ServerStart.HasValue && b.ServerStart.Value.TimeOfDay >= start && b.ServerStart.Value.TimeOfDay <= end);
        }
        else
        {
            query = query.Where(b => b.ServerEnd.HasValue && b.ServerEnd.Value.AddMinutes(b.ExtendedDuration ?? 0).TimeOfDay >= start && b.ServerEnd.Value.AddMinutes(b.ExtendedDuration ?? 0).TimeOfDay <= end);
        }

        return await query.ToListAsync();
    }

    public async Task<BookingDataDto?> GetDataBookingById(string bookingId)
    {
        var query = from booking in _context.Bookings
                    join room in _context.Rooms on booking.RoomId equals room.Radid
                    join building in _context.Buildings on room.BuildingId equals building.Id
                    select new BookingDataDto
                    {
                        BookingId = booking.BookingId,
                        BookingId365 = booking.BookingId365,
                        BookingIdGoogle = booking.BookingIdGoogle,
                        BookingDevices = booking.BookingDevices,
                        NoOrder = booking.NoOrder,
                        Title = booking.Title,
                        Date = booking.Date,
                        RoomId = booking.RoomId,
                        RoomName = booking.RoomName, 
                        Start = booking.Start,
                        End = booking.End,
                        CostTotalBooking = booking.CostTotalBooking,
                        DurationPerMeeting = booking.DurationPerMeeting,
                        TotalDuration = booking.TotalDuration,
                        Pic = booking.Pic,
                        AlocationId = booking.AlocationId,
                        AlocationName = booking.AlocationName,
                        Note = booking.Note,
                        ExternalLink = booking.ExternalLink,
                        IsDevice = booking.IsDevice,
                        IsMeal = booking.IsMeal,
                        IsRescheduled = booking.IsRescheduled,
                        IsCanceled = booking.IsCanceled,
                        IsExpired = booking.IsExpired,
                        IsAlive = booking.IsAlive,
                        CreatedAt = booking.CreatedAt,
                        CreatedBy = booking.CreatedBy,
                        Category = booking.Category,
                        Timezone = booking.Timezone,
                        IsVip = booking.IsVip,
                        VipUser = booking.VipUser,
                        IsApprove = booking.IsApprove,
                        UserApproval = booking.UserApproval,

                        // Additional properties from DTO
                        RoomDescription = room.Description,
                        RoomLocation = room.Location,
                        RoomCapacity = room.Capacity,
                        RoomGoogleMap = room.GoogleMap,
                        BuildingName = building.Name,
                        BuildingDetailAddress = building.DetailAddress,
                        BuildingGoogleMap = building.GoogleMap,
                        Price = room.Price,

                        // Formatting
                        FormatTimeStart = booking.Start.ToString("HH:mm:ss"),
                        FormatTimeEnd = booking.End.ToString("HH:mm:ss"),
                        FormatDate = booking.Date.ToString("yyyy-MM-dd"),
                    };


        return await query.FirstOrDefaultAsync();
    }




}

