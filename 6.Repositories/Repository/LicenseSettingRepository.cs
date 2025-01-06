using _6.Repositories.DB;
using _6.Repositories.Extension;
using _7.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _6.Repositories.Repository
{
    public class LicenseSettingRepository : BaseRepository<LicenseSetting>
    {
        private readonly MyDbContext _dbContext;

        public LicenseSettingRepository(MyDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<(IEnumerable<LicenseSetting>?, string? err)> GetAllLicenseSettingsAsync()
        {
            try
            {
                var query = _dbContext.LicenseSettings.AsQueryable()
                                .Where(c => c.IsDeleted == 0)
                                .OrderByColumn("Serial", "asc");

                var list = await query.ToListAsync();
                return (list, null);
            }
            catch (Exception e)
            {
                // Log the exception
                var err = e.Message;
                return (null, err);
            }
        }

        public async Task<LicenseSetting?> GetLicenseSettingBySerial(string serial)
        {
            return await _dbContext.LicenseSettings.FirstOrDefaultAsync(c => c.Serial == serial);
        }

        public async Task<LicenseSetting?> GetLicenseSettingById(int id)
        {
            return await _dbContext.LicenseSettings.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<LicenseSetting?> AddLicenseSettingAsync(LicenseSetting item)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                await transaction.CreateSavepointAsync("AddLicenseSettingAsync");

                _dbContext.LicenseSettings.Add(item);
                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
                return item;
            }
            catch (Exception e)
            {
                await transaction.RollbackToSavepointAsync("AddLicenseSettingAsync");
                // Log the exception
                return null;
            }
        }

        public async Task<bool> UpdateLicenseSettingAsync(LicenseSetting item)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                await transaction.CreateSavepointAsync("UpdateLicenseSettingAsync");

                _dbContext.Entry(item).Property(e => e.Id).IsModified = false;
                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
                return true;
            }
            catch (Exception e)
            {
                await transaction.RollbackToSavepointAsync("UpdateLicenseSettingAsync");
                // Log the exception
                return false;
            }
        }
    }
}