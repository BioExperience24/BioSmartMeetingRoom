using System.Diagnostics.Metrics;
using _6.Repositories.DB;
using _7.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _6.Repositories.Repository;

public class BookingRepository : BaseLongRepository<Booking>
{

    private readonly MyDbContext _dbContext;

    public BookingRepository(MyDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Booking>> GetAllAsync()
    {
        return await _context.Bookings.Where(b => b.IsDeleted == 0).ToListAsync();
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
            .Where(b => b.Date >= start && b.Date <= end && b.IsDeleted == 0)
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
            .Where(b => b.Date >= start && b.Date <= end && b.Pic == nik && b.IsDeleted == 0)
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

