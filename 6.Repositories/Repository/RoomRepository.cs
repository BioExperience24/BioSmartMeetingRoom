

using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace _6.Repositories.Repository;

public class RoomRepository : BaseLongRepository<Room>
{

    private readonly MyDbContext _dbContext;
        
    public RoomRepository(MyDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    // Get By Id
    public async Task<Room?> GetByRadId(string id)
    {
        return await _dbContext.Rooms
                        .FirstOrDefaultAsync(e => e.Radid == id); // Cari berdasarkan id 
    }

    public async Task<List<Room>> GetByRadIdsAsync(List<string> radIds)
    {
        return await _dbContext.Rooms
            .Where(r => radIds.Contains(r.Radid))
            .ToListAsync();
    }


    public async Task<List<Room>> GetMergeRoomList(string radId)
    {
        var query = from roomMergeDetail in _dbContext.RoomMergeDetails
                    join room in _dbContext.Rooms on roomMergeDetail.RoomId equals room.Radid
                    where room.IsDeleted == 0 && roomMergeDetail.RoomId == radId
                    orderby room.Name ascending
                    select room;

        var list = await query.ToListAsync();

        return list;
    }


    public async Task<IEnumerable<Room>> GetAllChartTopFiveRoomAsync(int year)
    {
        // total booking bedasarkan room
        var bookingByRooms = (
            from booking in _dbContext.Bookings
            where booking.IsDeleted == 0
            select new {
                RoomId = booking.RoomId,
                Year = booking.Date.Year,
                // Month = booking.Date.Month,
                Total = (
                    from b in _dbContext.Bookings
                    where b.IsDeleted == 0
                    && b.RoomId == booking.RoomId
                    && b.Date.Year == booking.Date.Year
                    select b
                ).Count()
            }
        ).Distinct();

        var query = (
            from room in _dbContext.Rooms
            from bookingByRoom in bookingByRooms
                .Where(b => room.Radid == b.RoomId && b.Year == year)
            where room.IsDeleted == 0
            orderby bookingByRoom.Total descending
            select new Room {
                Radid = room.Radid,
                Name = room.Name,
                TotalBook = bookingByRoom.Total
            }
            // select new {
            //     RoomId = room.Radid,
            //     Name = room.Name,
            //     Total = bookingByRoom.Total
            // }
        ).Take(5);

        var result = await query.ToListAsync();

        return result;
    }

    // Dipakai di Master/Base - Access Door
    public async Task<IEnumerable<object>> GetAllRoomItemAsync(bool withIsDisabled = true)
    {
        var query = from room in _dbContext.Rooms
                    from roomAutomation in _dbContext.RoomAutomations
                            .Where(q => q.Id == room.AutomationId).DefaultIfEmpty()
                    from building in _dbContext.Buildings
                            .Where(q => q.Id == room.BuildingId).DefaultIfEmpty()
                    where room.IsDeleted == 0
                    orderby room.Name ascending
                    select new {
                        room,
                        roomAutomation = new { 
                            RaName = roomAutomation != null ? roomAutomation.Name : "",
                            RaId = roomAutomation != null ? roomAutomation.Id : null },
                        building = new { 
                            BuildingName = building != null ? building.Name : "", 
                            BuildingDetail = building != null ? building.DetailAddress : "",
                            BuildingGoogleMap = building != null ? building.GoogleMap : "" }
                    };

        if (!withIsDisabled)
        {
            query = query.Where(q => q.room.IsDisabled == 0);
        }

        var list = await query.ToListAsync();
        
        return list;
    }

    public async Task<int> GetCountRoomItemAsync(bool withIsDisabled = true)
    {
        var query = from room in _dbContext.Rooms
                    from roomAutomation in _dbContext.RoomAutomations
                            .Where(q => q.Id == room.AutomationId).DefaultIfEmpty()
                    from building in _dbContext.Buildings
                            .Where(q => q.Id == room.BuildingId).DefaultIfEmpty()
                    where room.IsDeleted == 0
                    orderby room.Name ascending
                    select new {
                        room,
                        roomAutomation = new { 
                            RaName = roomAutomation != null ? roomAutomation.Name : "",
                            RaId = roomAutomation != null ? roomAutomation.Id : null },
                        building = new { 
                            BuildingName = building != null ? building.Name : "", 
                            BuildingDetail = building != null ? building.DetailAddress : "",
                            BuildingGoogleMap = building != null ? building.GoogleMap : "" }
                    };

        if (!withIsDisabled)
        {
            query = query.Where(q => q.room.IsDisabled == 0);
        }

        var count = await query.CountAsync();
        
        return count;
    }

    // Dipakai di Master/Base - Display Signage
    public async Task<IEnumerable<object>> GetAllRoomRoomDisplayItemAsync()
    {
        var query = from room in _dbContext.Rooms
                    from roomDisplay in _dbContext.RoomDisplays
                            .Where(q => q.RoomId == room.Radid).DefaultIfEmpty()
                    from building in _dbContext.Buildings
                            .Where(q => q.Id == room.BuildingId).DefaultIfEmpty()
                    where room.IsDeleted == 0
                    orderby room.Name ascending
                    select new {
                        room,
                        roomDisplay = new {
                            RoomDisplayBackground = roomDisplay != null ? roomDisplay.Background : ""
                        },
                        building = new { 
                            BuildingName = building != null ? building.Name : "", 
                            BuildingDetail = building != null ? building.DetailAddress : "",
                            BuildingGoogleMap = building != null ? building.GoogleMap : "" }
                    };

        var list = await query
                            .GroupBy(q => q.room.Radid)
                            .Select(q => q.FirstOrDefault())
                            .ToListAsync();
        
        return list!;
    }

    // Dipakai di Master/Base - Display Signage
    public async Task<IEnumerable<Room>> GetAllRoomWithRadidsItemAsycn(string[] radIds)
    {
        var query = from room in _dbContext.Rooms
                    where room.IsDeleted == 0
                    // && radIds.Any(radid => room.Radid.Contains(radid))
                    && radIds.Contains(room.Radid)
                    orderby room.Name ascending
                    select room;

        var list = await query.ToListAsync();
        
        return list;
    }

    public async Task<IEnumerable<object>> GetAllRoomAvailableAsync(Room entity)
    {
        // Console.WriteLine("-------------------GetAllRoomAvailableAsync-------------------");
        // Console.WriteLine($"{System.Text.Json.JsonSerializer.Serialize(entity)}");
        var qRoom = from room in _dbContext.Rooms
                    from building in _dbContext.Buildings
                        .Where(b => room.BuildingId == b.Id).DefaultIfEmpty()
                    where room.IsDeleted == 0
                    select new {
                        room,
                        Building = new {
                            BuildingName = building.Name
                        }
                    };

        if (entity.Radid != null)
        {
            qRoom = qRoom.Where(q => q.room.Radid == entity.Radid);
        }

        if (entity.KindRoom != null)
        {
            qRoom = qRoom.Where(q => q.room.KindRoom == entity.KindRoom);
        }

        if (entity.BuildingId > 0)
        {
            qRoom = qRoom.Where(q => q.room.BuildingId == entity.BuildingId);
        }

        if (entity.KindRoom == "trainingroom")
        {

            if (entity.WorkDay != null)
            {
                qRoom = qRoom.Where(q => entity.WorkDay!.Any(WorkDay => q.room.WorkDay!.Contains(WorkDay)));

            }

            if (entity.WorkStart != null && entity.WorkEnd != null)
            {
                qRoom = qRoom.Where(q => 
                    // string.Compare(q.room.WorkStart, entity.WorkStart) <= 0 
                    // && string.Compare(q.room.WorkEnd, entity.WorkEnd) >= 0 
                    string.Compare(q.room.WorkStart, entity.WorkEnd) <= 0 
                    && string.Compare(q.room.WorkEnd, entity.WorkStart) >= 0
                );

            }

            DateTime? startDate = entity.WorkDate.HasValue && !string.IsNullOrEmpty(entity.WorkStart) 
                ? entity.WorkDate.Value.ToDateTime(TimeOnly.Parse(entity.WorkStart)) 
                : null;
            DateTime? endDate = entity.WorkDateUntil.HasValue && !string.IsNullOrEmpty(entity.WorkEnd) 
                ? entity.WorkDateUntil.Value.ToDateTime(TimeOnly.Parse(entity.WorkEnd)) 
                : null;

            if (startDate != null && endDate != null)
            {
                if (entity.WorkDate == entity.WorkDateUntil)
                {
                    qRoom = qRoom.Where(q => 
                        !_dbContext.Bookings
                            .Any(b => 
                                b.RoomId == q.room.Radid 
                                && b.Start <= endDate 
                                && b.End >= startDate
                            ));

                }
                else
                {
                    qRoom = qRoom.Where(q => 
                        !_dbContext.Bookings
                            .Any(b => 
                                b.RoomId == q.room.Radid 
                                && b.Start.Date >= startDate.Value.Date
                                && b.End <= endDate.Value.Date

                                // && b.Start.TimeOfDay >= startDate.Value.TimeOfDay
                                // && b.End.TimeOfDay <= endDate.Value.TimeOfDay
                                && b.Start.TimeOfDay <= endDate.Value.TimeOfDay
                                && b.End.TimeOfDay >= startDate.Value.TimeOfDay
                            ));
                }
            }
        }
        else 
        {
            if (entity.IsAllDay == true)
            {
                DateOnly workDate = (entity.WorkDate != null) ? (DateOnly)entity.WorkDate : DateOnly.FromDateTime(DateTime.Now);
                // Console.WriteLine($"workDate: {workDate}");

                /* qRoom = qRoom.Where(q => (
                    from b in _dbContext.Bookings
                    where q.room.Radid == b.RoomId
                    && b.Date == workDate
                    select b
                ).Count() <  1); */
                qRoom = qRoom.Where(q => 
                    !_dbContext.Bookings  
                        .Any(b => 
                            b.RoomId == q.room.Radid 
                            && b.Date == workDate
                        ));
            } 
            else 
            {
                if (entity.WorkDay != null)
                {
                    /* var dayKeywords = entity.WorkDay.ToArray();
                    qRoom = from qroom in qRoom
                            where dayKeywords.Any(keyword => qroom.room.WorkDay!.Contains(keyword))
                            select qroom; */

                    qRoom = qRoom.Where(q => entity.WorkDay!.Any(WorkDay => q.room.WorkDay!.Contains(WorkDay)));

                }

                if (entity.WorkStart != null && entity.WorkEnd != null)
                {
                    qRoom = qRoom.Where(q => 
                        // string.Compare(q.room.WorkStart, entity.WorkStart) <= 0 
                        // && string.Compare(q.room.WorkEnd, entity.WorkEnd) >= 0 
                        string.Compare(q.room.WorkStart, entity.WorkEnd) <= 0 
                        && string.Compare(q.room.WorkEnd, entity.WorkStart) >= 0
                    );
                }

                if (entity.WorkDay != null && entity.WorkStart != null && entity.WorkEnd != null)
                {
                    DateTime? startDate = entity.WorkDate.HasValue && !string.IsNullOrEmpty(entity.WorkStart) 
                        ? entity.WorkDate.Value.ToDateTime(TimeOnly.Parse(entity.WorkStart)) 
                        : null;
                    DateTime? endDate = entity.WorkDate.HasValue && !string.IsNullOrEmpty(entity.WorkEnd) 
                        ? entity.WorkDate.Value.ToDateTime(TimeOnly.Parse(entity.WorkEnd)) 
                        : null;

                    qRoom = qRoom.Where(q => 
                        !_dbContext.Bookings
                            .Any(b => 
                                b.RoomId == q.room.Radid 
                                && b.Start <= endDate 
                                && b.End >= startDate
                            ));
                }
            }
        }

        if (entity.FacilityRoom != null)
        {
            /* var facKeywords = entity.FacilityRoom.ToArray();
            qRoom = from qroom in qRoom
                    where facKeywords.Any(keyword => qroom.room.FacilityRoom.Contains(keyword))
                    select qroom; */

            qRoom = qRoom.Where(q => entity.FacilityRoom!.Any(FacilityRoom => q.room.FacilityRoom!.Contains(FacilityRoom)));
        }

        if (entity.Capacities.Count() > 0)
        {
            int[] capacities = entity.Capacities;
            if (entity.Capacities.Count() > 1)
            {
                qRoom = qRoom.Where(q => q.room.Capacity >= capacities[0] && q.room.Capacity <= capacities[1]);
            } else {
                qRoom = qRoom.Where(q => q.room.Capacity >= capacities[0]);
            }
        }

        var list = await qRoom.ToListAsync();

        return list;
    }
    
    public async Task<Room?> UpdateRoom(Room entity)
    {
        var existingEntity = await _dbContext.Rooms.FindAsync(entity.Id);
        if (existingEntity == null) return null; // Handle if entity not found

        _dbContext.Entry(existingEntity).CurrentValues.SetValues(entity); // Update all properties
        await _dbContext.SaveChangesAsync(); // Save changes
        return existingEntity; // Return the updated entity
    }

    public async Task<IEnumerable<RoomDetailDto>> GetByDataRoomByRadId(string? radId)
    {
        var query = from room in _dbContext.Rooms
                    join roomAutomation in _dbContext.RoomAutomations
                        on room.AutomationId equals roomAutomation.Id into roomAutomationGroup
                    from roomAutomation in roomAutomationGroup.DefaultIfEmpty()
                    where room.IsDeleted == 0 && room.Radid == radId
                    select new RoomDetailDto
                    {
                        Id = room.Id,
                        Name = room.Name,
                        Description = room.Description,
                        Capacity = room.Capacity,
                        GoogleMap = room.GoogleMap,
                        RaName = roomAutomation != null ? roomAutomation.Name : null,
                        RaId = roomAutomation != null ? roomAutomation.Id : null,
                    };

        return await query.ToListAsync();
    }
    public async Task<List<Room>> GetDataRoomDisplayByListID(string[]? radIds)
    {
        if (radIds == null || radIds.Length == 0)
        {
            return new List<Room>();
        }

        var query = from room in _dbContext.Rooms
                    where room.IsDeleted == 0 && radIds.Contains(room.Radid)
                    orderby room.Name
                    select room;

        return await query.ToListAsync();
    }


}

