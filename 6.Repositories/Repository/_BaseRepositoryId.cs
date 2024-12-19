using _6.Repositories.DB;
using _7.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace _6.Repositories.Repository;
public class BaseRepositoryId<E> where E : BaseEntityId, new()
{
    public readonly MyDbContext _context;
    public readonly DbSet<E> _dbSet;

    public BaseRepositoryId(MyDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<E>(); // Inisialisasi DbSet dari tipe generik E
    }

    public async Task<int> GetNextID()
    {
        // Ambil metadata entitas untuk mendapatkan nama tabel dan schema
        var entityType = _context.Model.FindEntityType(typeof(E));
        var tableName = entityType?.GetTableName();
        var schema = entityType?.GetSchema() ?? "dbo"; // Default ke 'dbo' kalau schema null

        // Query SQL dengan schema
        var sqlQuery = $@"
            SELECT COALESCE(MAX(CAST(Id AS INT)), 0) + 1 as id
            FROM [{schema}].[{tableName}]";

        // Eksekusi query
        var result = await _context.IdOnly
            .FromSqlRaw(sqlQuery)
            .Select(x => x.id)
            .FirstOrDefaultAsync();

        return result;
    }

    // Get By Id
    public async Task<E?> GetByIdAsync(long id)
    {
        try
        {
            return await _dbSet
                            //.Where(e => e.IsDeleted == 0) // Filter is_deleted
                            .FirstOrDefaultAsync(e => e.Id == id); // Cari berdasarkan id 
        }
        catch (Exception)
        {
            throw;
        }
    }

    // Get All
    public async Task<IEnumerable<E>> GetAllAsync()
    {
        return await _dbSet
            //.Where(e => e.IsDeleted == 0) // Filter is_deleted
            .ToListAsync();
    }

    // Add Entity
    /* public async Task AddAsync(E entity)
    {
        await _dbSet.AddAsync(entity);
    } */
    public async Task<E?> AddAsync(E entity)
    {
        using var transaction = _context.Database.BeginTransaction();

        try
        {
            await transaction.CreateSavepointAsync("AddAsync");

            _context.Set<E>().Add(entity);

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackToSavepointAsync("AddAsync");

            return null;
        }

        return entity;
    }

    // Update Entity
    /* public void UpdateAsync(E entity)
    {
        _dbSet.Attach(entity).State = EntityState.Modified;
        //_dbSet.SaveChangesAsync();
        //_dbSet.Update(entity);
    } */
    public virtual async Task<int> UpdateAsync(E entity)
    {
        using var transaction = _context.Database.BeginTransaction();

        try
        {
            await transaction.CreateSavepointAsync("UpdateAsync");

            _context.Entry(entity).State = EntityState.Modified;
            // _context.Entry(entity).Property(e => e.Generate).IsModified = false;

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackToSavepointAsync("UpdateAsync");

            throw;
        }

        return 1;
    }

    // Delete Entity
    /* public void Delete(E entity)
    {
        _dbSet.Remove(entity);
    } */
    public async Task<bool> Delete(E entity)
    {
        using var transaction = _context.Database.BeginTransaction();

        try
        {
            await transaction.CreateSavepointAsync("Delete");

            _context.Set<E>().Remove(entity);

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackToSavepointAsync("Delete");

            throw;
        }

        return true;
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

}

