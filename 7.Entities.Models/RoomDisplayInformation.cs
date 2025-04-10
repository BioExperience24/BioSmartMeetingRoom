using System.ComponentModel.DataAnnotations.Schema;

namespace _7.Entities.Models;

public partial class RoomDisplayInformation
{
    [NotMapped]
    public int Generate { get; set; }
    public long? DisplayId { get; set; }
    public string? RoomId { get; set; }
    public string? Icon { get; set; }
    public int? Distance { get; set; }
}

public class RoomDisplayInformationSelect : RoomDisplayInformation
{
    public string? RoomName { get; set; }
}

public class RoomDisplayInformationMeetingDTO : RoomDisplayInformation
{
    public string RoomName { get; set; } = string.Empty;
    public int? Capacity { get; set; }
    public string BookingId { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public TimeSpan EndMeeting { get; set; } 
    public int IsAlive { get; set; }
    public int? IsApprove { get; set; }
    public int? IsExpired { get; set; }
    public int? IsCanceled { get; set; }
    public int? IsPrivate { get; set; }
    public string OrganizerName { get; set; } = string.Empty;
    public string? PinRoom { get; set; } = string.Empty;
    public string? BuildingName { get; set; } = string.Empty;
    public string? FloorName { get; set; } = string.Empty;
}
