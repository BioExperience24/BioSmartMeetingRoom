using _4.Data.ViewModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


public class RoomAutomationViewModel : BaseLongViewModel
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("ip_address")]
    public string IpAddress { get; set; } = null!;

    [JsonPropertyName("serial")]
    public string Serial { get; set; } = null!;

    [JsonPropertyName("room")]
    public string Room { get; set; } = null!;

    [JsonPropertyName("devices")]
    public string Devices { get; set; } = null!;

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }

}