using _6.Repositories.DB;
using _6.Repositories.Extension;
using _7.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace _6.Repositories.Repository
{
    public class SettingInvoiceConfigRepository : BaseRepository<SettingInvoiceConfig>
    {
        private readonly MyDbContext _dbContext;

        public SettingInvoiceConfigRepository(MyDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<(IEnumerable<SettingInvoiceConfig>?, string? err)> GetAllSettingInvoiceConfigsAsync()
        {
            try
            {
                var query = _dbContext.SettingInvoiceConfigs.AsQueryable();

                query = query.Where(c => c.IsDeleted == 0);

                query = query.OrderByColumn("DateFormat", "asc");

                var list = await query.ToListAsync();
                return (list, null);
            }
            catch (Exception e)
            {
                var err = e.Message;
                return (null, err);
            }
        }

        public async Task<SettingInvoiceConfig?> GetSettingInvoiceConfigById(long id)
        {
            return await _dbContext.SettingInvoiceConfigs.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<SettingInvoiceConfig?> AddSettingInvoiceConfigAsync(SettingInvoiceConfig item)
        {
            using var transaction = _dbContext.Database.BeginTransaction();

            try
            {
                await transaction.CreateSavepointAsync("AddSettingInvoiceConfigAsync");

                _dbContext.SettingInvoiceConfigs.Add(item);
                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackToSavepointAsync("AddSettingInvoiceConfigAsync");

                return null;
            }

            return item;
        }

        public async Task<bool> UpdateSettingInvoiceConfigAsync(SettingInvoiceConfig item)
        {
            using var transaction = _dbContext.Database.BeginTransaction();

            try
            {
                await transaction.CreateSavepointAsync("UpdateSettingInvoiceConfigAsync");

                _dbContext.Entry(item).Property(e => e.Id).IsModified = false;

                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackToSavepointAsync("UpdateSettingInvoiceConfigAsync");

                return false;
            }

            return true;
        }
    }
}