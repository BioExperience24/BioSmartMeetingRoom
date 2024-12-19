using System.Diagnostics.Metrics;
using System.Linq;
using _6.Repositories.DB;
using _7.Entities.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _6.Repositories.Repository;

public class BeaconFloorRepository : BaseLongRepository<BeaconFloor>
{

    private readonly MyDbContext _dbContext;
        
    public BeaconFloorRepository (MyDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<object>> GetAllItemAsync()
    {
        var query = from beaconFloor in _dbContext.BeaconFloors
                    from building in _dbContext.Buildings
                            .Where(b => beaconFloor.BuildingId == b.Id)
                    where beaconFloor.IsDeleted == 0 && building.IsDeleted == 0
                    select new { beaconFloor, building = new { BuildingName = building.Name } };
        
        return await query.ToListAsync();
    }

    public async Task<object?> GetItemByIdAsync(long id)
    {
        var query = from beaconFloor in _dbContext.BeaconFloors
                    from building in _dbContext.Buildings
                            .Where(b => beaconFloor.BuildingId == b.Id)
                    where beaconFloor.IsDeleted == 0 
                    && building.IsDeleted == 0
                    && beaconFloor.Id == id
                    select new { beaconFloor, building = new { BuildingName = building.Name } };
        
        return await query.FirstOrDefaultAsync();
    }
}

