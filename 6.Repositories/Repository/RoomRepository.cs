using System.Diagnostics.Metrics;
using System.Linq;
using _6.Repositories.DB;
using _7.Entities.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _6.Repositories.Repository;

public class RoomRepository : BaseLongRepository<Room>
{

    private readonly MyDbContext _dbContext;
        
    public RoomRepository(MyDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Room?> UpdateRoom(Room entity)
    {
        var existingEntity = await _dbContext.Rooms.FindAsync(entity.Id);
        if (existingEntity == null) return null; // Handle if entity not found

        _dbContext.Entry(existingEntity).CurrentValues.SetValues(entity); // Update all properties
        await _dbContext.SaveChangesAsync(); // Save changes
        return existingEntity; // Return the updated entity
    }

}

