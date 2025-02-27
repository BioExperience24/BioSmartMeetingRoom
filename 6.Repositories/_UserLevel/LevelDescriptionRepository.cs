namespace _6.Repositories.Repository;

public class LevelDescriptionRepository : BaseLongRepository<LevelDescriptiion>
{
    private readonly MyDbContext _dbContext;

    public LevelDescriptionRepository(MyDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<object?> GetItemByLevelIdAsync(int levelId)
    {
        var query = from levelDescriptiions in _dbContext.LevelDescriptiions
                    from levels in _dbContext.Levels
                            // .Where(l => levelDescriptiions.LevelId == l.Id).DefaultIfEmpty()
                            .Where(l => levelDescriptiions.LevelId == l.Id)
                    where levelDescriptiions.IsDeleted == 0
                    && levels.IsDeleted == 0
                    && levelDescriptiions.LevelId == levelId
                    select new { levelDescriptiions, levels = new { Name = levels.Name } };

        var list = await query.FirstOrDefaultAsync();

        return list;
    }
}