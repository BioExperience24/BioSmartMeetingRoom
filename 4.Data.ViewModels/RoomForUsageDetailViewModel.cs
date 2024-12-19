using _4.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

public class RoomForUsageDetailViewModel
{
    [JsonPropertyName("room_id")]
    public long? RoomId { get; set; }

    [JsonPropertyName("room_usage_id")]
    public int? RoomUsageId { get; set; }

    [JsonPropertyName("mincap")]
    public int? MinCap { get; set; }

    [JsonPropertyName("internal")]
    public int? Internal { get; set; }

    [JsonPropertyName("external")]
    public int? External { get; set; }
}