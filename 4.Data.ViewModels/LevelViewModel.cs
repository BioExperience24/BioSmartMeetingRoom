using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace _4.Data.ViewModels
{
    // public class LevelViewModel : BaseViewModel
    public class LevelViewModel : BaseLongViewModel
    {
        // [JsonPropertyName("id")]
        // public long Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("default_menu")]
        public int DefaultMenu { get; set; }

        // [JsonPropertyName("created_by")]
        // public int? CreatedBy { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        // [JsonPropertyName("is_deleted")]
        // public short IsDeleted { get; set; }
    }

    public class LevelVMUpdateFR
    {
        [BindProperty(Name = "id")]
        public long? Id { get; set; }

        [BindProperty(Name = "name")]
        public string? Name { get; set; }
    }
}