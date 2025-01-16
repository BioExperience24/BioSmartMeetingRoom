using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace _4.Data.ViewModels
{
    public partial class SettingRuleBookingViewModel : BaseViewModelId
    {
        [JsonPropertyName("duration")]
        public int? Duration { get; set; }

        [JsonPropertyName("if_unuse_room")]
        public int IfUnusedRoom { get; set; }

        [JsonPropertyName("max_end_meeting")]
        public int? MaxEndMeeting { get; set; }
        
        [JsonPropertyName("notif_unused_meeting")]
        public int? NotifUnusedMeeting { get; set; }
        
        [JsonPropertyName("notif_unuse_before_meeting")]
        public int NotifUnuseBeforeMeeting { get; set; }
        
        [JsonPropertyName("unuse_cancel_fee")]
        public int UnuseCancelFee { get; set; }
        
        [JsonPropertyName("max_display_duration")]
        public int? MaxDisplayDuration { get; set; }
        
        [JsonPropertyName("room_pin")]
        public short? RoomPin { get; set; }
        
        [JsonPropertyName("room_number")]
        public string? RoomPinNumber { get; set; }
        
        [JsonPropertyName("room_pin_refresh")]
        public TimeOnly? RoomPinRefresh { get; set; }
        
        [JsonPropertyName("extend_meeting")]
        public int? ExtendMeeting { get; set; }
        
        [JsonPropertyName("extend_meeting_max")]
        public int? ExtendMeetingMax { get; set; }
        
        [JsonPropertyName("extend_count_time")]
        public int? ExtendCountTime { get; set; }
        
        [JsonPropertyName("extend_meeting_notification")]
        public int? ExtendMeetingNotification { get; set; }
        
        [JsonPropertyName("end_early_meeting")]
        public int? EndEarlyMeeting { get; set; }
        
        [JsonPropertyName("limit_time_booking")]
        public int? LimitTimeBooking { get; set; }
        
        [JsonPropertyName("created_by")]
        public string? CreatedBy { get; set; }
        
        [JsonPropertyName("updated_by")]
        public string? UpdatedBy { get; set; }
        
        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAt { get; set; }
        
        [JsonPropertyName("is_config_setting_enable")]
        public int? IsConfigSettingEnable { get; set; }
        
        [JsonPropertyName("config_room_for_usage")]
        public string? ConfigRoomForUsage { get; set; }
        
        [JsonPropertyName("is_enable_approval")]
        public int? IsEnableApproval { get; set; }
        
        [JsonPropertyName("config_approval_user")]
        public string? ConfigApprovalUser { get; set; }
        
        [JsonPropertyName("is_enable_permission")]
        public int? IsEnablePermission { get; set; }
        
        [JsonPropertyName("config_permission_user")]
        public string? ConfigPermissionUser { get; set; }
        
        [JsonPropertyName("config_permission_checkin")]
        public string? ConfigPermissionCheckin { get; set; }
        
        [JsonPropertyName("config_permission_end")]
        public string? ConfigPermissionEnd { get; set; }
    }

    public partial class SettingRuleBookingViewModel : BaseViewModelId
    {
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
        public int? ConfigParticipantCheckinCount { get; set; }
        
        [JsonPropertyName("is_enable_checkin_count")]
        public int? IsEnableCheckinCount { get; set; }
    }

    public class SettingRuleBookingVMResponse
    {
        [JsonPropertyName("error")]
        public string? Error { get; set; }

        [JsonPropertyName("data")]
        public List<SettingRuleBookingVMProp>? Data { get; set; }
    }

    public class SettingRuleBookingVMProp
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("duration")]
        public int? Duration { get; set; }

        [JsonPropertyName("if_unused_room")]
        public int IfUnusedRoom { get; set; }

        [JsonPropertyName("max_end_meeting")]
        public int? MaxEndMeeting { get; set; }

        [JsonPropertyName("notif_unused_meeting")]
        public int? NotifUnusedMeeting { get; set; }

        [JsonPropertyName("notif_unuse_before_meeting")]
        public int NotifUnuseBeforeMeeting { get; set; }

        [JsonPropertyName("unuse_cancel_fee")]
        public int UnuseCancelFee { get; set; }

        [JsonPropertyName("max_display_duration")]
        public int? MaxDisplayDuration { get; set; }

        [JsonPropertyName("room_pin")]
        public short? RoomPin { get; set; }

        [JsonPropertyName("room_pin_number")]
        public string? RoomPinNumber { get; set; }

        [JsonPropertyName("room_pin_refresh")]
        public TimeOnly? RoomPinRefresh { get; set; }

        [JsonPropertyName("extend_meeting")]
        public int? ExtendMeeting { get; set; }

        [JsonPropertyName("extend_meeting_max")]
        public int? ExtendMeetingMax { get; set; }

        [JsonPropertyName("extend_count_time")]
        public int? ExtendCountTime { get; set; }

        [JsonPropertyName("extend_meeting_notification")]
        public int? ExtendMeetingNotification { get; set; }

        [JsonPropertyName("end_early_meeting")]
        public int? EndEarlyMeeting { get; set; }

        [JsonPropertyName("limit_time_booking")]
        public int? LimitTimeBooking { get; set; }

        [JsonPropertyName("created_by")]
        public string? CreatedBy { get; set; }

        [JsonPropertyName("updated_by")]
        public string? UpdatedBy { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonPropertyName("is_config_setting_enable")]
        public int? IsConfigSettingEnable { get; set; }

        [JsonPropertyName("config_room_for_usage")]
        public string? ConfigRoomForUsage { get; set; }

        [JsonPropertyName("is_enable_approval")]
        public int? IsEnableApproval { get; set; }

        [JsonPropertyName("config_approval_user")]
        public string? ConfigApprovalUser { get; set; }

        [JsonPropertyName("is_enable_permission")]
        public int? IsEnablePermission { get; set; }

        [JsonPropertyName("config_permission_user")]
        public string? ConfigPermissionUser { get; set; }

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
        public int? ConfigParticipantCheckinCount { get; set; }

        [JsonPropertyName("is_enable_checkin_count")]
        public int? IsEnableCheckinCount { get; set; }
    }

    public class SettingRuleBookingCreateViewModelFR
    {
        [BindProperty(Name = "duration")]
        public int? Duration { get; set; }

        [BindProperty(Name = "if_unused_room")]
        public int IfUnusedRoom { get; set; }

        [BindProperty(Name = "max_end_meeting")]
        public int? MaxEndMeeting { get; set; }

        [BindProperty(Name = "notif_unused_meeting")]
        public int? NotifUnusedMeeting { get; set; }

        [BindProperty(Name = "notif_unuse_before_meeting")]
        public int NotifUnuseBeforeMeeting { get; set; }

        [BindProperty(Name = "unuse_cancel_fee")]
        public int UnuseCancelFee { get; set; }

        [BindProperty(Name = "max_display_duration")]
        public int? MaxDisplayDuration { get; set; }

        [BindProperty(Name = "room_pin")]
        public short? RoomPin { get; set; }

        [BindProperty(Name = "room_pin_number")]
        public string? RoomPinNumber { get; set; }

        [BindProperty(Name = "room_pin_refresh")]
        public TimeOnly? RoomPinRefresh { get; set; }

        [BindProperty(Name = "extend_meeting")]
        public int? ExtendMeeting { get; set; }

        [BindProperty(Name = "extend_meeting_max")]
        public int? ExtendMeetingMax { get; set; }

        [BindProperty(Name = "extend_count_time")]
        public int? ExtendCountTime { get; set; }

        [BindProperty(Name = "extend_meeting_notification")]
        public int? ExtendMeetingNotification { get; set; }

        [BindProperty(Name = "end_early_meeting")]
        public int? EndEarlyMeeting { get; set; }

        [BindProperty(Name = "limit_time_booking")]
        public int? LimitTimeBooking { get; set; }

        [BindProperty(Name = "created_by")]
        public string? CreatedBy { get; set; }

        [BindProperty(Name = "updated_by")]
        public string? UpdatedBy { get; set; }

        [BindProperty(Name = "updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [BindProperty(Name = "is_config_setting_enable")]
        public int? IsConfigSettingEnable { get; set; }

        [BindProperty(Name = "config_room_for_usage")]
        public string? ConfigRoomForUsage { get; set; }

        [BindProperty(Name = "is_enable_approval")]
        public int? IsEnableApproval { get; set; }

        [BindProperty(Name = "config_approval_user")]
        public string? ConfigApprovalUser { get; set; }

        [BindProperty(Name = "is_enable_permission")]
        public int? IsEnablePermission { get; set; }

        [BindProperty(Name = "config_permission_user")]
        public string? ConfigPermissionUser { get; set; }

        [BindProperty(Name = "config_permission_checkin")]
        public string? ConfigPermissionCheckin { get; set; }

        [BindProperty(Name = "config_permission_end")]
        public string? ConfigPermissionEnd { get; set; }

        [BindProperty(Name = "config_min_duration")]
        public int? ConfigMinDuration { get; set; }

        [BindProperty(Name = "config_max_duration")]
        public int? ConfigMaxDuration { get; set; }

        [BindProperty(Name = "config_advance_booking")]
        public int? ConfigAdvanceBooking { get; set; }

        [BindProperty(Name = "is_enable_recurring")]
        public int? IsEnableRecurring { get; set; }

        [BindProperty(Name = "is_enable_checkin")]
        public int? IsEnableCheckin { get; set; }

        [BindProperty(Name = "config_advance_checkin")]
        public int? ConfigAdvanceCheckin { get; set; }

        [BindProperty(Name = "is_realease_checkin_timeout")]
        public int? IsRealeaseCheckinTimeout { get; set; }

        [BindProperty(Name = "config_release_room_checkin_timeout")]
        public int? ConfigReleaseRoomCheckinTimeout { get; set; }

        [BindProperty(Name = "config_participant_checkin_count")]
        public int? ConfigParticipantCheckinCount { get; set; }

        [BindProperty(Name = "is_enable_checkin_count")]
        public int? IsEnableCheckinCount { get; set; }
    }

    public class SettingRuleBookingUpdateViewModelFR : SettingRuleBookingCreateViewModelFR
    {
        //[BindProperty(Name = "id", SupportsGet = false)]
        //public long Id { get; set; }

        [BindProperty(Name = "updated_at", SupportsGet = false)]
        public new DateTime? UpdatedAt { get; set; }
    }

    public class SettingRuleBookingDeleteViewModelFR
    {
        [BindProperty(Name = "id")]
        public long Id { get; set; }

        [BindProperty(Name = "name")]
        public string? Name { get; set; }
    }
}
