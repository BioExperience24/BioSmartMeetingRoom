﻿using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _4.Data.ViewModels;

public class BeaconFloorViewModel : BaseLongViewModel
{

    [JsonPropertyName("building_id")]
    public long? BuildingId { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("image")]
    public string? Image { get; set; }

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

    [JsonPropertyName("created_by")]
    public string? CreatedBy { get; set; }

    [JsonPropertyName("updated_by")]
    public string? UpdatedBy { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [JsonPropertyName("building_name")]
    public string? BuildingName { get; set; }
}

public class BeaconFloorVMDefaultFR
{
    [BindProperty(Name = "building_id")]
    public long? BuildingId { get; set; }

    [BindProperty(Name = "name")]
    public string? Name { get; set; }

    [BindProperty(Name = "image")]
    public IFormFile? FileImage { get; set; }

    [BindProperty(Name = "pixel")]
    public string? Pixel { get; set; }

    [BindProperty(Name = "floor_length")]
    public double? FloorLength { get; set; }

    [BindProperty(Name = "floor_width")]
    public double? FloorWidth { get; set; }

    [BindProperty(Name = "meter_per_px")]
    public string? MeterPerPx { get; set; }
}

public class BeaconFloorVMDetailFR
{
    [BindProperty(Name = "id")]
    public long Id { get; set; }
}

public class BeaconFloorVMUpdateFR : BeaconFloorVMDefaultFR
{
    [BindProperty(Name = "id")]
    public long Id { get; set; }
}