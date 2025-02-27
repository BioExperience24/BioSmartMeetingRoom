using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace _4.Data.ViewModels;

public class PantryDetailViewModel : BaseLongViewModel
{
    public long pantry_id { get; set; }
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
    public long? menu_id { get; set; }
    public long? menuId { get; set; }
}

public class PantryDetailVMMenus
{
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("note")]
    public string Note { get; set; } = string.Empty;

    [JsonPropertyName("pantry_id")]
    public string PantryId { get; set; } = string.Empty;

    [JsonPropertyName("qty")]
    public string Qty { get; set; } = string.Empty;
}

