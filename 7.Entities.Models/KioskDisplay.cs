

namespace _7.Entities.Models;

public partial class KioskDisplay : BaseLongEntity
{
    // public int Id { get; set; }

    public string? DisplaySerial { get; set; }

    public string? DisplayType { get; set; }

    public string? DisplayName { get; set; }

    public string? Background { get; set; }

    public string? RunningText { get; set; }

    public string? TitleKiosk { get; set; }

    public string? DisplayUuid { get; set; }

    public string? DisplayHwSerial { get; set; }

    public string? Koordinate { get; set; }

    public short? IsLogged { get; set; }

    public string? LastLogged { get; set; }

    public DateTime? UpdatedAt { get; set; }

    // public int? IsDeleted { get; set; }
}
