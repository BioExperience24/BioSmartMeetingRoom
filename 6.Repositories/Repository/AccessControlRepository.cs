

using System.Text.Json;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Identity.Client;

namespace _6.Repositories.Repository;

public class AccessControlRepository : BaseRepository<AccessControl>
{
    private readonly MyDbContext _dbContext;
    public AccessControlRepository(MyDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<object>> GetAllItemAsync()
    {
        var query = from accessControl in _dbContext.AccessControls
                    where accessControl.IsDeleted == 0
                    orderby accessControl.Id ascending
                    select new {
                        accessControl,
                        Room = (
                            from accessIntegrated in _dbContext.AccessIntegrateds
                            from room in _dbContext.Rooms
                                .Where(q => q.Radid == accessIntegrated.RoomId)
                            where accessIntegrated.AccessId == accessControl.Id
                            && accessIntegrated.IsDeleted == 0
                            && room.IsDeleted == 0
                            select accessIntegrated
                        ).Count()
                    };

        var list = await query.ToListAsync();
        
        return list;
    }

    public async Task<object?> GetItemByIdAsync(string id)
    {
        var query = from accessControl in _dbContext.AccessControls
                    from accessControllerFalco in _dbContext.AccessControllerFalcos
                            .Where(q => q.AccessId == accessControl.Id && q.IsDeleted == 0)
                            .DefaultIfEmpty()
                    where accessControl.Id == id && accessControl.IsDeleted == 0
                    orderby accessControl.Id ascending
                    select new {
                        accessControl,
                        AccessControllerFalco = new {
                            FalcoUnitNo = accessControllerFalco != null ? accessControllerFalco.UnitNo : "",
                            FalcoGroupAccess = accessControllerFalco != null ? accessControllerFalco.GroupAccess : ""
                        },
                        Room = (
                            from accessIntegrated in _dbContext.AccessIntegrateds
                            where accessIntegrated.AccessId == accessControl.Id
                            && accessIntegrated.IsDeleted == 0
                            select accessIntegrated
                        ).Count()
                    };

        var item = await query.FirstOrDefaultAsync();
        
        return item;
    }

    // TODO: Jika menu meeting room - room management sudah di buat, merge repo ini
    // Dipakai di Master/Base - Access Door
    public async Task<IEnumerable<object>> GetAllItemRoomAsync()
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

        var list = await query.ToListAsync();
        
        return list;
    }

    // TODO: Jika menu meeting room - room management sudah di buat, merge repo ini
    // Dipakai di Master/Base - Display Signage
    public async Task<IEnumerable<object>> GetAllItemRoomRoomDisplayAsync()
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

    // TODO: Jika menu meeting room - room management sudah di buat, merge repo ini
    // Dipakai di Master/Base - Display Signage
    public async Task<IEnumerable<Room>> GetAllItemRoomWithRadidsAsycn(string[] radIds)
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

    public async Task<DoorAccessDto> CheckDataDoorOpen(string radId, string model = "")
    {
        var query = from accessControl in _dbContext.AccessControls
                    from accessIntegrated in _dbContext.AccessIntegrateds
                            .Where(q => q.AccessId == accessControl.Id).DefaultIfEmpty()
                    where accessControl.IsDeleted == 0 && accessIntegrated.RoomId == radId
                    select new DoorAccessDto
                    {
                        RoomId = accessIntegrated.RoomId ?? string.Empty,
                        Id = accessControl.Id ?? string.Empty,
                        AccessId = accessControl.AccessId ?? string.Empty,
                        Type = accessControl.Type ?? string.Empty,
                        IpController = accessControl.IpController ?? "0.0.0.0",
                        Delay = accessControl.Delay,
                        Channel = accessControl.Channel,
                        Name = accessControl.Name ?? string.Empty,
                        ModelController = accessControl.ModelController ?? "reader"
                    };

        // Apply filtering if model is not empty
        if (!string.IsNullOrEmpty(model))
        {
            query = query.Where(q => q.ModelController == model);
        }

        var data = await query.FirstOrDefaultAsync();

        return data!;
    }


}

public class AccessControlConfiguration : IEntityTypeConfiguration<AccessControl>
{
    public void Configure(EntityTypeBuilder<AccessControl> entity)
    {
        entity.HasKey(e => e.Id).HasName("PK_access_control_id");

        entity.ToTable("access_control", "smart_meeting_room");

        entity.Property(e => e.Id)
            .HasMaxLength(15)
            .HasDefaultValue("")
            .HasColumnName("id");
        entity.Property(e => e.AccessId)
            .HasMaxLength(255)
            .HasDefaultValueSql("(NULL)")
            .HasColumnName("access_id");
        entity.Property(e => e.Channel)
            .HasDefaultValueSql("(NULL)")
            .HasColumnName("channel");
        entity.Property(e => e.ControllerList)
            .HasMaxLength(100)
            .HasColumnName("controller_list");
        entity.Property(e => e.CreatedAt)
            .HasPrecision(0)
            .HasDefaultValueSql("(NULL)")
            .HasColumnName("created_at");
        entity.Property(e => e.CreatedBy)
            .HasMaxLength(100)
            .HasDefaultValueSql("(NULL)")
            .HasColumnName("created_by");
        entity.Property(e => e.Delay)
            .HasDefaultValue(3)
            .HasColumnName("delay");
        entity.Property(e => e.IpController)
            .HasMaxLength(100)
            .HasDefaultValue("0.0.0.0")
            .HasColumnName("ip_controller");
        entity.Property(e => e.IsDeleted)
            .HasDefaultValue(0)
            .HasColumnName("is_deleted");
        entity.Property(e => e.ModelController)
            .HasMaxLength(255)
            .HasDefaultValue("reader")
            .HasColumnName("model_controller");
        entity.Property(e => e.Name)
            .HasMaxLength(100)
            .HasDefaultValue("")
            .HasColumnName("name");
        entity.Property(e => e.RoomControllerFalco)
            .HasMaxLength(100)
            .HasColumnName("room_controller_falco");
        entity.Property(e => e.Type)
            .HasMaxLength(100)
            .HasColumnName("type");
        entity.Property(e => e.UpdatedAt)
            .HasPrecision(0)
            .HasDefaultValueSql("(NULL)")
            .HasColumnName("updated_at");
        entity.Property(e => e.UpdatedBy)
            .HasMaxLength(100)
            .HasDefaultValueSql("(NULL)")
            .HasColumnName("updated_by");
    }
}