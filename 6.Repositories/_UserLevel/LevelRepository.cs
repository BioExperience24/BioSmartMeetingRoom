using _6.Repositories.Extension;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace _6.Repositories.Repository;

public class LevelRepository : BaseLongRepository<Level>
{
    private readonly MyDbContext _dbContext;

    public LevelRepository(MyDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public override async Task<IEnumerable<Level>> GetAllAsync()
    {
        var query = _dbContext.Levels.AsQueryable();

        query = query.Where(c => c.IsDeleted == 0);

        query = query.OrderByColumn("Id", "asc");

        return await query.ToListAsync();
    }

    public async Task<List<MenuHeaderLevel>> GetLevel(int levelId)
    {
        var result = (from l in _dbContext.Levels
                      join ld in _dbContext.LevelDetails on l.Id equals ld.LevelId
                      join m in _dbContext.Menus on ld.MenuId equals m.Id
                      where l.Id == levelId && l.IsDeleted != 1 && m.IsDeleted != 1
                      //orderby ld.Sort ascending
                      select new MenuHeaderLevel
                      {
                          LevelName = l.Name,
                          MenuName = m.Name,
                          Url = m.Url,
                          Icon = m.Icon
                      }).ToListAsync();

        return await result;
    }

    public async Task<List<MenuHeaderLevel>> GetMenuHeader(int levelId)
    {
        var result = (from l in _dbContext.Levels
                      join ld in _dbContext.LevelHeaderDetails on l.Id equals ld.LevelId
                      join m in _dbContext.MenuHeaders on ld.MenuId equals m.Id
                      where l.Id == levelId && l.IsDeleted != 1 && m.IsDeleted != 1
                      let name = m.Name                      //orderby ld.Sort ascending
                      let url = m.Url
                      let icon = m.Icon
                      select new MenuHeaderLevel
                      {
                          LevelName = l.Name,
                          MenuName = name,
                          Url = url,
                          Icon = icon
                      }).ToListAsync();
        return await result;
    }


}