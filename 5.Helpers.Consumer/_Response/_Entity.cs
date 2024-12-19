using System.Text.Json.Serialization;

namespace _5.Helpers.Consumer._Response
{
    public class _EntityJson
    {
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("msg")]
        public string? Message { get; set; }

        // [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("collection")]
        public object? Collection { get; set; }
    }
}