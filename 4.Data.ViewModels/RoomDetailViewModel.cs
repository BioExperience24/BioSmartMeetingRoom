using _4.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

public class RoomDetailViewModel
{
    [JsonPropertyName("id")]
    public long? Id { get; set; }

    [JsonPropertyName("room_id")]
    public long RoomId { get; set; }

    [JsonPropertyName("facility_id")]
    public long FacilityId { get; set; }

    [JsonPropertyName("datetime")]
    public DateTime? Datetime { get; set; }
}