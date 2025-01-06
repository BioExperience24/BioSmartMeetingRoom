using System.Diagnostics.Metrics;
using _6.Repositories.DB;
using _7.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _6.Repositories.Repository;

public class EmployeeRepository : BaseRepository<Employee>
{

    private readonly MyDbContext _dbContext;

    public EmployeeRepository(MyDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<object>> GetItemsAsync()
    {
        var query = from employee in _dbContext.Employees
                    from alocationType in _dbContext.AlocationTypes
                        .Where(at => employee.CompanyId == at.Id).DefaultIfEmpty()
                    from alocation in _dbContext.Alocations
                        .Where(a => employee.DepartmentId == a.Id).DefaultIfEmpty()
                    where employee.IsDeleted == 0 
                    orderby employee.Generate ascending
                    select new { employee, alocationType = new { CompanyName = alocationType.Name }, alocation = new { DepartmentName = alocation.Name } };

        return await query.ToListAsync();
    }

    public async Task<int> GetCountAsync()
    {
        var query = from employee in _dbContext.Employees
                    from alocationType in _dbContext.AlocationTypes
                        .Where(at => employee.CompanyId == at.Id).DefaultIfEmpty()
                    from alocation in _dbContext.Alocations
                        .Where(a => employee.DepartmentId == a.Id).DefaultIfEmpty()
                    where employee.IsDeleted == 0 
                    orderby employee.Generate ascending
                    select new { employee, alocationType = new { CompanyName = alocationType.Name }, alocation = new { DepartmentName = alocation.Name } };

        return await query.CountAsync();
    }

    public async Task<IEnumerable<object>> GetItemsWithoutUserAsync()
    {
        var query = (from employee in _dbContext.Employees
                    from user in _dbContext.Users
                        .Where(u => u.EmployeeId == employee.Id).DefaultIfEmpty()
                    where employee.IsDeleted == 0 
                    && (
                        user == null
                        || (
                            user != null
                            && user.IsDeleted == 1
                            && !_dbContext.Users
                                .Any(u => u.EmployeeId == employee.Id && u.IsDeleted == 0)
                        )
                    )
                    select new { employee }).Distinct();

        return await query.ToListAsync();
    }

    public override async Task<int> UpdateAsync(Employee item)
    {
        // _dbContext.Entry(item).State = EntityState.Modified;
        _dbContext.Entry(item).Property(e => e.Generate).IsModified = false;

        return await _dbContext.SaveChangesAsync();
    }
}

