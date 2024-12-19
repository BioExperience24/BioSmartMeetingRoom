namespace _4.Data.ViewModels;

public class PantryDetailMenuVariantViewModel
    : BaseViewModel
{
    public int menuid { get; set; }
    public string name { get; set; } = null!;
    public int multiple { get; set; } = 1;
    public int rule { get; set; } = 1;
    public int? min { get; set; } = 0;
    public int? max { get; set; } = 0;
    public List<PantryDetailMenuVariantDetailViewModel>? variant { get; set; }
}

public class PantryVariantDataAndDetail
{
    public PantryDetailMenuVariantViewModel? data { get; set; }
    public List<PantryDetailMenuVariantDetailViewModel>? detail { get; set; }
}