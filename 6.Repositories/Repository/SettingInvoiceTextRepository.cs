using _6.Repositories.DB;
using _6.Repositories.Extension;
using _7.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace _6.Repositories.Repository
{
    public class SettingInvoiceTextRepository : BaseRepository<SettingInvoiceText>
    {
        private readonly MyDbContext _dbContext;

        public SettingInvoiceTextRepository(MyDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<(IEnumerable<SettingInvoiceText>?, string? err)> GetAllSettingInvoiceTextsAsync()
        {
            try
            {
                var query = _dbContext.SettingInvoiceTexts.AsQueryable();

                query = query.Where(c => c.IsDeleted == 0);

                query = query.OrderByColumn("Name", "asc");

                var list = await query.ToListAsync();
                return (list, null);
            }
            catch (Exception e)
            {
                var err = e.Message;
                return (null, err);
            }
        }

        public async Task<SettingInvoiceText?> GetSettingInvoiceTextById(string id)
        {
            return await _dbContext.SettingInvoiceTexts.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<SettingInvoiceText?> AddSettingInvoiceTextAsync(SettingInvoiceText item)
        {
            using var transaction = _dbContext.Database.BeginTransaction();

            try
            {
                await transaction.CreateSavepointAsync("AddSettingInvoiceTextAsync");

                _dbContext.SettingInvoiceTexts.Add(item);
                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackToSavepointAsync("AddSettingInvoiceTextAsync");

                return null;
            }

            return item;
        }

        public async Task<bool> UpdateSettingInvoiceTextAsync(SettingInvoiceText item)
        {
            using var transaction = _dbContext.Database.BeginTransaction();

            try
            {
                await transaction.CreateSavepointAsync("UpdateSettingInvoiceTextAsync");

                _dbContext.Entry(item).Property(e => e.Id).IsModified = false;

                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackToSavepointAsync("UpdateSettingInvoiceTextAsync");

                return false;
            }

            return true;
        }
    }
}