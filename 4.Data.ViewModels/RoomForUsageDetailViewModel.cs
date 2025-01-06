using Newtonsoft.Json;
using System.Text.Json.Serialization;

public class RoomForUsageDetailViewModel
{

    [JsonProperty("room_id")]
    public long? RoomId { get; set; }

    [JsonProperty("room_usage_id")]
    public int? RoomUsageId { get; set; }

    [JsonProperty("min_cap")]
    public int? MinCap { get; set; }

    [JsonProperty("internal")]
    public int? Internal { get; set; }

    [JsonProperty("external")]
    public int? External { get; set; }
}

public class RoomForUsageDetailListViewModel
{
    [JsonPropertyName("room_id")]
    public long? RoomId { get; set; }

    [JsonPropertyName("room_usage_id")]
    public int? RoomUsageId { get; set; }

    [JsonPropertyName("min_cap")]
    public int? MinCap { get; set; } // Updated JSON property name to match "min_cap".

    [JsonPropertyName("internal")]
    public int? Internal { get; set; }

    [JsonPropertyName("external")]
    public int? External { get; set; }
}