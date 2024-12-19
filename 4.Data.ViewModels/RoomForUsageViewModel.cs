using _4.Data.ViewModels;
using System.Text.Json.Serialization;

public class RoomForUsageViewModel
{

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("is_deleted")]
    public int? IsDeleted { get; set; }
}