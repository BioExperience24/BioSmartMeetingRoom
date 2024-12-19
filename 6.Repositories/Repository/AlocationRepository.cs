using System.Text.Json;
using _6.Repositories.DB;
using _7.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace _6.Repositories.Repository
{
    public class AlocationRepository : BaseRepository<Alocation>
    {
        private readonly MyDbContext _dbContext;
        
        public AlocationRepository (MyDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
                
        public async Task<IEnumerable<object>> GetItemsAsync()
        {
        
            var query = from alocation in _dbContext.Alocations
                        from alocationType in _dbContext.AlocationTypes
                            .Where(at => alocation.Type == at.Id).DefaultIfEmpty()
                        where alocation.IsDeleted == 0 
                        && alocationType.IsDeleted == 0
                        orderby alocation.Id ascending
                        select new { alocation, alocationType = new { TypeName = alocationType.Name } };

            var list = await query.ToListAsync();

            return list;
        }
    
        public async Task<IEnumerable<object>> GetItemByTypeAsync(string type)
        {
            var query = from alocation in _dbContext.Alocations
                        from alocationType in _dbContext.AlocationTypes
                            .Where(at => alocation.Type == at.Id).DefaultIfEmpty()
                        where alocation.IsDeleted == 0 
                        && alocationType.IsDeleted == 0
                        && alocation.Type == type
                        orderby alocation.Id ascending
                        select new { alocation, alocationType = new { TypeName = alocationType.Name } };

            var list = await query.ToListAsync();

            return list;
        }
        
        public override async Task<int> UpdateAsync(Alocation item)
        {
            // _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.Entry(item).Property(e => e.Generate).IsModified = false;

            return await _dbContext.SaveChangesAsync();
        }
    }
}