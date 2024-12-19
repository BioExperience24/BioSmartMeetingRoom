using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace _7.Entities.Models;

public partial class BuildingFloor : BaseLongEntity
{
    [JsonPropertyName("_generate")]
    public int Generate { get; set; }

    [JsonPropertyName("building_id")]
    public long? BuildingId { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("position")]
    public int? Position { get; set; }

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

    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; set; }

    [JsonPropertyName("created_by")]
    public string? CreatedBy { get; set; }

    [JsonPropertyName("updated_by")]
    public string? UpdatedBy { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    // public int? IsDeleted { get; set; }
}
