namespace _6.Repositories.Repository;

public class UserRepository(MyDbContext dbContext) : BaseLongRepository<User>(dbContext!)
{
    public async Task<IEnumerable<object>> GetItemsAsync()
    {
        var query = from user in dbContext.Users
                    from level in dbContext.Levels
                        .Where(l => l.Id == user.LevelId).DefaultIfEmpty()
                    from employee in dbContext.Employees
                        .Where(e => e.Id == user.EmployeeId)
                    where user.IsDeleted == 0
                    select new { user, level = new { GroupName = level.Name }, employee = new { EmployeeName = employee.Name } };

        var list = await query.ToListAsync();

        return list;
    }

    public async Task<object?> GetUserJoin(string username, string password)
    {
        var query = from user in dbContext.Users
                        .Where(u =>
                        u.IsDeleted == 0 &&
                        u.Username.ToLower() == username.ToLower() &&
                        u.Password == password)
                    from level in dbContext.Levels
                        .Where(l => l.Id == user.LevelId).DefaultIfEmpty()
                    from employee in dbContext.Employees
                        .Where(e => e.Id == user.EmployeeId)
                    where user.IsDeleted == 0
                    select new { user, level = new { GroupName = level.Name }, employee = new { EmployeeName = employee.Name } };

        var list = await query.FirstOrDefaultAsync();

        return list;
    }

    public async Task<User?> GetUserByUsername(string username)
    {
        var query = from u in dbContext.Users
                    where u.IsDeleted == 0 && u.Username.ToLower() == username.ToLower()
                    select u;

        return await query.FirstOrDefaultAsync();
    }

    public async Task<UserLogin?> GetUserByUsernamePassword(string username, string password)
    {
        var query = from user in dbContext.Users
                    join employee in dbContext.Employees
                        on user.EmployeeId equals employee.Nik into empGroup
                    from employee in empGroup.DefaultIfEmpty()
                    where user.IsDeleted == 0
                    where user.IsDisactived == 0
                    where user.Username == username
                    where user.Password == password
                    select new
                    {
                        User = user,   // Select User directly
                        Nik = employee.Nik
                    };

        var result = await query.FirstOrDefaultAsync();
        if (result == null) return null;

        var userLogin = new UserLogin
        {
            Id = result.User.Id,
            SecureQr = result.User.SecureQr,
            Name = result.User.Name,
            Username = result.User.Username,
            EmployeeId = result.User.EmployeeId,
            Password = result.User.Password,
            RealPassword = result.User.RealPassword,
            LevelId = result.User.LevelId,
            AccessId = result.User.AccessId,
            CreatedBy = result.User.CreatedBy,
            CreatedAt = result.User.CreatedAt,
            UpdatedAt = result.User.UpdatedAt,
            IsDisactived = result.User.IsDisactived,
            UpdatedBy = result.User.UpdatedBy,
            IsVip = result.User.IsVip,
            VipApproveBypass = result.User.VipApproveBypass,
            VipLimitCapBypass = result.User.VipLimitCapBypass,
            VipShiftedBypass = result.User.VipShiftedBypass,
            IsApproval = result.User.IsApproval,
            Nik = result.Nik
        };

        return userLogin;
    }

    public async Task<User?> GetUserByUsernameWithFilter(string username)
    {
        var query = from user in dbContext.Users
                    join employee in dbContext.Employees
                        on user.EmployeeId equals employee.Nik into empGroup
                    from employee in empGroup.DefaultIfEmpty()
                    where user.IsDeleted == 0
                    where user.IsDisactived == 0
                    where user.Username == username
                    select user;  // Return only user

        return await query.FirstOrDefaultAsync(); // Use FirstOrDefault instead of ToList
    }

}