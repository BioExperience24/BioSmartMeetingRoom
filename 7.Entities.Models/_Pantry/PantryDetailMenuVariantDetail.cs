namespace _7.Entities.Models;

public partial class PantryDetailMenuVariantDetail : BaseLongEntity
{
    public string Name { get; set; } = null!;

    public string VariantId { get; set; } = null!;

    public int? Price { get; set; }

}
