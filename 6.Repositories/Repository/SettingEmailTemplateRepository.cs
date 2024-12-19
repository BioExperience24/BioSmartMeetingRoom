using _6.Repositories.DB;
using _6.Repositories.Extension;
using _7.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace _6.Repositories.Repository
{
    public class SettingEmailTemplateRepository : BaseRepository<SettingEmailTemplate>
    {
        private readonly MyDbContext _dbContext;

        public SettingEmailTemplateRepository(MyDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<(IEnumerable<SettingEmailTemplate>?, string? err)> GetAllSettingEmailTemplatesAsync()
        {
            try
            {
                var query = _dbContext.SettingEmailTemplates.AsQueryable();

                query = query.Where(c => c.IsDeleted == 0);

                query = query.OrderByColumn("Type", "asc");

                var list = await query.ToListAsync();
                return (list, null);
            }
            catch (Exception e)
            {
                var err = e.Message;
                return (null, err);
            }
        }

        public async Task<SettingEmailTemplate?> GetSettingEmailTemplateById(long id)
        {
            return await _dbContext.SettingEmailTemplates.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<SettingEmailTemplate?> AddSettingEmailTemplateAsync(SettingEmailTemplate item)
        {
            using var transaction = _dbContext.Database.BeginTransaction();

            try
            {
                await transaction.CreateSavepointAsync("AddSettingEmailTemplateAsync");

                _dbContext.SettingEmailTemplates.Add(item);
                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackToSavepointAsync("AddSettingEmailTemplateAsync");

                return null;
            }

            return item;
        }

        public override async Task<SettingEmailTemplate?> GetOneByField(string propertyName, string value)
        {
            var entityType = _context.Model.FindEntityType(typeof(SettingEmailTemplate));
            var tableName = entityType?.GetTableName();
            var schema = entityType?.GetSchema() ?? "dbo";
            var schemaDB = $"[{schema}].[{tableName}]";

            // Query SQL dinamis
            var sqlQuery = $@"SELECT * FROM {schemaDB} WHERE {propertyName} = @value";// AND is_deleted = 0";

            // Eksekusi query dengan parameter
            return await _dbSet.FromSqlRaw(sqlQuery, [new SqlParameter("@value", value)]).FirstOrDefaultAsync();
            //FormattableString sqlQuery = $@"SELECT * FROM {schemaDB} WHERE {propertyName} = @value AND is_deleted = 0";
            //return await _context.Set<SettingEmailTemplate>().FromSqlInterpolated(sqlQuery).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateSettingEmailTemplateAsync(SettingEmailTemplate item)
        {
            using var transaction = _dbContext.Database.BeginTransaction();

            try
            {
                await transaction.CreateSavepointAsync("UpdateSettingEmailTemplateAsync");

                // _dbContext.Entry(item).State = EntityState.Modified;
                // _dbContext.Entry(item).Property(e => e.Id).IsModified = false;

                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackToSavepointAsync("UpdateSettingEmailTemplateAsync");

                return false;
            }

            return true;
        }
    }
}