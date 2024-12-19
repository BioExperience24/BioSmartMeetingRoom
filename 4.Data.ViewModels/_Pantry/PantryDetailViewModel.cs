using Microsoft.AspNetCore.Http;

namespace _4.Data.ViewModels;

public class PantryDetailViewModel : BaseLongViewModel
{
    public int pantry_id { get; set; }
    public string name { get; set; } = null!;
    public string? description { get; set; } = null!;
    public string? pic { get; set; } = null!;
    public int prefix_id { get; set; }
    public int rasio { get; set; }
    public string? note { get; set; } = "";
    public int? price { get; set; }
    public DateTime created_at { get; set; }
    public DateTime updated_at { get; set; }

    //adds on
    public string? picBase64 { get; set; } = null;
    public IFormFile? image { get; set; }
}

