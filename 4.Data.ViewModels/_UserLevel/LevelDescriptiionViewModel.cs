using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace _4.Data.ViewModels
{
    public class LevelDescriptiionViewModel : BaseLongViewModel
    {
        // [JsonPropertyName("id")]
        // public int Id { get; set; }

        [JsonPropertyName("level_id")]
        public int LevelId { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        // [JsonPropertyName("is_deleted")]
        // public int IsDeleted { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }
}