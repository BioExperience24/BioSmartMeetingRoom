using _6.Repositories.DB;
using _7.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace _8.PAMA.Scheduler.Repositories;

public class SettingRuleBookingRepository(MyDbContext _dbContext)
{
    public async Task<SettingRuleBooking?> GetData()
    {
        return await _dbContext.SettingRuleBookings.FirstOrDefaultAsync() ?? null;
    }
}