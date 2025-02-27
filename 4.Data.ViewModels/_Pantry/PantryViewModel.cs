using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace _4.Data.ViewModels;


public class PantryViewModel : BaseLongViewModel
{
    public int? building_id { get; set; }
    public string name { get; set; } = null!;
    public string? detail { get; set; } = "";
    public string? location { get; set; } = "";
    public int days { get; set; }
    public TimeOnly opening_hours_start { get; set; }
    public TimeOnly opening_hours_end { get; set; }
    public bool is_show_price { get; set; }
    public string? pic { get; set; } = null;
    public string? employee_id { get; set; } = "";
    public int? created_by { get; set; }
    public DateTime? created_at { get; set; }
    public DateTime? updated_at { get; set; }
    public bool is_approval { get; set; }
    public bool is_internal { get; set; }
    public string? owner_pantry { get; set; }

    //adds on
    public string? picBase64 { get; set; } = null;
    public IFormFile? image { get; set; }
}

public class DatabookViewModel
{
    [JsonPropertyName("is_alive")]
    public int IsAlive { get; set; }
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;
    [JsonPropertyName("nikPic")]
    public string NikPic { get; set; } = string.Empty;

    [JsonPropertyName("date")]
    public string? Date { get; set; }

    [JsonPropertyName("start_str")]
    public string StartStr { get; set; } = string.Empty;

    [JsonPropertyName("end_str")]
    public string EndStr { get; set; } = string.Empty;

    [JsonPropertyName("pantry_package")]
    public string PantryPackage { get; set; } = string.Empty;

    [JsonPropertyName("pantry_detail")]
    public List<PantryTransaksiDViewModel> PantryDetail { get; set; }
}
