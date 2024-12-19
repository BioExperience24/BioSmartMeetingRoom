using _6.Repositories.DB;
using _6.Repositories.Extension;
using _7.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace _6.Repositories.Repository
{
    public class VariableTimeExtendRepository : BaseRepositoryId<VariableTimeExtend>
    {
        private readonly MyDbContext _dbContext;

        public VariableTimeExtendRepository(MyDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<(IEnumerable<VariableTimeExtend>?, string? err)> GetAllVariableTimeExtendsAsync()
        {
            try
            {
                var query = _dbContext.VariableTimeExtends.AsQueryable();

                //query = query.Where(c => c.IsDeleted == 0);

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

        public async Task<VariableTimeExtend?> GetVariableTimeExtendById(long id)
        {
            return await _dbContext.VariableTimeExtends.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<VariableTimeExtend?> AddVariableTimeExtendAsync(VariableTimeExtend item)
        {
            using var transaction = _dbContext.Database.BeginTransaction();

            try
            {
                await transaction.CreateSavepointAsync("AddVariableTimeExtendAsync");

                _dbContext.VariableTimeExtends.Add(item);
                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackToSavepointAsync("AddVariableTimeExtendAsync");

                return null;
            }

            return item;
        }

        public async Task<bool> UpdateVariableTimeExtendAsync(VariableTimeExtend item)
        {
            using var transaction = _dbContext.Database.BeginTransaction();

            try
            {
                await transaction.CreateSavepointAsync("UpdateVariableTimeExtendAsync");

                _dbContext.Entry(item).Property(e => e.Id).IsModified = false;

                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackToSavepointAsync("UpdateVariableTimeExtendAsync");

                return false;
            }

            return true;
        }
    }
}