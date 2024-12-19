

namespace _7.Entities.Models;

public partial class AccessControl : BaseEntity
{
    public string? Name { get; set; }

    public string? IpController { get; set; }

    public string? AccessId { get; set; }

    public int? Channel { get; set; }

    public string ControllerList { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string RoomControllerFalco { get; set; } = null!;

    public int? Delay { get; set; }

    public string? ModelController { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

}
