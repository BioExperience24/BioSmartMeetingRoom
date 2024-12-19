namespace _6.Repositories.Repository;
public class BaseRepository<E> where E : BaseEntity, new()
{
    public readonly MyDbContext _context;
    public readonly DbSet<E> _dbSet;
    public readonly string _schemaDB;

    public BaseRepository(MyDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<E>(); // Inisialisasi DbSet dari tipe generik E

        // Ambil metadata entitas untuk mendapatkan nama tabel dan schema
        var entityType = _context.Model.FindEntityType(typeof(E));
        var tableName = entityType?.GetTableName();
        var schema = entityType?.GetSchema() ?? "dbo"; // Default ke 'dbo' kalau schema null
        _schemaDB = $"[{schema}].[{tableName}]";
    }

    public async Task<int> GetNextID()
    {
        var sqlQuery = $@"
            SELECT COALESCE(MAX(CAST(Id AS INT)), 0) + 1 as id
            FROM {_schemaDB}";

        // Eksekusi query
        var result = await _context.IdOnly
            .FromSqlRaw(sqlQuery)
            .Select(x => x.id)
            .FirstOrDefaultAsync();

        return result;
    }

    public virtual async Task<E?> GetOneByField(string propertyName, string value)
    {
        // Query SQL dinamis
        var sqlQuery = $"SELECT * FROM {_schemaDB} WHERE {propertyName} = @value AND is_deleted = 0";

        // Eksekusi query dengan parameter
        return await _dbSet.FromSqlRaw(sqlQuery, [new SqlParameter("value", value)]).FirstOrDefaultAsync();
    }

    public virtual async Task<List<E>> GetListByField(string propertyName, string value)
    {
        // Query SQL dinamis
        var sqlQuery = $"SELECT * FROM {_schemaDB} WHERE {propertyName} = @value AND is_deleted = 0";

        // Eksekusi query dengan parameter
        return await _dbSet.FromSqlRaw(sqlQuery, [new SqlParameter("value", value)]).ToListAsync();
    }

    // Get By Id
    public virtual async Task<E?> GetByIdAsync(string id)
    {
        return await _dbSet
                        .Where(e => e.IsDeleted == 0) // Filter is_deleted
                        .FirstOrDefaultAsync(e => e.Id == id); // Cari berdasarkan id 
    }

    // Get All
    public virtual async Task<IEnumerable<E>> GetAllAsync()
    {
        return await _dbSet
            .Where(e => e.IsDeleted == 0) // Filter is_deleted
            .ToListAsync();
    }

    // Add Entity
    /* public async Task AddAsync(E entity)
    {
        await _dbSet.AddAsync(entity);
    } */
    public virtual async Task<E?> AddAsync(E entity)
    {
        _context.Set<E>().Add(entity);

        await _context.SaveChangesAsync();

        return entity;
    }
    public virtual async Task CreateBulk(IEnumerable<E> entities)
    {
        await _context.BulkInsertAsync(entities.ToList());
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
        _context.Entry(entity).State = EntityState.Modified;
        // _context.Entry(entity).Property(e => e.Generate).IsModified = false;

        return await _context.SaveChangesAsync();
    }
    public virtual async Task UpdateBulk(IEnumerable<E> entities)
    {
        await _context.BulkUpdateAsync(entities.ToList());
    }
    // Delete Entity
    /* public void Delete(E entity)
    {
        _dbSet.Remove(entity);
    } */
    public virtual async Task<int> Delete(E entity)
    {
        _dbSet.Remove(entity);

        return await _context.SaveChangesAsync();
    }

    /* public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    } */

}

