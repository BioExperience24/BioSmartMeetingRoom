using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _4.Data.ViewModels;

public class BookingViewModel : BaseLongViewModel
{
    [JsonPropertyName("booking_id")]
    public string BookingId { get; set; } = string.Empty;

    [JsonPropertyName("booking_id_365")]
    public string? BookingId365 { get; set; }

    [JsonPropertyName("booking_id_google")]
    public string? BookingIdGoogle { get; set; }

    [JsonPropertyName("booking_devices")]
    public string? BookingDevices { get; set; }

    [JsonPropertyName("no_order")]
    public string? NoOrder { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    [JsonPropertyName("room_id")]
    public string RoomId { get; set; } = string.Empty;

    [JsonPropertyName("room_name")]
    public string? RoomName { get; set; }

    [JsonPropertyName("is_merge")]
    public short? IsMerge { get; set; }

    [JsonPropertyName("merge_room")]
    public string? MergeRoom { get; set; }

    [JsonPropertyName("merge_room_id")]
    public string? MergeRoomId { get; set; }

    [JsonPropertyName("merge_room_name")]
    public string? MergeRoomName { get; set; }

    [JsonPropertyName("start")]
    public DateTime Start { get; set; }

    [JsonPropertyName("end")]
    public DateTime End { get; set; }

    [JsonPropertyName("cost_total_booking")]
    public long? CostTotalBooking { get; set; }

    [JsonPropertyName("duration_per_meeting")]
    public int? DurationPerMeeting { get; set; }

    [JsonPropertyName("total_duration")]
    public int TotalDuration { get; set; }

    [JsonPropertyName("extended_duration")]
    public int ExtendedDuration { get; set; }

    [JsonPropertyName("pic")]
    public string Pic { get; set; } = string.Empty;

    [JsonPropertyName("alocation_id")]
    public string? AlocationId { get; set; }

    [JsonPropertyName("alocation_name")]
    public string? AlocationName { get; set; }

    [JsonPropertyName("note")]
    public string Note { get; set; } = string.Empty;

    [JsonPropertyName("canceled_note")]
    public string? CanceledNote { get; set; }

    [JsonPropertyName("participants")]
    public string Participants { get; set; } = string.Empty;

    [JsonPropertyName("external_link")]
    public string? ExternalLink { get; set; }

    [JsonPropertyName("external_link_365")]
    public string? ExternalLink365 { get; set; }

    [JsonPropertyName("external_link_google")]
    public string? ExternalLinkGoogle { get; set; }

    [JsonPropertyName("end_early_meeting")]
    public int EndEarlyMeeting { get; set; }

    [JsonPropertyName("text_early")]
    public string? TextEarly { get; set; }

    [JsonPropertyName("is_device")]
    public int? IsDevice { get; set; }

    [JsonPropertyName("is_meal")]
    public short IsMeal { get; set; }

    [JsonPropertyName("is_ear")]
    public int IsEar { get; set; }

    [JsonPropertyName("is_rescheduled")]
    public int IsRescheduled { get; set; }

    [JsonPropertyName("is_canceled")]
    public int IsCanceled { get; set; }

    [JsonPropertyName("is_expired")]
    public int IsExpired { get; set; }

    [JsonPropertyName("canceled_by")]
    public string CanceledBy { get; set; } = string.Empty;

    [JsonPropertyName("canceled_at")]
    public DateTime CanceledAt { get; set; }

    [JsonPropertyName("expired_by")]
    public string ExpiredBy { get; set; } = string.Empty;

    [JsonPropertyName("expired_at")]
    public DateTime ExpiredAt { get; set; }

    [JsonPropertyName("rescheduled_by")]
    public string RescheduledBy { get; set; } = string.Empty;

    [JsonPropertyName("rescheduled_at")]
    public DateTime RescheduledAt { get; set; }

    [JsonPropertyName("early_ended_by")]
    public string EarlyEndedBy { get; set; } = string.Empty;

    [JsonPropertyName("early_ended_at")]
    public DateTime EarlyEndedAt { get; set; }

    [JsonPropertyName("is_alive")]
    public int IsAlive { get; set; }

    [JsonPropertyName("timezone")]
    public string? Timezone { get; set; }

    [JsonPropertyName("comment")]
    public string? Comment { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; set; }

    [JsonPropertyName("created_by")]
    public string? CreatedBy { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [JsonPropertyName("updated_by")]
    public string? UpdatedBy { get; set; }

    [JsonPropertyName("is_notif_end_meeting")]
    public int IsNotifEndMeeting { get; set; }

    [JsonPropertyName("is_notif_before_end_meeting")]
    public int IsNotifBeforeEndMeeting { get; set; }

    [JsonPropertyName("is_access_trigger")]
    public int? IsAccessTrigger { get; set; }

    [JsonPropertyName("is_config_setting_enable")]
    public int? IsConfigSettingEnable { get; set; }

    [JsonPropertyName("is_enable_approval")]
    public int? IsEnableApproval { get; set; }

    [JsonPropertyName("is_enable_permission")]
    public int? IsEnablePermission { get; set; }

    [JsonPropertyName("is_enable_recurring")]
    public int? IsEnableRecurring { get; set; }

    [JsonPropertyName("is_enable_checkin")]
    public int? IsEnableCheckin { get; set; }

    [JsonPropertyName("is_realease_checkin_timeout")]
    public int? IsRealeaseCheckinTimeout { get; set; }

    [JsonPropertyName("is_released")]
    public int IsReleased { get; set; }

    [JsonPropertyName("is_enable_checkin_count")]
    public int IsEnableCheckinCount { get; set; }

    [JsonPropertyName("category")]
    public int? Category { get; set; }

    [JsonPropertyName("last_modified_date_time_365")]
    public string? LastModifiedDateTime365 { get; set; }

    [JsonPropertyName("permission_end")]
    public string? PermissionEnd { get; set; }

    [JsonPropertyName("permission_checkin")]
    public string? PermissionCheckin { get; set; }

    [JsonPropertyName("release_room_checkin_time")]
    public int? ReleaseRoomCheckinTime { get; set; }

    [JsonPropertyName("checkin_count")]
    public int? CheckinCount { get; set; }

    [JsonPropertyName("is_vip")]
    public int IsVip { get; set; }

    [JsonPropertyName("is_approve")]
    public int IsApprove { get; set; }

    [JsonPropertyName("vip_user")]
    public string? VipUser { get; set; }

    [JsonPropertyName("user_end_meeting")]
    public string? UserEndMeeting { get; set; }

    [JsonPropertyName("user_checkin")]
    public string? UserCheckin { get; set; }

    [JsonPropertyName("user_approval")]
    public string? UserApproval { get; set; }

    [JsonPropertyName("user_approval_datetime")]
    public DateTime? UserApprovalDatetime { get; set; }

    [JsonPropertyName("room_meeting_move")]
    public string? RoomMeetingMove { get; set; }

    [JsonPropertyName("room_meeting_old")]
    public string? RoomMeetingOld { get; set; }

    [JsonPropertyName("is_moved")]
    public int? IsMoved { get; set; }

    [JsonPropertyName("is_moved_agree")]
    public int? IsMovedAgree { get; set; }

    [JsonPropertyName("moved_duration")]
    public int? MovedDuration { get; set; }

    [JsonPropertyName("meeting_end_note")]
    public string? MeetingEndNote { get; set; }

    [JsonPropertyName("vip_approve_bypass")]
    public int? VipApproveBypass { get; set; }

    [JsonPropertyName("vip_limit_cap_bypass")]
    public int? VipLimitCapBypass { get; set; }

    [JsonPropertyName("vip_lock_room")]
    public int? VipLockRoom { get; set; }

    [JsonPropertyName("vip_force_moved")]
    public string? VipForceMoved { get; set; }

    [JsonPropertyName("duration_saved_release")]
    public int? DurationSavedRelease { get; set; }

    [JsonPropertyName("is_cleaning_need")]
    public int? IsCleaningNeed { get; set; }

    [JsonPropertyName("cleaning_time")]
    public int? CleaningTime { get; set; }

    [JsonPropertyName("cleaning_start")]
    public DateTime? CleaningStart { get; set; }

    [JsonPropertyName("cleaning_end")]
    public DateTime? CleaningEnd { get; set; }

    [JsonPropertyName("user_cleaning")]
    public string? UserCleaning { get; set; }

    [JsonPropertyName("server_date")]
    public DateTime? ServerDate { get; set; }

    [JsonPropertyName("server_start")]
    public DateTime? ServerStart { get; set; }

    [JsonPropertyName("server_end")]
    public DateTime? ServerEnd { get; set; }
}



public class BookingModuleDetailsViewModel
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


public class BookingModulesViewModel
{
    [JsonPropertyName("automation")]
    public BookingModuleDetailsViewModel Automation { get; set; }

    [JsonPropertyName("vip")]
    public BookingModuleDetailsViewModel Vip { get; set; }

    [JsonPropertyName("price")]
    public BookingModuleDetailsViewModel Price { get; set; }

    [JsonPropertyName("int_365")]
    public BookingModuleDetailsViewModel Int365 { get; set; }

    [JsonPropertyName("int_google")]
    public BookingModuleDetailsViewModel IntGoogle { get; set; }

    [JsonPropertyName("room_adv")]
    public BookingModuleDetailsViewModel RoomAdv { get; set; }
}


public class BookingMenuDetailViewModel
{
    [JsonPropertyName("pagename")]
    public string Pagename { get; set; }

    [JsonPropertyName("menu")]
    public object Menu { get; set; }

    [JsonPropertyName("building")]
    public object Building { get; set; }

    [JsonPropertyName("invoice")]
    public object Invoice { get; set; }

    [JsonPropertyName("category")]
    public object Category { get; set; }

    [JsonPropertyName("setting_general")]
    public object SettingGeneral { get; set; }

    [JsonPropertyName("room")]
    public object Room { get; set; }

    [JsonPropertyName("organizer")]
    public object Organizer { get; set; }

    [JsonPropertyName("modules")]
    public BookingModulesViewModel Modules { get; set; }

    [JsonPropertyName("facility")]
    public object Facility { get; set; }
}