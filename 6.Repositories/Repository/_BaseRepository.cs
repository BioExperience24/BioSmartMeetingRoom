using _6.Repositories.DB;
using _7.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace _6.Repositories.Repository;
public class BaseRepository<E> where E : BaseEntity, new()
{
    public readonly MyDbContext _context;
    public readonly DbSet<E> _dbSet;

    public BaseRepository(MyDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<E>(); // Inisialisasi DbSet dari tipe generik E
    }

    // Get By Id
    public async Task<E?> GetByIdAsync(string id)
    {
        return await _dbSet
            .Where(e => e.IsDeleted == 0) // Filter is_deleted
            .FirstOrDefaultAsync(e => e.Id == id); // Cari berdasarkan id
    }

    // Get All
    public async Task<IEnumerable<E>> GetAllAsync()
    {
        return await _dbSet
            .Where(e => e.IsDeleted == 0) // Filter is_deleted
            .ToListAsync();
    }

    // Add Entity
    public async Task AddAsync(E entity)
    {
        await _dbSet.AddAsync(entity);
    }

    // Update Entity
    public void UpdateAsync(E entity)
    {
        _dbSet.Attach(entity).State = EntityState.Modified;
        //_dbSet.SaveChangesAsync();
        //_dbSet.Update(entity);
    }

    // Delete Entity
    public void Delete(E entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

}

