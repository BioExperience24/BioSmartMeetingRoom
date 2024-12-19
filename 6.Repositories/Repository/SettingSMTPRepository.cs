using _6.Repositories.DB;
using _6.Repositories.Extension;
using _7.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace _6.Repositories.Repository
{
    public class SettingSmtpRepository : BaseRepository<SettingSmtp>
    {
        private readonly MyDbContext _dbContext;

        public SettingSmtpRepository(MyDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<(IEnumerable<SettingSmtp>?, string? err)> GetAllSettingSmtpsAsync()
        {
            try
            {
                var query = _dbContext.SettingSmtps.AsQueryable();

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
        
        public async Task<SettingSmtp?> GetSettingSmtpByName(string name)
        {
            return await _dbContext.SettingSmtps.FirstOrDefaultAsync(c => c.Name == name);
        }
        public async Task<SettingSmtp?> GetSettingSmtpById(int id)
        {
            return await _dbContext.SettingSmtps.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<SettingSmtp?> AddSettingSmtpAsync(SettingSmtp item)
        {
            using var transaction = _dbContext.Database.BeginTransaction();

            try
            {
                await transaction.CreateSavepointAsync("AddSettingSmtpAsync");

                _dbContext.SettingSmtps.Add(item);
                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackToSavepointAsync("AddSettingSmtpAsync");

                return null;
            }

            return item;
        }

        public async Task<bool> UpdateSettingSmtpAsync(SettingSmtp item)
        {
            using var transaction = _dbContext.Database.BeginTransaction();

            try
            {
                await transaction.CreateSavepointAsync("UpdateSettingSmtpAsync");

                _dbContext.Entry(item).Property(e => e.Id).IsModified = false;

                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackToSavepointAsync("UpdateSettingSmtpAsync");

                return false;
            }

            return true;
        }
    }
}