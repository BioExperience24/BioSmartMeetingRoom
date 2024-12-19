

namespace _7.Entities.Models;

public partial class AccessControllerFalco : BaseLongEntity
{
    // public int Id { get; set; }

    public string AccessId { get; set; } = null!;

    public string GroupAccess { get; set; } = null!;

    public string? UnitNo { get; set; }

    public string FalcoIp { get; set; } = null!;

    // public int IsDeleted { get; set; }
}
