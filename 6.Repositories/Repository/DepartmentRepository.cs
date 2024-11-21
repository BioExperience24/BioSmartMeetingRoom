using _6.Repositories.DB;
using _7.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace _6.Repositories.Repository;

public class DepartmentRepository : BaseRepository<Department>
{
    public DepartmentRepository(MyDbContext context) : base(context)
    {

    }
    public async Task<Department?> GetDepartmentWithCompany(string id)
    {
        return await _dbSet
            .Where(e => e.IsDeleted == 0) // Filter is_deleted
            .Include(d => d.Company) // Include untuk eager loading relasi ke Company
            .FirstOrDefaultAsync(e => e.Id == id); // Cari berdasarkan id
    }

}

