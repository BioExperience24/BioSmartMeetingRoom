using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace _7.Entities.Models;

public partial class RoomUserCheckin
{

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("key")]
    public string? Key { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("is_deleted")]
    public int? IsDeleted { get; set; }
}
