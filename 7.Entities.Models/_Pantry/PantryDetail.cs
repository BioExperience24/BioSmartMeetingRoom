namespace _7.Entities.Models;

public partial class PantryDetail : BaseLongEntity
{
    public int PantryId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Pic { get; set; } = null!;

    public int PrefixId { get; set; }

    public int Rasio { get; set; }

    public string Note { get; set; } = null!;

    public int? Price { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
