using _6.Repositories.DB;
using _6.Repositories.Extension;
using _7.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace _6.Repositories.Repository;

public class CompanyRepository : BaseRepository<Company>
{
    private readonly MyDbContext _dbContext;
    public CompanyRepository(MyDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Company?> GetOneItemAsync()
    {
        var query = _dbContext.Companies.AsQueryable();

        query = query.OrderByColumn("CreatedAt", "desc");

        var result = await query.FirstOrDefaultAsync();

        return result;
    }
}
