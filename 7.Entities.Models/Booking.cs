using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace _7.Entities.Models;

public partial class Booking : BaseLongEntity
{

    public string BookingId { get; set; } = null!;

    public string? BookingId365 { get; set; }

    public string? BookingIdGoogle { get; set; }

    public string? BookingDevices { get; set; }

    public string? NoOrder { get; set; }

    public string Title { get; set; } = null!;

    public DateTime Date { get; set; }

    public string RoomId { get; set; } = null!;

    public string? RoomName { get; set; }

    public short? IsMerge { get; set; }

    public string? MergeRoom { get; set; }

    public string? MergeRoomId { get; set; }

    public string? MergeRoomName { get; set; }

    public DateTime Start { get; set; }

    public DateTime End { get; set; }

    public long? CostTotalBooking { get; set; }

    public int? DurationPerMeeting { get; set; }

    public int? TotalDuration { get; set; }

    public int? ExtendedDuration { get; set; }

    public string Pic { get; set; } = null!;

    public string? AlocationId { get; set; }

    public string? AlocationName { get; set; }

    public string Note { get; set; } = null!;

    public string? CanceledNote { get; set; }

    public string Participants { get; set; } = null!;

    public string? ExternalLink { get; set; }

    public string? ExternalLink365 { get; set; }

    public string? ExternalLinkGoogle { get; set; }

    public int EndEarlyMeeting { get; set; }

    public string? TextEarly { get; set; }

    public int? IsDevice { get; set; }

    public short IsMeal { get; set; }

    public int IsEar { get; set; }

    public int IsRescheduled { get; set; }

    public int IsCanceled { get; set; }

    public int IsExpired { get; set; }

    public string CanceledBy { get; set; } = null!;

    public DateTime CanceledAt { get; set; }

    public string ExpiredBy { get; set; } = null!;

    public DateTime ExpiredAt { get; set; }

    public string RescheduledBy { get; set; } = null!;

    public DateTime RescheduledAt { get; set; }

    public string EarlyEndedBy { get; set; } = null!;

    public DateTime EarlyEndedAt { get; set; }

    public int IsAlive { get; set; }

    public string? Timezone { get; set; }

    public string? Comment { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public int IsNotifEndMeeting { get; set; }

    public int IsNotifBeforeEndMeeting { get; set; }

    public int? IsAccessTrigger { get; set; }

    public int? IsConfigSettingEnable { get; set; }

    public int? IsEnableApproval { get; set; }

    public int? IsEnablePermission { get; set; }

    public int? IsEnableRecurring { get; set; }

    public int? IsEnableCheckin { get; set; }

    public int? IsRealeaseCheckinTimeout { get; set; }

    public int? IsReleased { get; set; }

    public int IsEnableCheckinCount { get; set; }

    public int? Category { get; set; }

    public string? LastModifiedDateTime365 { get; set; }

    public string? PermissionEnd { get; set; }

    public string? PermissionCheckin { get; set; }

    public int? ReleaseRoomCheckinTime { get; set; }

    public int? CheckinCount { get; set; }

    public int? IsVip { get; set; }

    public int? IsApprove { get; set; }

    public string? VipUser { get; set; }

    public string? UserEndMeeting { get; set; }

    public string? UserCheckin { get; set; }

    public string? UserApproval { get; set; }

    public DateTime? UserApprovalDatetime { get; set; }

    public string? RoomMeetingMove { get; set; }

    public string? RoomMeetingOld { get; set; }

    public int? IsMoved { get; set; }

    public int? IsMovedAgree { get; set; }

    public int? MovedDuration { get; set; }

    public string? MeetingEndNote { get; set; }

    public int? VipApproveBypass { get; set; }

    public int? VipLimitCapBypass { get; set; }

    public int? VipLockRoom { get; set; }

    public string? VipForceMoved { get; set; }

    public int? DurationSavedRelease { get; set; }

    public int? IsCleaningNeed { get; set; }

    public int? CleaningTime { get; set; }

    public DateTime? CleaningStart { get; set; }

    public DateTime? CleaningEnd { get; set; }

    public string? UserCleaning { get; set; }

    public DateTime? ServerDate { get; set; }

    public DateTime? ServerStart { get; set; }

    public DateTime? ServerEnd { get; set; }
}


public class BookingMenuDto
{
    [JsonPropertyName("level_id")]
    public long? LevelId { get; set; }

    [JsonPropertyName("level_name")]
    public string LevelName { get; set; }

    [JsonPropertyName("menu_group_id")]
    public long? MenuGroupId { get; set; }

    [JsonPropertyName("g_menu_name")]
    public string GMenuName { get; set; }

    [JsonPropertyName("g_menu_id")]
    public long? GMenuId { get; set; }

    [JsonPropertyName("mg_icon")]
    public string MgIcon { get; set; }

    [JsonPropertyName("menu_name")]
    public string MenuName { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("icon")]
    public string Icon { get; set; }

    [JsonPropertyName("is_child")]
    public int? IsChild { get; set; }
}
