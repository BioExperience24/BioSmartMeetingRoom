using _6.Repositories.DB;
using _6.Repositories.Extension;
using _7.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace _6.Repositories.Repository;

public class BuildingFloorRepository : BaseLongRepository<BuildingFloor>
{
    private readonly MyDbContext _dbContext;
        
    public BuildingFloorRepository (MyDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<object>> GetAllItemAsync(BuildingFloor? entity = null)
    {
        var query = from buildingFloor in _dbContext.BuildingFloors
                    from building in _dbContext.Buildings
                        .Where(b => b.Id == buildingFloor.BuildingId)
                    where buildingFloor.IsDeleted == 0 && building.IsDeleted == 0
                    select buildingFloor;

        if (entity != null)
        {
            if (entity.Id != null)
            {
                query = query.Where(q  => q.Id == entity.Id);
            }

            if (entity.BuildingId != null)
            {
                query = query.Where(q  => q.BuildingId == entity.BuildingId);
            }
        }

        query = query.OrderByColumn("Position", "asc");

        var list = await query.ToListAsync();

        return list;
    }

    public async Task<BuildingFloor?> GetItemByEntityAsync(BuildingFloor? entity = null, bool isLast = true)
    {
        var query = from buildingFloor in _dbContext.BuildingFloors
                    from building in _dbContext.Buildings
                        .Where(b => b.Id == buildingFloor.BuildingId)
                    where buildingFloor.IsDeleted == 0 && building.IsDeleted == 0
                    select buildingFloor;

        if (entity != null)
        {
            if (entity.Id != null)
            {
                query = query.Where(q  => q.Id == entity.Id);
            }

            if (entity.BuildingId != null)
            {
                query = query.Where(q  => q.BuildingId == entity.BuildingId);
            }
        }

        if (isLast == true)
        {
            query = query.OrderByColumn("Position", "desc");
        } else {
            query = query.OrderByColumn("Position", "asc");
        }

        query = query.Take(1);

        var item = await query.FirstOrDefaultAsync();

        return item;
    }
}
