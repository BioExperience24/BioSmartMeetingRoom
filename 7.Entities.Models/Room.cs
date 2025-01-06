using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace _7.Entities.Models;

[NotMapped]
public partial class Room : BaseLongEntity
{
    [JsonPropertyName("radid")]
    public string Radid { get; set; } = null!;

    [JsonPropertyName("building_id")]
    public long? BuildingId { get; set; }

    [JsonPropertyName("floor_id")]
    public long? FloorId { get; set; }

    [JsonPropertyName("type_room")]
    public string? TypeRoom { get; set; }

    [JsonPropertyName("kind_room")]
    public string? KindRoom { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("capacity")]
    public int Capacity { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; } = null!;

    [JsonPropertyName("google_map")]
    public string? GoogleMap { get; set; }

    [JsonPropertyName("is_automation")]
    public short IsAutomation { get; set; }

    [JsonPropertyName("automation_id")]
    public int AutomationId { get; set; }

    [JsonPropertyName("facility_room")]
    public List<string> FacilityRoom { get; set; } = null!;

    [JsonPropertyName("work_day")]
    public List<string>? WorkDay { get; set; }

    [JsonPropertyName("work_time")]
    public string WorkTime { get; set; } = null!;

    [JsonPropertyName("work_start")]
    public string? WorkStart { get; set; }

    [JsonPropertyName("work_end")]
    public string? WorkEnd { get; set; }

    [JsonPropertyName("image")]
    public string? Image { get; set; }

    public string? Image2 { get; set; }

    [JsonPropertyName("multiple_image")]
    public string MultipleImage { get; set; } = null!;

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
}
public class ResponseData
{
    public string Error { get; set; }
    public IEnumerable<object> Data { get; set; }
}

public class ResponseModel<T>
{
    public string Status { get; set; }
    public T Data { get; set; }
    public string Message { get; set; }
    public string Error { get; set; }
}

public class RoomData : Room
{
    public string RadId { get; set; }
    public long? RaId { get; set; }
    public string RaName { get; set; }
    public string AutomationName { get; set; }
    public string BuildingName { get; set; }
    public string BuildingDetail { get; set; }
    public string BuildingGoogleMap { get; set; }
    public List<RoomDetail> RoomDetail { get; set; }
}
[NotMapped]
public class SingleRoom : Room
{
    public bool IsSingle { get; set; }
}

public class RoomDetailDto : Room
{
    public long? Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Capacity { get; set; }
    public string? GoogleMap { get; set; }
    public string? RaName { get; set; }
    public long? RaId { get; set; }
    public string? BuildingName { get; set; }
    public string? BuildingDetail { get; set; }
    public string? BuildingGoogleMap { get; set; }
    public RoomAutomation RoomAutomation { get; set; }
    public Building Building { get; set; }
}