using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace _4.Data.ViewModels;

public class AccessControlViewModel : BaseViewModel
{
    // public string Id { get; set; } = null!;

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("ip_controller")]
    public string? IpController { get; set; }

    [JsonPropertyName("access_id")]
    public string? AccessId { get; set; }

    [JsonPropertyName("channel")]
    public int? Channel { get; set; }

    [JsonPropertyName("controller_list")]
    public string ControllerList { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("room_controller_falco")]
    public string RoomControllerFalco { get; set; } = string.Empty;

    [JsonPropertyName("delay")]
    public int? Delay { get; set; }

    [JsonPropertyName("model_controller")]
    public string? ModelController { get; set; }

    // [JsonPropertyName("created_by")]
    // public string? CreatedBy { get; set; }

    // [JsonPropertyName("updated_by")]
    // public string? UpdatedBy { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    // public int? IsDeleted { get; set; }

    [JsonPropertyName("falco_unit_no")]
    public string? FalcoUnitNo { get; set; }
    
    [JsonPropertyName("falco_group_access")]
    public string? FalcoGroupAccess { get; set; }


    [JsonPropertyName("room")]
    public int? Room { get; set; }
}

public class AccessControlVMCreateFR {
    [BindProperty(Name = "name")]
    public string? Name { get; set; }

    [BindProperty(Name = "type")]
    public string Type { get; set; } = string.Empty;

    [BindProperty(Name = "model_controller")]
    public string? ModelController { get; set; }

    [BindProperty(Name = "ip_controller")]
    public string? IpController { get; set; }

    [BindProperty(Name = "access_id")]
    public string? AccessId { get; set; }

    [BindProperty(Name = "channel")]
    public int? Channel { get; set; }


    [BindProperty(Name = "falco_group_access")]
    public string? GroupAccess { get; set; }

    [BindProperty(Name = "falco_unit_no")]
    public string? UnitNo { get; set; }


    [BindProperty(Name = "room")]
    public string[] Room { get; set; } = default!;
}

public class AccessControlVMUpdateFR : AccessControlVMCreateFR
{
    [BindProperty(Name = "id")]
    public string Id { get; set; } = string.Empty;
}

public class AccessControlVMDeleteFR
{
    [BindProperty(Name = "id")]
    public string Id { get; set; } = string.Empty;

    [BindProperty(Name = "name")]
    public string Name { get; set; } = string.Empty;
}

// TODO: Jika menu meeting room - room management sudah di buat, merge vm ini
public class AccessControlVMRoom : BaseLongViewModel
{
    [JsonPropertyName("radid")]
    public string RadId { get; set; } = string.Empty;

    [JsonPropertyName("building_id")]
    public long? BuildingId { get; set; }

    [JsonPropertyName("floor_id")]
    public long? FloorId { get; set; }

    [JsonPropertyName("type_room")]
    public string? TypeRoom { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("capacity")]
    public int Capacity { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("google_map")]
    public string? GoogleMap { get; set; }

    [JsonPropertyName("is_automation")]
    public short IsAutomation { get; set; }

    [JsonPropertyName("automation_id")]
    public int AutomationId { get; set; }

    [JsonPropertyName("facility_room")]
    public List<string> FacilityRoom { get; set; } = new List<string>();

    [JsonPropertyName("work_day")]
    public List<string>? WorkDay { get; set; }

    [JsonPropertyName("work_time")]
    public string WorkTime { get; set; } = string.Empty;

    [JsonPropertyName("work_start")]
    public string? WorkStart { get; set; }

    [JsonPropertyName("work_end")]
    public string? WorkEnd { get; set; }

    [JsonPropertyName("image")]
    public string? Image { get; set; }

    public string? Image2 { get; set; }

    [JsonPropertyName("multiple_image")]
    public string MultipleImage { get; set; } = string.Empty;

    [JsonPropertyName("price")]
    public long Price { get; set; }

    [JsonPropertyName("location")]
    public string? Location { get; set; }

    [JsonPropertyName("is_disabled")]
    public short? IsDisabled { get; set; }

    [JsonPropertyName("is_beacon")]
    public short? IsBeacon { get; set; }

    [JsonPropertyName("created_by")]
    public int? CreatedBy { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [JsonPropertyName("is_config_setting_enable")]
    public int? IsConfigSettingEnable { get; set; }

    [JsonPropertyName("config_room_for_usage")]
    public List<string>? ConfigRoomForUsage { get; set; }

    [JsonPropertyName("is_enable_approval")]
    public int? IsEnableApproval { get; set; }

    [JsonPropertyName("config_approval_user")]
    public List<string>? ConfigApprovalUser { get; set; }

    [JsonPropertyName("is_enable_permission")]
    public int? IsEnablePermission { get; set; }

    [JsonPropertyName("config_permission_user")]
    public List<string>? ConfigPermissionUser { get; set; }

    [JsonPropertyName("config_permission_checkin")]
    public string? ConfigPermissionCheckin { get; set; }

    [JsonPropertyName("config_permission_end")]
    public string? ConfigPermissionEnd { get; set; }

    [JsonPropertyName("config_min_duration")]
    public int? ConfigMinDuration { get; set; }

    [JsonPropertyName("config_max_duration")]
    public int? ConfigMaxDuration { get; set; }

    [JsonPropertyName("config_advance_booking")]
    public int? ConfigAdvanceBooking { get; set; }

    [JsonPropertyName("is_enable_recurring")]
    public int? IsEnableRecurring { get; set; }

    [JsonPropertyName("is_enable_checkin")]
    public int? IsEnableCheckin { get; set; }

    [JsonPropertyName("config_advance_checkin")]
    public int? ConfigAdvanceCheckin { get; set; }

    [JsonPropertyName("is_realease_checkin_timeout")]
    public int? IsRealeaseCheckinTimeout { get; set; }

    [JsonPropertyName("config_release_room_checkin_timeout")]
    public int? ConfigReleaseRoomCheckinTimeout { get; set; }

    [JsonPropertyName("config_participant_checkin_count")]
    public int ConfigParticipantCheckinCount { get; set; }

    [JsonPropertyName("is_enable_checkin_count")]
    public int? IsEnableCheckinCount { get; set; }

    [JsonPropertyName("config_google")]
    public string? ConfigGoogle { get; set; }

    [JsonPropertyName("config_microsoft")]
    public string? ConfigMicrosoft { get; set; }




    [JsonPropertyName("ra_name")]
    public string RaName { get; set; } = string.Empty;

    [JsonPropertyName("ra_id")]
    public long RaId { get; set; }

    [JsonPropertyName("building_name")]
    public string BuildingName { get; set; } = string.Empty;
    
    [JsonPropertyName("building_detail")]
    public string BuildingDetail { get; set; } = string.Empty;
    
    [JsonPropertyName("building_google_map")]
    public string BuildingGoogleMap { get; set; } = string.Empty;

    [JsonPropertyName("room_display_background")]
    public string RoomDisplayBackground { get; set; } = string.Empty;
}
