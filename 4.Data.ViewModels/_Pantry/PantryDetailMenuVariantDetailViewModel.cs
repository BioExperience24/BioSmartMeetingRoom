namespace _4.Data.ViewModels;

public class PantryDetailMenuVariantDetailViewModel
    : BaseLongViewModel
{
    public string? name { get; set; } = null!;
    public string? variant_id { get; set; } = null!;
    public int? price { get; set; }

}