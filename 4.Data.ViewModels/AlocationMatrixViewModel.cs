

using System.Text.Json.Serialization;

namespace _4.Data.ViewModels
{
    public class AlocationMatrixViewModel
    {
        [JsonPropertyName("alocation_id")]
        public string? AlocationId { get; set; }

        [JsonPropertyName("nik")]
        public string? Nik { get; set; }
    }
}