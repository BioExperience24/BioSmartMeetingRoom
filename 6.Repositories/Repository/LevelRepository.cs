using _6.Repositories.DB;
using _6.Repositories.Extension;
using _7.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace _6.Repositories.Repository
{
    public class LevelRepository : BaseLongRepository<Level>
    {
        private readonly MyDbContext _dbContext;
        
        public LevelRepository (MyDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<IEnumerable<Level>> GetAllAsync()
        {
            var query = _dbContext.Levels.AsQueryable();

            query = query.Where(c => c.IsDeleted == 0);

            query = query.OrderByColumn("Id", "asc");

            return await query.ToListAsync();
        }
    }
}