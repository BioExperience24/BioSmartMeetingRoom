using _6.Repositories.DB;
using _6.Repositories.Extension;
using _7.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace _6.Repositories.Repository
{
    public class SettingLogConfigRepository : BaseRepository<SettingLogConfig>
    {
        private readonly MyDbContext _dbContext;

        public SettingLogConfigRepository(MyDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<(IEnumerable<SettingLogConfig>?, string? err)> GetAllSettingLogConfigsAsync()
        {
            try
            {
                var query = _dbContext.SettingLogConfigs.AsQueryable();

                query = query.Where(c => c.IsDeleted == 0);

                query = query.OrderByColumn("Text", "asc");

                var list = await query.ToListAsync();
                return (list, null);
            }
            catch (Exception e)
            {
                var err = e.Message;
                return (null, err);
            }
        }

        public async Task<SettingLogConfig?> GetSettingLogConfigById(long id)
        {
            return await _dbContext.SettingLogConfigs.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<SettingLogConfig?> AddSettingLogConfigAsync(SettingLogConfig item)
        {
            using var transaction = _dbContext.Database.BeginTransaction();

            try
            {
                await transaction.CreateSavepointAsync("AddSettingLogConfigAsync");

                _dbContext.SettingLogConfigs.Add(item);
                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackToSavepointAsync("AddSettingLogConfigAsync");

                return null;
            }

            return item;
        }

        public async Task<bool> UpdateSettingLogConfigAsync(SettingLogConfig item)
        {
            using var transaction = _dbContext.Database.BeginTransaction();

            try
            {
                await transaction.CreateSavepointAsync("UpdateSettingLogConfigAsync");

                _dbContext.Entry(item).Property(e => e.Id).IsModified = false;

                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackToSavepointAsync("UpdateSettingLogConfigAsync");

                return false;
            }

            return true;
        }


        public async Task<List<SettingLogConfig>> GetListByIdAsync(long id)
        {
            return await _dbContext.SettingLogConfigs
                            .Where(e => e.Id == id && e.IsDeleted == 0)
                            .ToListAsync(); 
        }
    }
}