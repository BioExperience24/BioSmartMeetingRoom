namespace _7.Entities.Models;

public partial class PantryDetailMenuVariant : BaseEntity
{

    public int MenuId { get; set; }

    public string Name { get; set; } = null!;

    public int Multiple { get; set; }

    public int Min { get; set; }

    public int Max { get; set; }

}
