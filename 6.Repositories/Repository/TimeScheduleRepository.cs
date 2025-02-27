
namespace _6.Repositories.Repository
{
    public class TimeScheduleRepository(MyDbContext dbContext)
    {
        private readonly MyDbContext _dbContext = dbContext;

        public async Task<IEnumerable<TimeSchedule>> GetAllTimeScheduleFilteredByDurationAsync(int duration)
        {
            IQueryable<TimeSchedule> query = duration switch
            {
                30 => _dbContext.TimeSchedule30s.AsQueryable().Select(x => new TimeSchedule
                {
                    Timeid = x.Timeid,
                    Time = x.Time,
                    IsDeleted = x.IsDeleted
                }),
                60 => _dbContext.TimeSchedule60s.AsQueryable().Select(x => new TimeSchedule
                {
                    Timeid = x.Timeid,
                    Time = x.Time,
                    IsDeleted = x.IsDeleted
                }),
                _ => _dbContext.TimeSchedule15s.AsQueryable().Select(x => new TimeSchedule
                {
                    Timeid = x.Timeid,
                    Time = x.Time,
                    IsDeleted = x.IsDeleted
                })
            };

            return await query.ToListAsync();
        }
    }
}