

using _6.Repositories.Extension;

namespace _6.Repositories.Repository
{
    public class KioskDisplayRepository : BaseLongRepository<KioskDisplay>
    {
        private readonly MyDbContext _dbContext;
        public KioskDisplayRepository(MyDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<IEnumerable<KioskDisplay>> GetAllAsync()
        {
            var query = _dbContext.KioskDisplays.AsQueryable();

            query = query.Where(q => q.IsDeleted == 0);

            query = query.OrderByColumn("Id", "asc");

            var list = await query.ToListAsync();

            return list;
        }
    }
}