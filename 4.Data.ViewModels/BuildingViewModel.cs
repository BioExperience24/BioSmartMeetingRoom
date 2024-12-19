using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _4.Data.ViewModels
{
    public class BuildingViewModel : BaseLongViewModel
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("image")]
        public string? Image { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("timezone")]
        public string? Timezone { get; set; }

        [JsonPropertyName("detail_address")]
        public string? DetailAddress { get; set; }

        [JsonPropertyName("google_map")]
        public string? GoogleMap { get; set; }

        [JsonPropertyName("koordinate")]
        public string? Koordinate { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonPropertyName("count_room")]
        public int? CountRoom { get; set; }

        [JsonPropertyName("count_floor")]
        public int? CountFloor { get; set; }

        [JsonPropertyName("count_desk")]
        public int? CountDesk { get; set; }

        [JsonPropertyName("encrypt")]
        public string? Encrypt { get; set; }
    }

    public class BuildingVMDefaultFR
    {
        [BindProperty(Name = "image")]
        public IFormFile? FileImage { get; set; }

        public string? Image { get; set; }

        [BindProperty(Name = "name")]
        public string? Name { get; set; }

        [BindProperty(Name = "description")]
        public string? Description { get; set; }

        [BindProperty(Name = "detail_address")]
        public string? DetailAddress { get; set; }

        [BindProperty(Name = "timezone")]
        public string? Timezone { get; set; }

        [BindProperty(Name = "google_map")]
        public string? GoogleMap { get; set; }
    }

    public class BuildingVMDeleteFR
    {
        [BindProperty(Name = "id")]
        public long Id { get; set; }

        [BindProperty(Name = "name")]
        public string? Name { get; set; }
    }
}