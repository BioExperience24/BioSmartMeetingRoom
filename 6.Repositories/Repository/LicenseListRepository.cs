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
    public class LicenseListRepository : BaseRepository<LicenseList>
    {
        private readonly MyDbContext _dbContext;

        public LicenseListRepository(MyDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<(IEnumerable<LicenseList>?, string? err)> GetAllLicenseListsAsync()
        {
            try
            {
                var query = _dbContext.LicenseLists.AsQueryable()
                                //.Where(c => c.IsDeleted == 0)
                                .OrderByColumn("Name", "asc");

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

        public async Task<LicenseList?> GetLicenseListByName(string name)
        {
            return await _dbContext.LicenseLists.FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task<LicenseList?> GetLicenseListById(int id)
        {
            return await _dbContext.LicenseLists.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<LicenseList?> AddLicenseListAsync(LicenseList item)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                await transaction.CreateSavepointAsync("AddLicenseListAsync");

                _dbContext.LicenseLists.Add(item);
                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
                return item;
            }
            catch (Exception e)
            {
                await transaction.RollbackToSavepointAsync("AddLicenseListAsync");
                // Log the exception
                return null;
            }
        }

        public async Task<bool> UpdateLicenseListAsync(LicenseList item)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                await transaction.CreateSavepointAsync("UpdateLicenseListAsync");

                _dbContext.Entry(item).Property(e => e.Id).IsModified = false;
                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
                return true;
            }
            catch (Exception e)
            {
                await transaction.RollbackToSavepointAsync("UpdateLicenseListAsync");
                // Log the exception
                return false;
            }
        }
    }
}