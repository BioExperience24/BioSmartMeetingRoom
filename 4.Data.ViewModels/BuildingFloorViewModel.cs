using System;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _4.Data.ViewModels;

public class BuildingFloorViewModel : BaseLongViewModel
{
    [JsonPropertyName("building_id")]
    public long? BuildingId { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("position")]
    public int? Position { get; set; }

    [JsonPropertyName("image")]
    public string Image { get; set; } = string.Empty;

    [JsonPropertyName("pixel")]
    public string? Pixel { get; set; }

    [JsonPropertyName("floor_length")]
    public double? FloorLength { get; set; }

    [JsonPropertyName("floor_width")]
    public double? FloorWidth { get; set; }

    [JsonPropertyName("meter_per_px")]
    public string? MeterPerPx { get; set; }

    [JsonPropertyName("meter_per_px2")]
    public string? MeterPerPx2 { get; set; }

    [JsonPropertyName("plus_width")]
    public double? PlusWidth { get; set; }

    [JsonPropertyName("plus_height")]
    public double? PlusHeight { get; set; }

    [JsonPropertyName("center_x")]
    public int? CenterX { get; set; }

    [JsonPropertyName("center_y")]
    public int? CenterY { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    // [JsonIgnore]
    [JsonPropertyName("enc_building_id")]
    public string? BuildingEncId { get; set; }
    
    // [JsonIgnore]
    [JsonPropertyName("enc_id")]
    public string? EncId { get; set; }
}

public class BuildingFloorVMListQuery
{
    [FromQuery(Name = "building")]
    public string? Building { get; set; }
    
    [FromQuery(Name = "_")]
    public long? Token { get; set; }
}

public class BuildingFloorVMCreateFR
{
    [BindProperty(Name = "building_id")]
    public string? BuildingId { get; set; }
    
    [BindProperty(Name = "name")]
    public string? Name { get; set; }
}

public class BuildingFloorVMShowFR
{
    [BindProperty(Name = "building")]
    public string? Building { get; set; }

    [BindProperty(Name = "floor")]
    public string? Floor { get; set; }
}

public class BuildingFloorVMUpdateFR : BuildingFloorVMCreateFR
{
    [BindProperty(Name = "id")]
    public string? Id { get; set; }
}

public class BuildingFloorVMDeleteFR
{
    [BindProperty(Name = "id")]
    public string? Id { get; set; }
}

public class BuildingFloorVMUploadFR : BuildingFloorVMDeleteFR
{
    [BindProperty(Name = "image")]
    public IFormFile? FileImage { get; set; }
}
