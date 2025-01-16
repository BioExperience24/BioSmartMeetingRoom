

namespace _6.Repositories.Repository
{
    public class RoomForUsageRepository : BaseLongRepository<RoomForUsage>
    {
        private readonly MyDbContext _dbContext;
        
        public RoomForUsageRepository (MyDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}