﻿using System.Text.Json.Serialization;
using _4.Data.ViewModels;

public class RoomForUsageViewModel : BaseLongViewModel
{

    // [JsonPropertyName("id")]
    // public int Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    // [JsonPropertyName("is_deleted")]
    // public int? IsDeleted { get; set; }
}
