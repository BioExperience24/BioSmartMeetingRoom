using _4.Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


public class RoomViewModel : BaseLongViewModel
{
    [JsonPropertyName("radid")]
    public string Radid { get; set; } = string.Empty;

    [JsonPropertyName("building_id")]
    public long? BuildingId { get; set; }

    [JsonPropertyName("floor_id")]
    public long? FloorId { get; set; }

    [JsonPropertyName("type_room")]
    public string? TypeRoom { get; set; }

    [JsonPropertyName("kind_room")]
    public string? KindRoom { get; set; }

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
    public string? CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public string? UpdatedAt { get; set; }

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

public class RoomVMChartTopRoom
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("january")]
    public int January { get; set; } 

    [JsonPropertyName("february")]
    public int February { get; set; } 

    [JsonPropertyName("maret")]
    public int Maret { get; set; } 

    [JsonPropertyName("april")]
    public int April { get; set; } 

    [JsonPropertyName("may")]
    public int May { get; set; } 

    [JsonPropertyName("june")]
    public int June { get; set; } 

    [JsonPropertyName("july")]
    public int July { get; set; } 

    [JsonPropertyName("august")]
    public int August { get; set; } 

    [JsonPropertyName("september")]
    public int September { get; set; } 

    [JsonPropertyName("october")]
    public int October { get; set; } 

    [JsonPropertyName("november")]
    public int November { get; set; } 

    [JsonPropertyName("december")]
    public int December { get; set; } 
}

public class ModuleDetailsViewModel
{
    [JsonPropertyName("module_id")]
    public string ModuleId { get; set; }

    [JsonPropertyName("module_text")]
    public string ModuleText { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("module_serial")]
    public string ModuleSerial { get; set; }

    [JsonPropertyName("is_enabled")]
    public int IsEnabled { get; set; }
}

public class RoomModulesViewModel
{
    [JsonPropertyName("automation")]
    public ModuleDetailsViewModel Automation { get; set; }

    [JsonPropertyName("vip")]
    public ModuleDetailsViewModel Vip { get; set; }

    [JsonPropertyName("price")]
    public ModuleDetailsViewModel Price { get; set; }

    [JsonPropertyName("int_365")]
    public ModuleDetailsViewModel Int365 { get; set; }

    [JsonPropertyName("int_google")]
    public ModuleDetailsViewModel IntGoogle { get; set; }

    [JsonPropertyName("room_adv")]
    public ModuleDetailsViewModel RoomAdv { get; set; }
}

public class RoomDetailsViewModel
{
    [JsonPropertyName("page_name")]
    public string Pagename { get; set; }

    [JsonPropertyName("menu")]
    public object Menu { get; set; }

    [JsonPropertyName("building")]
    public object Building { get; set; }

    [JsonPropertyName("floor")]
    public object Floor { get; set; }

    [JsonPropertyName("room_for_usage")]
    public object RoomForUsage { get; set; }

    [JsonPropertyName("user_approval")]
    public object UserApproval { get; set; }

    [JsonPropertyName("user_permission")]
    public object UserPermission { get; set; }

    [JsonPropertyName("room_user_checking")]
    public object RoomUserCheckin { get; set; }

    [JsonPropertyName("modules")]
    public RoomModulesViewModel Modules { get; set; }
}

public class FacilityRoom2
{
    [JsonPropertyName("id")]
    public long Id { get; set; }
    [JsonPropertyName("room_id")]
    public long RoomId { get; set; }
    [JsonPropertyName("facility_id")]
    public long FacilityId { get; set; }
    [JsonPropertyName("datetime")]
    public DateTime? Datetime { get; set; }
}

[NotMapped]
public class RoomDataViewModel : RoomViewModel
{
    [JsonPropertyName("room_id")]
    public long RoomId { get; set; }
    [JsonPropertyName("room_name")]
    public string RoomName { get; set; }
    [JsonPropertyName("rad_id")]
    public string RadId { get; set; }
    [JsonPropertyName("ra_id")]
    public int RaId { get; set; }
    [JsonPropertyName("ra_name")]
    public string RaName { get; set; }
    [JsonPropertyName("automation_name")]
    public string AutomationName { get; set; }
    [JsonPropertyName("building_name")]
    public string BuildingName { get; set; }
    [JsonPropertyName("building_detail")]
    public string BuildingDetail { get; set; }
    [JsonPropertyName("building_google_map")]
    public string BuildingGoogleMap { get; set; }
    [JsonPropertyName("facility_room2")]
    public List<FacilityRoom2> facility_room2 { get; set; }
}

public class RoomBuildingViewModel
{
    [JsonPropertyName("id")]
    public long? Id { get; set; }

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

    [JsonPropertyName("is_deleted")]
    public int? IsDeleted { get; set; }

    [JsonPropertyName("created_by")]
    public string? CreatedBy { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [JsonPropertyName("updated_by")]
    public DateTime? UpdatedBy { get; set; }

    [JsonPropertyName("count_room")]
    public int? CountRoom { get; set; }

    [JsonPropertyName("count_floor")]
    public int? CountFloor { get; set; }

    [JsonPropertyName("count_desk")]
    public int? CountDesk { get; set; }
    // Add other properties as needed, following the same pattern
}


public class RoomVMDefaultFR
{
    [BindProperty(Name = "name")]
    public string? Name { get; set; }

    [BindProperty(Name = "description")]
    public string? Description { get; set; }

    [BindProperty(Name = "building_id")]
    public long? BuildingId { get; set; }

    [BindProperty(Name = "floor_id")]
    public long? FloorId { get; set; }

    [BindProperty(Name = "type_room")]
    public string? TypeRoom { get; set; }

    [BindProperty(Name = "kind_room")]
    public string? KindRoom { get; set; }

    [BindProperty(Name = "merge_room")]
    public List<string>? MergeRoom { get; set; }

    [BindProperty(Name = "capacity")]
    public int? Capacity { get; set; }

    [BindProperty(Name = "work_day")]
    public List<string>? WorkDay { get; set; }

    [BindProperty(Name = "work_start")]
    public string? WorkStart { get; set; }

    [BindProperty(Name = "work_end")]
    public string? WorkEnd { get; set; }

    [BindProperty(Name = "facility_room")]
    public List<long>? FacilityRoom { get; set; }

    [BindProperty(Name = "is_disabled")]
    public int? IsDisabled { get; set; }

    [BindProperty(Name = "location")]
    public string? Location { get; set; }

    [BindProperty(Name = "google_map")]
    public string? GoogleMap { get; set; }

    [BindProperty(Name = "image")]
    public IFormFile? Image { get; set; }

    [BindProperty(Name = "image2[]")]
    public List<IFormFile>? Image2 { get; set; }

    [BindProperty(Name = "facility_room_name")]
    public List<string>? FacilityRoomName { get; set; }

    [BindProperty(Name = "is_automation")]
    public short IsAutomation { get; set; }

    [BindProperty(Name = "automation_id")]
    public int AutomationId { get; set; }
    [BindProperty(Name = "is_config_setting_enable")]
    public string? IsConfigSettingEnableRaw { get; set; }
    public int? IsConfigSettingEnable => IsConfigSettingEnableRaw == "on" ? 1 : (int?)null;

    [BindProperty(Name = "config_room_for_usage")]
    public List<string>? ConfigRoomForUsage { get; set; }

    [BindProperty(Name = "room_usage_detail")]
    public List<RoomForUsageDetailViewModel>? RoomUsageDetail { get; set; }

    [BindProperty(Name = "config_min_duration")]
    public int? ConfigMinDuration { get; set; }

    [BindProperty(Name = "config_max_duration")]
    public int? ConfigMaxDuration { get; set; }

    [BindProperty(Name = "config_advance_booking")]
    public int? ConfigAdvanceBooking { get; set; }


    [BindProperty(Name = "config_approval_user")]
    public List<string>? ConfigApprovalUser { get; set; }

    [BindProperty(Name = "config_permission_user")]
    public List<string>? ConfigPermissionUser { get; set; }

    [BindProperty(Name = "config_permission_checkin")]
    public string? ConfigPermissionCheckin { get; set; }

    [BindProperty(Name = "config_permission_end")]
    public string? ConfigPermissionEnd { get; set; }

    [BindProperty(Name = "config_release_room_checkin_timeout")]
    public int? ConfigReleaseRoomCheckinTimeout { get; set; }

    [BindProperty(Name = "facility_room_names")]
    public List<string>? FacilityRoomNames { get; set; }

    [BindProperty(Name = "is_realease_checkin_timeout")]
    public string? IsRealeaseCheckinTimeoutRaw { get; set; }
    public int? IsRealeaseCheckinTimeout => IsRealeaseCheckinTimeoutRaw == "on" ? 1 : (int?)null;

    [BindProperty(Name = "is_enable_recurring")]
    public string? IsEnableRecurringRaw { get; set; }

    public int? IsEnableRecurring => IsEnableRecurringRaw == "on" ? 1 : (int?)null;


    [BindProperty(Name = "is_enable_approval")]
    public string? IsEnableApprovalRaw { get; set; }

    public int? IsEnableApproval => IsEnableApprovalRaw == "on" ? 1 : (int?)null;


    [BindProperty(Name = "is_enable_permission")]
    public string? IsEnablePermissionRaw { get; set; }

    public int? IsEnablePermission => IsEnablePermissionRaw == "on" ? 1 : (int?)null;
    [BindProperty(Name = "is_enable_checkin")]
    public string? IsEnableCheckinRaw { get; set; }
    public int? IsEnableCheckin => IsEnableCheckinRaw == "on" ? 1 : (int?)null;

    [BindProperty(Name = "is_enable_checkin_count")]
    public string? IsEnableCheckinCountRaw { get; set; }
    public int? IsEnableCheckinCount => IsEnableCheckinCountRaw == "on" ? 1 : (int?)null;

    [JsonPropertyName("work_time")]
    public string WorkTime { get; set; } = string.Empty;
}

public class RoomVMUResponseFRViewModel
{
    [JsonPropertyName("id")]
    public long Id { get; set; }
    [JsonPropertyName("radid")]
    public string Radid { get; set; }
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("building_id")]
    public long? BuildingId { get; set; }

    [JsonPropertyName("floor_id")]
    public long? FloorId { get; set; }

    [JsonPropertyName("type_room")]
    public string? TypeRoom { get; set; }

    [JsonPropertyName("kind_room")]
    public string? KindRoom { get; set; }

    [JsonPropertyName("capacity")]
    public int? Capacity { get; set; }

    [JsonPropertyName("work_day")]
    public List<string>? WorkDay { get; set; }

    [JsonPropertyName("work_start")]
    public string? WorkStart { get; set; }

    [JsonPropertyName("work_end")]
    public string? WorkEnd { get; set; }

    [JsonPropertyName("facility_room")]
    public List<string>? FacilityRoom { get; set; }

    [JsonPropertyName("is_disabled")]
    public int? IsDisabled { get; set; }

    [JsonPropertyName("location")]
    public string? Location { get; set; }

    [JsonPropertyName("google_map")]
    public string? GoogleMap { get; set; }

    [JsonPropertyName("image")]
    public string? Image { get; set; }

    [JsonPropertyName("image2")]
    public string? Image2 { get; set; }

    [JsonPropertyName("facility_room_name")]
    public List<string>? FacilityRoomName { get; set; }

    [JsonPropertyName("is_automation")]
    public short IsAutomation { get; set; }

    [JsonPropertyName("automation_id")]
    public int AutomationId { get; set; }

    [JsonPropertyName("is_config_setting_enable")]
    public int? IsConfigSettingEnable { get; set; }

    [JsonPropertyName("config_room_for_usage")]
    public List<string>? ConfigRoomForUsage { get; set; }

    [JsonPropertyName("config_min_duration")]
    public int? ConfigMinDuration { get; set; }

    [JsonPropertyName("config_max_duration")]
    public int? ConfigMaxDuration { get; set; }

    [JsonPropertyName("config_advance_booking")]
    public int? ConfigAdvanceBooking { get; set; }

    [JsonPropertyName("config_approval_user")]
    public List<string>? ConfigApprovalUser { get; set; }

    [JsonPropertyName("config_permission_user")]
    public List<string>? ConfigPermissionUser { get; set; }

    [JsonPropertyName("config_permission_checkin")]
    public string? ConfigPermissionCheckin { get; set; }

    [JsonPropertyName("config_permission_end")]
    public string? ConfigPermissionEnd { get; set; }

    [JsonPropertyName("config_release_room_checkin_timeout")]
    public int? ConfigReleaseRoomCheckinTimeout { get; set; }

    [JsonPropertyName("facility_room_names")]
    public List<string>? FacilityRoomNames { get; set; }

    [JsonPropertyName("is_realease_checkin_timeout")]
    public int? IsRealeaseCheckinTimeout { get; set; }

    [JsonPropertyName("is_enable_recurring")]
    public string? IsEnableRecurring { get; set; }

    [JsonPropertyName("is_enable_approval")]
    public int? IsEnableApproval { get; set; }
    [JsonPropertyName("is_enable_permission")]
    public int? IsEnablePermission { get; set; }

    [JsonPropertyName("is_enable_checkin")]
    public int? IsEnableCheckin { get; set; }

    [JsonPropertyName("is_enable_checkin_count")]
    public int? IsEnableCheckinCount { get; set; }

    [JsonPropertyName("facility_room2")]
    public List<RoomDetailViewModel> FacilityRoom2 { get; set; }


    [JsonPropertyName("work_time")]
    public string WorkTime { get; set; } = string.Empty;
    // Uncomment if required
    // [JsonPropertyName("config_room_for_usage")]
    // public List<RoomForUsageDetailViewModel?> ConfigRoomForUsage { get; set; } = new List<RoomForUsageDetailViewModel?>();
}

public class RoomVMUpdateFRViewModel
{
    [FromForm(Name = "radid")]
    public string? Radid { get; set; }

    [FromForm(Name = "name")]
    public string? Name { get; set; }

    [FromForm(Name = "description")]
    public string? Description { get; set; }

    [FromForm(Name = "building_id")]
    public long? BuildingId { get; set; }

    [FromForm(Name = "floor_id")]
    public long? FloorId { get; set; }

    [FromForm(Name = "type_room")]
    public string? TypeRoom { get; set; }

    [FromForm(Name = "kind_room")]
    public string? KindRoom { get; set; }

    [FromForm(Name = "capacity")]
    public int? Capacity { get; set; }

    [FromForm(Name = "work_day")]
    public List<string>? WorkDay { get; set; }

    [FromForm(Name = "work_start")]
    public string? WorkStart { get; set; }

    [FromForm(Name = "work_end")]
    public string? WorkEnd { get; set; }

    [FromForm(Name = "facility_room")]
    public List<long>? FacilityRoom { get; set; }

    [FromForm(Name = "is_disabled")]
    public int? IsDisabled { get; set; }

    [FromForm(Name = "location")]
    public string? Location { get; set; }

    [FromForm(Name = "google_map")]
    public string? GoogleMap { get; set; }

    [FromForm(Name = "image")]
    public IFormFile? Image { get; set; }

    [FromForm(Name = "image2_1")]
    public IFormFile? Image2_1 { get; set; }

    [FromForm(Name = "image2_2")]
    public IFormFile? Image2_2 { get; set; }

    [FromForm(Name = "image2_3")]
    public IFormFile? Image2_3 { get; set; }

    [FromForm(Name = "facility_room_name")]
    public List<string>? FacilityRoomName { get; set; }

    [FromForm(Name = "is_automation")]
    public short IsAutomation { get; set; }

    [FromForm(Name = "config_room_for_usage")]
    public List<string>? ConfigRoomForUsage { get; set; }

    [FromForm(Name = "room_usage_detail")]
    public string? RoomUsageDetail { get; set; }

    [FromForm(Name = "automation_id")]
    public int AutomationId { get; set; }

    [FromForm(Name = "is_config_setting_enable")]
    public string? IsConfigSettingEnableRaw { get; set; }

    public int? IsConfigSettingEnable => IsConfigSettingEnableRaw == "on" ? 1 : (int?)null;

    [FromForm(Name = "config_min_duration")]
    public int? ConfigMinDuration { get; set; }

    [FromForm(Name = "config_max_duration")]
    public int? ConfigMaxDuration { get; set; }

    [FromForm(Name = "config_advance_booking")]
    public int? ConfigAdvanceBooking { get; set; }

    [FromForm(Name = "config_approval_user")]
    public List<string>? ConfigApprovalUser { get; set; }

    [FromForm(Name = "config_permission_user")]
    public List<string>? ConfigPermissionUser { get; set; }

    [FromForm(Name = "config_permission_checkin")]
    public string? ConfigPermissionCheckin { get; set; }

    [FromForm(Name = "config_permission_end")]
    public string? ConfigPermissionEnd { get; set; }

    [FromForm(Name = "config_release_room_checkin_timeout")]
    public int? ConfigReleaseRoomCheckinTimeout { get; set; }

    [FromForm(Name = "facility_room_names")]
    public List<string>? FacilityRoomNames { get; set; }

    [FromForm(Name = "is_realease_checkin_timeout")]
    public string? IsRealeaseCheckinTimeoutRaw { get; set; }

    public int? IsRealeaseCheckinTimeout => IsRealeaseCheckinTimeoutRaw == "on" ? 1 : (int?)null;

    [FromForm(Name = "is_enable_recurring")]
    public string? IsEnableRecurringRaw { get; set; }

    public int? IsEnableRecurring => IsEnableRecurringRaw == "on" ? 1 : (int?)null;

    [FromForm(Name = "is_enable_approval")]
    public string? IsEnableApprovalRaw { get; set; }

    public int? IsEnableApproval => IsEnableApprovalRaw == "on" ? 1 : (int?)null;

    [FromForm(Name = "is_enable_permission")]
    public string? IsEnablePermissionRaw { get; set; }

    public int? IsEnablePermission => IsEnablePermissionRaw == "on" ? 1 : (int?)null;

    [FromForm(Name = "is_enable_checkin")]
    public string? IsEnableCheckinRaw { get; set; }

    public int? IsEnableCheckin => IsEnableCheckinRaw == "on" ? 1 : (int?)null;

    [FromForm(Name = "is_enable_checkin_count")]
    public string? IsEnableCheckinCountRaw { get; set; }

    public int? IsEnableCheckinCount => IsEnableCheckinCountRaw == "on" ? 1 : (int?)null;

    [FromForm(Name = "facility_room2")]
    public List<RoomDetailViewModel>? FacilityRoom2 { get; set; }

    [FromForm(Name = "merge_room")]
    public List<string>? MergeRoom { get; set; }
}