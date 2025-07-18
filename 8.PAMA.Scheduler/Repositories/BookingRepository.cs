using _6.Repositories.DB;
using Microsoft.EntityFrameworkCore;
using _8.PAMA.Scheduler.ViewModel;
using _7.Entities.Models;

namespace _8.PAMA.Scheduler.Repositories;

public class BookingRepository(MyDbContext _dbContext)
{
    public async Task<List<BookingReminderBeforeStart>> GetBookingReminderBeforeStartAsync()
    {
        var now = DateTime.Now;
        var today = DateOnly.FromDateTime(now);

        var query = from b in _dbContext.Bookings
                    join r in _dbContext.Rooms on b.RoomId equals r.Radid into roomJoin
                    from r in roomJoin.DefaultIfEmpty() // LEFT JOIN
                    where b.Date == today
                          && b.Start > now
                          && b.IsExpired == 0
                          && b.IsNotifBeforeEndMeeting == 0
                          && b.IsCanceled == 0
                          && b.IsDeleted == 0
                    select new
                    {
                        b.BookingId,
                        b.Title,
                        b.Date,
                        b.Start,
                        b.End,
                        b.RoomId,
                        RoomName = r.Name
                    };

        var result = await query.ToListAsync();

        return [.. result.Select(x => new BookingReminderBeforeStart
        {
            BookingId = x.BookingId,
            Title = x.Title,
            Date = x.Date,
            Start = x.Start,
            End = x.End,
            RoomId = x.RoomId,
            RoomName = x.RoomName
        })];
    }

    public async Task<List<BookingNotifBeforeEnd>> GetBookingNotifBeforeEndAsync(int numBefore)
    {
        var now = DateTime.Now;
        var today = DateOnly.FromDateTime(now);
        var timeNow = TimeOnly.FromDateTime(now);

        var result = await _dbContext.Bookings
            .Where(b => b.Date == today &&
                        b.IsExpired == 0 &&
                        b.IsCanceled == 0 &&
                        b.IsNotifBeforeEndMeeting == 0)
            .Select(b => new
            {
                Booking = b,
                EndDur = b.End.AddMinutes(b.ExtendedDuration == null ? 0 : (double)b.ExtendedDuration),
                EndNotifTime = b.End.AddMinutes(b.ExtendedDuration == null ? 0 : (double)b.ExtendedDuration - numBefore)
            })
            .Where(x => x.EndNotifTime < now)
            .Select(x => new BookingNotifBeforeEnd
            {
                BookingId = x.Booking.BookingId,
                Title = x.Booking.Title,
                Start = x.Booking.Start,
                End = x.Booking.End,
            })
            .ToListAsync();

        return result;
    }

    public async Task<List<BookingCheckExpiredMeeting>> CheckExpiredMeetingPastAsync()
    {
        var today = DateOnly.FromDateTime(DateTime.Now);

        var bookings = await _dbContext.Bookings
            .Where(b => b.Date < today &&
                        b.IsExpired == 0 &&
                        b.IsCanceled == 0 &&
                        b.IsAlive == 1)
            .Select(b => new BookingCheckExpiredMeeting
            {
                BookingId = b.BookingId,
                Title = b.Title,
                Date = b.Date,
                Start = b.Start,
                End = b.End
            })
            .ToListAsync();

        return bookings;
    }

    public async Task<List<BookingCheckExpiredMeeting>> CheckExpiredMeetingTodayAsync()
    {
        var now = DateTime.Now;
        var today = DateOnly.FromDateTime(now);
        var currentTime = now.TimeOfDay;

        var result = await _dbContext.Bookings
            .Where(b => b.Date == today &&
                        b.IsExpired == 0 &&
                        b.IsCanceled == 0 &&
                        b.IsAlive == 1 &&
                        b.End.AddMinutes(b.ExtendedDuration == null ? 0 : (double)b.ExtendedDuration) < now)
            .ToListAsync();

        return [.. result.Select(b => new BookingCheckExpiredMeeting
        {
            BookingId = b.BookingId,
            Title = b.Title,
            Date = b.Date,
            Start = b.Start,
            End = b.End
        })];
    }

    public async Task PostExpiredAsync(string bookingId)
    {
        var now = DateTime.Now;

        var booking = await _dbContext.Bookings
            .FirstOrDefaultAsync(b => b.BookingId == bookingId);

        if (booking != null)
        {
            booking.IsAlive = 4;
            booking.IsExpired = 1;
            booking.ExpiredBy = "sistem";
            booking.ExpiredAt = now;

            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<List<BookingReminder>> GetBookingReminderUnusedAsync()
    {
        var now = DateTime.Now;
        var today = DateOnly.FromDateTime(now);

        var result = await (from b in _dbContext.Bookings
                            join r in _dbContext.Rooms on b.RoomId equals r.Radid into roomJoin
                            from r in roomJoin.DefaultIfEmpty()
                            where b.Date == today &&
                                  b.IsExpired == 0 &&
                                  b.IsAlive == 1 &&
                                  b.IsNotifBeforeEndMeeting == 0
                            select new BookingReminder
                            {
                                BookingId = b.BookingId,
                                Title = b.Title,
                                Start = b.Start,
                                End = b.End,
                                ExtendedDuration = b.ExtendedDuration,
                                EndDur = b.End.AddMinutes(b.ExtendedDuration == null ? 0 : (double)b.ExtendedDuration),
                                RoomName = r != null ? r.Name : null,
                                RoomLocation = r != null ? r.Location : null,
                                IsConfigSettingEnableRoom = r != null ? r.IsConfigSettingEnable : 0,
                                IsEnableCheckinRoom = r != null ? r.IsEnableCheckin : 0,
                                IsReleaseCheckinTimeoutRoom = r != null ? r.IsRealeaseCheckinTimeout : 0,
                                ConfigReleaseRoomCheckinTimeoutRoom = r != null ? r.ConfigReleaseRoomCheckinTimeout : 0,
                                ConfigPermissionCheckinRoom = r.ConfigPermissionCheckin ?? "",
                                ConfigPermissionEndRoom = r.ConfigPermissionEnd ?? "",
                                IsEnableCheckinCountRoom = r != null ? r.IsEnableCheckinCount : 0
                            })
                            .ToListAsync();

        return result;
    }

    public async Task<List<BookingInvitation>> GetCkinRoomBooking(string bookingId)
    {
        return await _dbContext.BookingInvitations
            .Where(b => b.BookingId == bookingId && b.Checkin == 1)
            .ToListAsync();
    }

    public async Task<List<BookingInvitation>> GetCkinRoomBookingPic(string bookingId)
    {
        return await _dbContext.BookingInvitations
            .Where(
                b => b.BookingId == bookingId &&
                b.Checkin == 1 &&
                b.IsPic == 1 &&
                b.Internal == 1
            )
            .ToListAsync();
    }

    public async Task<bool> PostExpiredUnusedAsync(string bookingId)
    {
        var now = DateTime.Now;
        var booking = await _dbContext.Bookings
            .FirstOrDefaultAsync(b => b.BookingId == bookingId);

        if (booking != null)
        {
            booking.IsAlive = 4;
            booking.IsExpired = 1;
            booking.ExpiredBy = "sistem";
            booking.ExpiredAt = now;
            booking.IsReleased = 1;
            booking.EndEarlyMeeting = 1;
            booking.CanceledNote = "Room released by sistem";

            await _dbContext.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<List<MeetingAccess>> OpenDataMeetingAccessAsync()
    {
        var today = DateOnly.FromDateTime(DateTime.Now);

        var result = await (
            from b in _dbContext.Bookings
            join r in _dbContext.Rooms on b.RoomId equals r.Radid
            join ai in _dbContext.AccessIntegrateds on r.Radid equals ai.RoomId
            join ac in _dbContext.AccessControls on ai.AccessId equals ac.Id
            where b.Date == today
                && b.IsDeleted == 0
                && b.IsCanceled == 0
                && r.IsDeleted == 0
                && ac.ModelController == "reader"
            select new MeetingAccess
            {
                BookingId = b.BookingId,
                Title = b.Title,
                Start = b.Start,
                End = b.End,
                Date = b.Date,
                RoomName = r.Name,
                AccessGroup = ac.Name,
                IpController = ac.IpController,
                Channel = ac.Channel ?? 0,
                Type = ac.Type
            }
        ).ToListAsync();

        return result;
    }

    public async Task<List<BookingEndAccessModel>> OpenDataMeetingEndAccessAsync()
    {
        var today = DateOnly.FromDateTime(DateTime.Now);

        var data = await (
            from b in _dbContext.Bookings
            join r in _dbContext.Rooms on b.RoomId equals r.Radid
            join ai in _dbContext.AccessIntegrateds on r.Radid equals ai.RoomId
            join ac in _dbContext.AccessControls on ai.AccessId equals ac.Id
            where b.Date == today
                  && (b.EndEarlyMeeting == 1 || b.IsDeleted == 1 || b.IsExpired == 1 || b.IsCanceled == 1)
                  && r.IsDeleted == 0
                  && ac.ModelController == "reader"
                  && b.IsAccessTrigger == 0
            select new BookingEndAccessModel
            {
                BookingId = b.BookingId,
                RoomId = b.RoomId,
                Start = b.Start,
                End = b.End,
                Date = b.Date,
                EndEarlyMeeting = b.EndEarlyMeeting,
                EarlyEndedAt = b.EarlyEndedAt,
                IsDeleted = b.IsDeleted,
                IsExpired = b.IsExpired,
                IsCanceled = b.IsCanceled,
                IsAccessTrigger = b.IsAccessTrigger,

                RoomName = r.Name,
                AccessGroup = ac.Name,
                IpController = ac.IpController,
                Channel = ac.Channel,
                Type = ac.Type
            }
        ).ToListAsync();

        return data;
    }


    public async Task UpdateAccessTriggerAsync(string bookingId)
    {
        var row = await _dbContext.Bookings.FirstOrDefaultAsync(b => b.BookingId == bookingId);
        if (row != null)
        {
            row.IsAccessTrigger = 1;
            await _dbContext.SaveChangesAsync();
        }
    }


}