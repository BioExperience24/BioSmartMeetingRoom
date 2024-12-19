using _6.Repositories.DB;
using _6.Repositories.Extension;
using _7.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace _6.Repositories.Repository
{
    public class SettingPantryConfigRepository : BaseRepositoryId<SettingPantryConfig>
    {
        private readonly MyDbContext _dbContext;

        public SettingPantryConfigRepository(MyDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<(IEnumerable<SettingPantryConfig>?, string? err)> GetAllSettingPantryConfigsAsync()
        {
            try
            {
                var query = _dbContext.SettingPantryConfigs.AsQueryable();

                //query = query.Where(c => c.IsDeleted == 0);

                //query = query.OrderByColumn("Id", "asc");

                var list = await query.ToListAsync();
                return (list, null);
            }
            catch (Exception e)
            {
                var err = e.Message;
                return (null, err);
            }
        }

        public async Task<SettingPantryConfig?> GetSettingPantryConfigTopOne()
        {
            return await _dbContext.SettingPantryConfigs.FirstOrDefaultAsync();
        }
        public async Task<SettingPantryConfig?> GetSettingPantryConfigById(int id)
        {
            return await _dbContext.SettingPantryConfigs.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<SettingPantryConfig?> AddSettingPantryConfigAsync(SettingPantryConfig item)
        {
            using var transaction = _dbContext.Database.BeginTransaction();

            try
            {
                await transaction.CreateSavepointAsync("AddSettingPantryConfigAsync");

                _dbContext.SettingPantryConfigs.Add(item);
                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackToSavepointAsync("AddSettingPantryConfigAsync");

                return null;
            }

            return item;
        }

        public async Task<bool> UpdateSettingPantryConfigAsync(SettingPantryConfig item)
        {
            using var transaction = _dbContext.Database.BeginTransaction();

            try
            {
                await transaction.CreateSavepointAsync("UpdateSettingPantryConfigAsync");

                _dbContext.Entry(item).Property(e => e.Id).IsModified = false;

                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackToSavepointAsync("UpdateSettingPantryConfigAsync");

                return false;
            }

            return true;
        }
    }
}