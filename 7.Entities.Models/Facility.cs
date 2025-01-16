namespace _7.Entities.Models;

public partial class Facility : BaseLongEntity
{

    public string Name { get; set; } = null!;

    public string GoogleIcon { get; set; } = null!;

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    // public int IsDeleted { get; set; }
}
