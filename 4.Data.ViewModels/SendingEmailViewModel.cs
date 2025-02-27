using System.Text.Json.Serialization;
using _4.Data.ViewModels;

public class SendingEmailViewModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("batch")]
    public string Batch { get; set; } = null!;

    [JsonPropertyName("type")]
    public int? Type { get; set; }

    [JsonPropertyName("booking_id")]
    public string BookingId { get; set; } = null!;

    [JsonPropertyName("pending")]
    public string Pending { get; set; } = null!;

    [JsonPropertyName("error_sending")]
    public int ErrorSending { get; set; }

    [JsonPropertyName("success")]
    public int Success { get; set; }

    [JsonPropertyName("is_status")]
    public int? IsStatus { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [JsonPropertyName("is_deleted")]
    public short IsDeleted { get; set; }
}
