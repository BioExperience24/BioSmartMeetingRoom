

using System.Text.Json;
using _6.Repositories.DB;
using _7.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace _6.Repositories.Repository
{
    public class AlocationMatrixRepository
    {
        private readonly MyDbContext _dbContext;

        public AlocationMatrixRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<AlocationMatrix>> GetItemsWithEntityAsync(AlocationMatrix entity)
        {
            var query = _dbContext.AlocationMatrices.AsQueryable();

            if (entity.AlocationId != null)
            {
                query = query.Where(c => c.AlocationId == entity.AlocationId);
            }

            if (entity.Nik != null)
            {
                query = query.Where(c => c.Nik == entity.Nik);
            }

            var items = await query.ToListAsync();

            return items;
        }

        public async Task<AlocationMatrix?> GetItemWithEntityAsync(AlocationMatrix entity)
        {
            var query = _dbContext.AlocationMatrices.AsQueryable();

            if (entity.AlocationId != null)
            {
                query = query.Where(c => c.AlocationId == entity.AlocationId);
            }

            if (entity.Nik != null)
            {
                query = query.Where(c => c.Nik == entity.Nik);
            }

            var item = await query.FirstOrDefaultAsync();

            return item;
        }

        public async Task<AlocationMatrix?> AddAsync(AlocationMatrix item)
        {
            _dbContext.AlocationMatrices.Add(item);
                
            await _dbContext.SaveChangesAsync();

            return item;
        }

        public async Task AddRangeAsync(IEnumerable<AlocationMatrix> entities)
        {
            await _dbContext.BulkInsertAsync(entities.ToList());
        }

        public async Task<int> UpdateAsync(AlocationMatrix item)
        {
            // _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.Entry(item).Property(e => e.Generate).IsModified = false;

            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(AlocationMatrix item)
        {
            _dbContext.AlocationMatrices.Remove(item);
            return await _dbContext.SaveChangesAsync();

        }

        public async Task<int> DeleteRangeAsync(IEnumerable<AlocationMatrix> items)
        {
            _dbContext.AlocationMatrices.RemoveRange(items);
                
            return await _dbContext.SaveChangesAsync();
        }
    }
}