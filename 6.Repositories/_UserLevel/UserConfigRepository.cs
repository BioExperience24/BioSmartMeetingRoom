using _6.Repositories.Extension;

namespace _6.Repositories.Repository;

public class UserConfigRepository
{
    private readonly MyDbContext _dbContext;

    public UserConfigRepository(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserConfig?> GetOneItem()
    {
        var query = _dbContext.UserConfigs.AsQueryable();

        query = query.OrderByColumn("Id", "asc");

        var userConfig = await query.FirstOrDefaultAsync();

        return userConfig;
    }
}