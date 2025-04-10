using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _4.Data.ViewModels
{
    public class RoomDisplayViewModel : BaseLongViewModel
    {
        [JsonPropertyName("room_id")]
        public string RoomId { get; set; } = string.Empty;

        [JsonPropertyName("display_serial")]
        public string DisplaySerial { get; set; } = string.Empty;

        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("background")]
        public string Background { get; set; } = string.Empty;

        [JsonPropertyName("background_update")]
        public int BackgroundUpdate { get; set; }

        [JsonPropertyName("color_occupied")]
        public string ColorOccupied { get; set; } = string.Empty;

        [JsonPropertyName("color_available")]
        public string ColorAvailable { get; set; } = string.Empty;

        [JsonPropertyName("enable_signage")]
        public int EnableSignage { get; set; }

        [JsonPropertyName("signage_type")]
        public string SignageType { get; set; } = string.Empty;

        [JsonPropertyName("signage_media")]
        public string SignageMedia { get; set; } = string.Empty;

        [JsonPropertyName("signage_update")]
        public int SignageUpdate { get; set; }

        /* [JsonPropertyName("created_by")]
        public string CreatedBy { get; set; } = string.Empty;

        [JsonPropertyName("updated_by")]
        public string UpdatedBy { get; set; } = string.Empty; */

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyName("status_sync")]
        public int StatusSync { get; set; }

        [JsonPropertyName("enabled")]
        public int Enabled { get; set; }

        [JsonPropertyName("hardware_uuid")]
        public string HardwareUuid { get; set; } = string.Empty;

        [JsonPropertyName("hardware_info")]
        public string HardwareInfo { get; set; } = string.Empty;

        [JsonPropertyName("hardware_last_sync")]
        public DateTime HardwareLastsync { get; set; }

        [JsonPropertyName("room_select")]
        public string RoomSelect { get; set; } = string.Empty;

        [JsonPropertyName("disable_msg")]
        public string DisableMsg { get; set; } = string.Empty;
        
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;


        [JsonPropertyName("room_name")]
        public string RoomName { get; set; } = string.Empty;

        [JsonPropertyName("room_select_data")]
        public IEnumerable<RoomDisplayInformationViewModel> RoomSelectData { get; set; } = new List<RoomDisplayInformationViewModel>();

        [JsonIgnore]
        public long BuildingId { get; set; }

        [JsonIgnore]
        public long FloorId { get; set; }

        [JsonPropertyName("building_id")]
        public string BuildingIdEnc { get; set; } = string.Empty;

        [JsonPropertyName("floor_id")]
        public string FloorIdEnc { get; set; } = string.Empty;

        [JsonPropertyName("building_name")]
        public string BuildingName { get; set; } = string.Empty;
        
        [JsonPropertyName("floor_name")]
        public string FloorName { get; set; } = string.Empty;

        // [JsonPropertyName("display_information")]
        // public List<RoomDisplayVMRoomSelected> DisplayInformation { get; set; } = new List<RoomDisplayVMRoomSelected>();
    }

    public class RoomDisplayVMCreateFR
    {
        [BindProperty(Name = "id")]
        public long? Id { get; set; } = null;
        
        [BindProperty(Name = "display_serial")]
        public string DisplaySerial { get; set; } = string.Empty;
        
        [BindProperty(Name = "name")]
        public string Name { get; set; } = string.Empty;

        [BindProperty(Name = "description")]
        public string Description { get; set; } = string.Empty;

        [BindProperty(Name = "type")]
        public string Type { get; set; } = string.Empty;

        [BindProperty(Name = "room_id")]
        public string RoomId { get; set; } = string.Empty;

        [BindProperty(Name = "background")]
        public IFormFile? FileBackground { get; set; }

        [BindProperty(Name = "color_occupied")]
        public string ColorOccupied { get; set; } = string.Empty;

        [BindProperty(Name = "color_available")]
        public string ColorAvailable { get; set; } = string.Empty;

        [BindProperty(Name = "room_select[]")]
        public string[] RoomSelectArr { get; set; } = new string[]{};

        [BindProperty(Name = "building")]
        public string Building { get; set; } = string.Empty;

        [BindProperty(Name = "floor")]
        public string Floor { get; set; } = string.Empty;
        
        public List<RoomDisplayInformationViewModel> RoomSelected { get; set; } = new List<RoomDisplayInformationViewModel>();

        [BindProperty(Name = "roomSelected")]
        // public string RoomSelected { get; set; } = string.Empty;
        public string RoomSelectedJson
        {
            get => JsonSerializer.Serialize(RoomSelected);
            set => RoomSelected = JsonSerializer.Deserialize<List<RoomDisplayInformationViewModel>>(value) ?? new List<RoomDisplayInformationViewModel>();
        }
    }

    public class RoomDisplayVMUpdateFR : RoomDisplayVMCreateFR
    {
        // [BindProperty(Name = "id")]
        // public long Id { get; set; }
    }
    
    public class RoomDisplayVMChangeStatusFR
    {
        [BindProperty(Name = "id")]
        public long Id { get; set; }

        [BindProperty(Name = "action")]
        public int Action { get; set; }

        [BindProperty(Name = "disable_msg")]
        public string DisableMsg { get; set; } = string.Empty;
    }

    public class RoomDisplayVMDeleteFR
    {
        [BindProperty(Name = "id")]
        public long Id { get; set; }
    }

    public class RoomDisplayBySerialViewModel : RoomDisplayViewModel
    {
        [JsonPropertyName("room_select_data")]
        public new List<RoomViewModel> RoomSelectData { get; set; } = new();
    }
    public class DisplayUpdateSerialSyncFRViewModel
    {
        [JsonPropertyName("serial")]
        public string? Serial { get; set; }

        [JsonPropertyName("hardware_uuid")]
        public string? HardwareUuid { get; set; }

        [JsonPropertyName("hardware_info")]
        public string? HardWareInfo { get; set; }
    }
}