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

        public async Task<object?> GetItemByIdAsync(string id)
        {
            var query = from alocation in _dbContext.Alocations
                        from alocationType in _dbContext.AlocationTypes
                            .Where(at => alocation.Type == at.Id).DefaultIfEmpty()
                        where alocation.IsDeleted == 0 
                        && alocation.Id == id
                        orderby alocation.Id ascending
                        select new { 
                            alocation, 
                            alocationType = new { 
                                TypeName = alocationType.Name,
                                Invoice = alocationType.InvoiceStatus 
                            } 
                        };

            var item = await query.FirstOrDefaultAsync();

            return item;
        }
        
        public override async Task<int> UpdateAsync(Alocation item)
        {
            // _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.Entry(item).Property(e => e.Generate).IsModified = false;

            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> CountByIds(string[] ids)
        {
            var query = _context.Alocations.AsQueryable();

            query = query.Where(c => c.IsDeleted == 0 && ids.Contains(c.Id));

            return await query.CountAsync();
        }

        public async Task<List<Alocation>> GetItemsByDepartmentCode(string[] departmentCodes)
        {
            var query = _context.Alocations.AsQueryable();

            query = query.Where(c => c.IsDeleted == 0 && departmentCodes.Contains(c.DepartmentCode));

            return await query.ToListAsync();
        }
    }
}