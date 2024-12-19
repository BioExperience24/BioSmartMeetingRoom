
using System.Text.Json;
using _6.Repositories.DB;
using _6.Repositories.Extension;
using _7.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace _6.Repositories.Repository
{
    public class UserRepository : BaseLongRepository<User>
    {
        private readonly MyDbContext _dbContext;
        
        public UserRepository (MyDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<IEnumerable<object>> GetItemsAsync()
        {
            var query = from user in _dbContext.Users
                        from level in _dbContext.Levels
                            .Where(l => l.Id == user.LevelId).DefaultIfEmpty()
                        from employee in _dbContext.Employees
                            .Where(e => e.Id == user.EmployeeId)
                        where user.IsDeleted == 0
                        select new { user, level = new { GroupName = level.Name }, employee = new { EmployeeName = employee.Name } };

            var list = await query.ToListAsync();

            return list;
        }
    }
}