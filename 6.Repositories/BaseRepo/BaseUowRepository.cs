using Microsoft.EntityFrameworkCore;

namespace _6.Repositories.Repository;
public class BaseUowRepository<E> where E : BaseEntity, new()
{
    public IUnitOfWork _uowo = null;
    public readonly MyDbContext _context;
    public readonly DbSet<E> _dbSet;
    public readonly string _schemaDB;

    public BaseUowRepository(IUnitOfWork uow)
    {
        _uowo = uow;
        _context = _uowo.Dbcon();
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
            .AsNoTracking()
            .Select(x => x.id)
            .FirstOrDefaultAsync();

        return result;
    }

    public virtual async Task<E?> GetOneByField(string propertyName, string value, bool isTracking = false)
    {
        // Query SQL dinamis
        var sqlQuery = $"SELECT * FROM {_schemaDB} WHERE {propertyName} = @value AND is_deleted = 0";

        // Eksekusi query dengan parameter
        var query = _dbSet.FromSqlRaw(sqlQuery, new SqlParameter("value", value));
        if (!isTracking)
        {
            query = query.AsNoTracking();
        }

        return await query.FirstOrDefaultAsync();
    }

    public virtual async Task<List<E>> GetListByField(string propertyName, string value, bool isTracking = false)
    {
        // Query SQL dinamis
        var sqlQuery = $"SELECT * FROM {_schemaDB} WHERE {propertyName} = @value AND is_deleted = 0";

        // Eksekusi query dengan parameter
        var query = _dbSet.FromSqlRaw(sqlQuery, new SqlParameter("value", value));
        if (!isTracking)
        {
            query = query.AsNoTracking();
        }

        return await query.ToListAsync();
    }


    public virtual async Task<E?> GetById(string id, bool isTracking = false)
    {
        var query = _dbSet.Where(e => e.IsDeleted == 0); // Filter is_deleted
        // Tambahkan AsNoTracking jika isTracking == false
        if (!isTracking)
        {
            query = query.AsNoTracking();
        }
        return await query.FirstOrDefaultAsync(e => e.Id == id); // Cari berdasarkan id
    }


    public virtual async Task<IEnumerable<E>> GetAll(bool isTracking = false)
    {
        IQueryable<E> query = _dbSet.Where(e => e.IsDeleted == 0); // Filter is_deleted
        if (!isTracking)
        {
            query = query.AsNoTracking();
        }
        return await query.ToListAsync();
    }


    public virtual async Task<E?> Create(E entity)
    {
        _context.Set<E>().Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    public virtual async Task CreateBulk(IEnumerable<E> entities)
    {
        await _context.BulkInsertAsync(entities.ToList());
    }
    public virtual async Task<int> Update(E entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        return await _context.SaveChangesAsync();
    }
    public virtual async Task UpdateBulk(IEnumerable<E> entities)
    {
        await _context.BulkUpdateAsync(entities.ToList());
    }

    public virtual async Task<int> Delete(E entity)
    {
        _dbSet.Remove(entity);

        return await _context.SaveChangesAsync();
    }


}

