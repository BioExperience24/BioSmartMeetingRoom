

using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace _4.Data.ViewModels
{
    public class AccessIntegratedViewModel : BaseLongViewModel
    {
        [JsonPropertyName("access_id")]
        public string? AccessId { get; set; }

        [JsonPropertyName("room_id")]
        public string? RoomId { get; set; }
    }

    public class AccessIntegratedVMRoom
    {
        [JsonPropertyName("room")]
        public string Room { get; set; } = string.Empty;

        [JsonPropertyName("status")]
        public int Status { get; set; }
    }

    public class AccessIntegratedVMAssignFR
    {
        [BindProperty(Name = "strdata")]
        public string StrDataJson { get; set; } = string.Empty;
        // public Dictionary<string, AccessIntegratedVMRoom> StrData { get; set; } = new Dictionary<string, AccessIntegratedVMRoom>();

        public Dictionary<string, AccessIntegratedVMRoom> StrData 
        {
            get
            {
                return string.IsNullOrWhiteSpace(StrDataJson)
                    ? new Dictionary<string, AccessIntegratedVMRoom>()
                    : JsonSerializer.Deserialize<Dictionary<string, AccessIntegratedVMRoom>>(StrDataJson) 
                    ?? new Dictionary<string, AccessIntegratedVMRoom>();
            }
        }

        [BindProperty(Name = "access")]
        public string AccessId { get; set; } = string.Empty;
    }
}