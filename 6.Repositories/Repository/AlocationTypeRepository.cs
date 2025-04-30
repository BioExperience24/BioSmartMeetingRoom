using _6.Repositories.DB;
using _6.Repositories.Extension;
using _7.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace _6.Repositories.Repository
{
    public class AlocationTypeRepository : BaseRepository<AlocationType>
    {
        // private readonly MyDbContext _dbContext;
        
        public AlocationTypeRepository (MyDbContext dbContext) : base(dbContext)
        {
            // _dbContext = dbContext;
        }

        public override async Task<IEnumerable<AlocationType>> GetAllAsync()
        {            
            var query = _context.AlocationTypes.AsQueryable();

            query = query.Where(c => c.IsDeleted == 0);

            query = query.OrderByColumn("Name", "asc");

            var list = await query.ToListAsync();

            return list;
        }

        public override async Task<int> UpdateAsync(AlocationType item)
        {
            
            // _context.Entry(item).State = EntityState.Modified;
            _context.Entry(item).Property(e => e.Generate).IsModified = false;

            return await _context.SaveChangesAsync();
        }

        public async Task<int> CountByIds(string[] ids)
        {
            var query = _context.AlocationTypes.AsQueryable();

            query = query.Where(c => c.IsDeleted == 0 && ids.Contains(c.Id));

            return await query.CountAsync();
        }
    }
}