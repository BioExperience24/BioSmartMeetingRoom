namespace _7.Entities.Models;

public partial class PantryMenuPaket : BaseEntity
{
    public long PantryId { get; set; }

    public string Name { get; set; } = null!;

    public int CreatedBy { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
    public virtual Pantry? Pantry { get; set; }
    public virtual ICollection<PantryMenuPaketD> PackageD { get; set; } = new List<PantryMenuPaketD>();


}

public class PantryPackageDTO
{
    public string? Id { get; set; }
    public string Name { get; set; }
    public long PantryId { get; set; }
    public string PantryName { get; set; }
}