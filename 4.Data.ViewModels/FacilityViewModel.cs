using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace _4.Data.ViewModels;


public class FacilityViewModel : BaseLongViewModel
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("google_icon")]
    public string GoogleIcon { get; set; } = null!;

    [JsonPropertyName("created_by")]
    public int? CreatedBy { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime? UpdatedAt { get; set; }
}
public class FacilityVMResponse
{
    [JsonPropertyName("error")]
    public string? Error { get; set; }

    [JsonPropertyName("data")]
    public List<FacilityVMProp>? Data { get; set; }
}
public class FacilityVMProp
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = null!;

    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("google_icon")]
    public string GoogleIcon { get; set; } = null!;

    [JsonPropertyName("created_by")]
    public int? CreatedBy { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [JsonPropertyName("is_deleted")]
    public short? IsDeleted { get; set; }
}

public class FacilityCreateViewModelFR
{
    [BindProperty(Name = "name")]
    public string Name { get; set; } = string.Empty;

    [BindProperty(Name = "google_icon")]
    public string GoogleIcon { get; set; } = string.Empty;

    [BindProperty(Name = "created_by")]
    public int? CreatedBy { get; set; }
}

public class FacilityUpdateViewModelFR : FacilityCreateViewModelFR
{
    [BindProperty(Name = "id", SupportsGet = false)]
    public long Id { get; set; }

    [BindProperty(Name = "updated_at", SupportsGet = false)]
    public DateTime? UpdatedAt { get; set; }

    [BindProperty(Name = "is_deleted", SupportsGet = false)]
    public short? IsDeleted { get; set; }
}

public class FacilityDeleteViewModelFR
{
    [BindProperty(Name = "id")]
    public long Id { get; set; }

    [BindProperty(Name = "name")]
    public string Name { get; set; } = string.Empty;
}