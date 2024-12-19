using _6.Repositories.DB;
using _6.Repositories.Extension;
using _7.Entities.Models;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace _6.Repositories.Repository
{
    public class VariableTimeDurationRepository : BaseRepositoryId<VariableTimeDuration>
    {
        private readonly MyDbContext _dbContext;

        public VariableTimeDurationRepository(MyDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<(IEnumerable<VariableTimeDuration>?, string? err)> GetAllVariableTimeDurationsAsync()
        {
            try
            {
                var query = _dbContext.VariableTimeDurations.AsQueryable();

               // query = query.Where(c => c.IsDeleted == 0);

                query = query.OrderByColumn("Id", "asc");

                var list = await query.ToListAsync();
                return (list, null);
            }
            catch (Exception e)
            {
                var err = e.Message;
                return (null, err);
            }
        }

        public async Task<(Dictionary<string, object>?, string? err)> GetAllVariablesAsync()
        {
            try
            {
                var query = _dbContext.VariableTimeDurations.AsQueryable();
                query = query.OrderByColumn("Id", "asc");

                var varDurationList = await query.ToListAsync();

                var query2 = (IQueryable<VariableTimeExtend>)_dbContext.VariableTimeExtends.AsQueryable();
                query2 = query2.OrderByColumn("Id", "asc");
                
                var varExtendList = await query2.ToListAsync();
               
                var dataList = new Dictionary<string, object>();
                dataList["duration"] = varDurationList;
                dataList["time_extend"] = varExtendList;
                return (dataList, null);
            }
            catch (Exception e)
            {
                var err = e.Message;
                return (null, err);
            }
        }

        public async Task<VariableTimeDuration?> GetVariableTimeDurationById(long id)
        {
            return await _dbContext.VariableTimeDurations.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<VariableTimeDuration?> AddVariableTimeDurationAsync(VariableTimeDuration item)
        {
            using var transaction = _dbContext.Database.BeginTransaction();

            try
            {
                await transaction.CreateSavepointAsync("AddVariableTimeDurationAsync");

                _dbContext.VariableTimeDurations.Add(item);
                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackToSavepointAsync("AddVariableTimeDurationAsync");

                return null;
            }

            return item;
        }

        public async Task<bool> UpdateVariableTimeDurationAsync(VariableTimeDuration item)
        {
            using var transaction = _dbContext.Database.BeginTransaction();

            try
            {
                await transaction.CreateSavepointAsync("UpdateVariableTimeDurationAsync");

                _dbContext.Entry(item).Property(e => e.Id).IsModified = false;

                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackToSavepointAsync("UpdateVariableTimeDurationAsync");

                return false;
            }

            return true;
        }
    }
}