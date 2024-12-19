using _4.Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


public class RoomMergeDetailViewModel
{

    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("merge_room_id")]
    public string MergeRoomId { get; set; } = null!;

    [JsonPropertyName("room_id")]
    public long? RoomId { get; set; }
}
