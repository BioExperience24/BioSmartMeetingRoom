

using System.Text.Json.Serialization;

namespace _4.Data.ViewModels
{
    public class HttpUrlViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("headers")]
        public List<string> Headers { get; set; } = null!;

        [JsonPropertyName("is_deleted")]
        public short IsDeleted { get; set; }

        [JsonPropertyName("is_enable")]
        public short IsEnable { get; set; }
    }
}