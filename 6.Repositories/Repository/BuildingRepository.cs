
using System.Text.Json;
using _6.Repositories.DB;
using _6.Repositories.Extension;
using _7.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace _6.Repositories.Repository
{
    public class BuildingRepository : BaseLongRepository<Building>
    {
        private readonly MyDbContext _dbContext;
        
        public BuildingRepository (MyDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<IEnumerable<object>> GetAllItemAsync(long? excludeId = null)
        {
            var query = from building in _dbContext.Buildings
                        where building.IsDeleted == 0
                        orderby building.Id ascending
                        select new { 
                            // CountRoom = _dbContext.Rooms
                            //                 .Where(rr => rr.IsDeleted == 0 && rr.BuildingId == building.Id).Count(),
                            // CountFloor = _dbContext.BeaconFloors
                            //                 .Where(ff => ff.IsDeleted == 0 && ff.BuildingId == building.Id).Count(),
                            building,
                            CountRoom = (
                                from room in _dbContext.Rooms
                                where room.IsDeleted == 0
                                && room.BuildingId == building.Id
                                select new { room.RadId }
                            ).Count(),
                            CountFloor = (
                                from beaconFloor in _dbContext.BeaconFloors
                                where beaconFloor.IsDeleted == 0 
                                && beaconFloor.BuildingId == building.Id
                                select new { beaconFloor.Id }
                            ).Count(),
                            CountDesk = (
                                from deskRoomTable in _dbContext.DeskRoomTables
                                from deskRoom in _dbContext.DeskRooms
                                        .Where(dr => dr.Id == deskRoomTable.DeskRoomId)
                                where deskRoomTable.IsDeleted == 0
                                && building.Id.ToString() == deskRoom.Id
                                select new { deskRoomTable.DeskId }
                            ).Count()
                        };

            // Aplikasikan filter excludeId jika nilainya tidak null
            if (excludeId != null)
            {
                query = query.Where(q => q.building.Id != excludeId);
            }

            var list = await query.ToListAsync();

            return list;
        }

        public async Task<object?> GetItemByIdAsync(long id)
        {
            var query = from building in _dbContext.Buildings
                        where building.IsDeleted == 0
                        && building.Id == id
                        orderby building.Id ascending
                        select new { 
                            building,
                            CountRoom = (
                                from room in _dbContext.Rooms
                                where room.IsDeleted == 0
                                && room.BuildingId == building.Id
                                select new { room.RadId }
                            ).Count(),
                            CountFloor = (
                                from beaconFloor in _dbContext.BeaconFloors
                                where beaconFloor.IsDeleted == 0 
                                && beaconFloor.BuildingId == building.Id
                                select new { beaconFloor.Id }
                            ).Count(),
                            CountDesk = (
                                from deskRoomTable in _dbContext.DeskRoomTables
                                from deskRoom in _dbContext.DeskRooms
                                        .Where(dr => dr.Id == deskRoomTable.DeskRoomId)
                                where deskRoomTable.IsDeleted == 0
                                && building.Id.ToString() == deskRoom.Id
                                select new { deskRoomTable.DeskId }
                            ).Count()
                        };

            var list = await query.FirstOrDefaultAsync();

            return list;
        }
    }
}