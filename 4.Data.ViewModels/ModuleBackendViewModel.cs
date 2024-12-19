

using System.Text.Json.Serialization;

namespace _4.Data.ViewModels
{
    public class ModuleBackendViewModel
    {
        [JsonPropertyName("module_id")]
        public int ModuleId { get; set; }

        [JsonPropertyName("module_text")]
        public string ModuleText { get; set; } = null!;

        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("module_serial")]
        public string? ModuleSerial { get; set; }

        [JsonPropertyName("is_enabled")]
        public int IsEnabled { get; set; }
    }
}