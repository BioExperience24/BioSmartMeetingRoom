using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace _7.Entities.Models;

public partial class Building : BaseLongEntity
{
     //public int Id { get; set; }

    public int Generate { get; set; }

    public string? Name { get; set; }

    public string? Image { get; set; }

    public string? Description { get; set; }

    public string? Timezone { get; set; }

    public string? DetailAddress { get; set; }

    public string? GoogleMap { get; set; }

    public string? Koordinate { get; set; }

    // public string? IsDeleted { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? UpdatedBy { get; set; }
}

public class BuildingDataDto
{
    [JsonPropertyName("id")]
    public long? Id { get; set; }

    [JsonPropertyName("generate")]
    public int Generate { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("image")]
    public string? Image { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("timezone")]
    public string? Timezone { get; set; }

    [JsonPropertyName("detail_address")]
    public string? DetailAddress { get; set; }

    [JsonPropertyName("google_map")]
    public string? GoogleMap { get; set; }

    [JsonPropertyName("koordinate")]
    public string? Koordinate { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; set; }

    [JsonPropertyName("is_deleted")]
    public int? IsDeleted { get; set; }

    [JsonPropertyName("created_by")]
    public string? CreatedBy { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [JsonPropertyName("updated_by")]
    public DateTime? UpdatedBy { get; set; }

    [JsonPropertyName("count_room")]
    public int? CountRoom { get; set; }

    [JsonPropertyName("count_floor")]
    public int? CountFloor { get; set; }

    [JsonPropertyName("count_desk")]
    public int? CountDesk { get; set; }
}
