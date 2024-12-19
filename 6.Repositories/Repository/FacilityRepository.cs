using _6.Repositories.DB;
using _6.Repositories.Extension;
using _7.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace _6.Repositories.Repository;

public class FacilityRepository(MyDbContext context) : BaseLongRepository<Facility>(context)
{
    public async Task<(IEnumerable<Facility>?, string? err)> GetAllFacilityAsync()
    {
        try
        {
            var query = context.Facilities.AsQueryable();

            query = query.Where(c => c.IsDeleted == 0);

            query = query.OrderByColumn("Name", "asc");

            var list = await query.ToListAsync();
            return (list, null);
        }
        catch (Exception e)
        {
            // var err = e.Message;
            // return (null, err);
            throw;
        }
    }

    public async Task<Facility?> GetFacilityById(long id)
    {
        return await context.Facilities.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Facility?> AddFacilityAsync(Facility item)
    {
        using var transaction = context.Database.BeginTransaction();

        try
        {
            await transaction.CreateSavepointAsync("AddFacilityAsync");

            context.Facilities.Add(item);
            await context.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackToSavepointAsync("AddFacilityAsync");

            // return null;
            throw;
        }

        return item;
    }

    public async Task<bool> UpdateFacilityAsync(Facility item)
    {
        using var transaction = context.Database.BeginTransaction();

        try
        {
            await transaction.CreateSavepointAsync("UpdateFacilityAsync");

            // _dbContext.Entry(item).State = EntityState.Modified;
            context.Entry(item).Property(e => e.Id).IsModified = false;

            await context.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            /* Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine($"error message: {e.Message}");
            Console.WriteLine($"error message InnerException: {e.InnerException?.Message}"); */

            await transaction.RollbackToSavepointAsync("UpdateFacilityAsync");

            // return false;
            throw;
        }

        return true;
    }

}

