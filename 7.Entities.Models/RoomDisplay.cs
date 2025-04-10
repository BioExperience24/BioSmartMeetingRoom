
namespace _7.Entities.Models;

public partial class RoomDisplay : BaseLongEntity
{
    // public long Id { get; set; }

    public string? RoomId { get; set; }

    public string? DisplaySerial { get; set; }

    public string? Type { get; set; }

    public string? Background { get; set; }

    public int? BackgroundUpdate { get; set; }

    public string? ColorOccupied { get; set; }

    public string? ColorAvailable { get; set; }

    public int? EnableSignage { get; set; }

    public string? SignageType { get; set; }

    public string? SignageMedia { get; set; }

    public int? SignageUpdate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    // public int? IsDeleted { get; set; }

    public int? StatusSync { get; set; }

    public int? Enabled { get; set; }

    public string? HardwareUuid { get; set; }

    public string? HardwareInfo { get; set; }

    public DateTime? HardwareLastsync { get; set; }

    public string? RoomSelect { get; set; }

    public string? DisableMsg { get; set; }

    public string? Name { get; set; }
    
    public string? Description { get; set; }

    public long? BuildingId { get; set; }

    public long? FloorId { get; set; }
}

public class RoomDisplaySelect : RoomDisplay
{
    public string? RoomName { get; set; }
    public string? BuildingName { get; set; }
    public string? FloorName { get; set; }
}