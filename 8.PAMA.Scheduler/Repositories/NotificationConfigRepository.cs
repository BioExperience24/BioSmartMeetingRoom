using _6.Repositories.DB;
using _7.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace _8.PAMA.Scheduler.Repositories;

public class NotificationConfigRepository(MyDbContext _dbContext)
{
    public async Task<NotificationConfig?> GetData()
    {
        return await _dbContext.NotificationConfigs.FirstOrDefaultAsync() ?? null;
    }
}