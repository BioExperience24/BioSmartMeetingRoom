﻿using System.ComponentModel.DataAnnotations.Schema;
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

    public DateOnly Date { get; set; }

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

    public string BookingType { get; set; } = "general";
    
    public int IsPrivate { get; set; }

    [NotMapped] // tidak akan dipetakan ke kolom dalam basis data
    public long BuildingId { get; set; }
    [NotMapped] // tidak akan dipetakan ke kolom dalam basis data
    public DateOnly? DateStart { get; set; }
    [NotMapped] // tidak akan dipetakan ke kolom dalam basis data
    public DateOnly? DateEnd { get; set; }
}

public class BookingChart
{
    public string Month { get; set; } = string.Empty;
    public string Md { get; set; } = string.Empty;
    public int Total { get; set; }
    public int Tahun { get; set; }
}

public class BookingMenuDto
{
    [JsonPropertyName("level_id")]
    public long? LevelId { get; set; }

    [JsonPropertyName("level_name")]
    public string LevelName { get; set; } = null!;

    [JsonPropertyName("menu_group_id")]
    public long? MenuGroupId { get; set; }

    [JsonPropertyName("g_menu_name")]
    public string GMenuName { get; set; } = null!;

    [JsonPropertyName("g_menu_id")]
    public long? GMenuId { get; set; }

    [JsonPropertyName("mg_icon")]
    public string MgIcon { get; set; } = null!;

    [JsonPropertyName("menu_name")]
    public string MenuName { get; set; } = null!;

    [JsonPropertyName("url")]
    public string Url { get; set; } = null!;

    [JsonPropertyName("icon")]
    public string Icon { get; set; } = null!;

    [JsonPropertyName("is_child")]
    public int? IsChild { get; set; }
}

public class BookingDto
{
    public string NumParticipant { get; set; }
    public string Id { get; set; }
    public string BookingId { get; set; } = null!;
    public string BookingId365 { get; set; }
    public string BookingIdGoogle { get; set; }
    public string BookingDevices { get; set; }
    public string NoOrder { get; set; }
    public string Title { get; set; }
    public string Date { get; set; }
    public string RoomId { get; set; }
    public string RoomName { get; set; }
    public string IsMerge { get; set; }
    public string MergeRoom { get; set; }
    public string MergeRoomId { get; set; }
    public string MergeRoomName { get; set; }
    public string Start { get; set; }
    public string End { get; set; }
    public string CostTotalBooking { get; set; }
    public string DurationPerMeeting { get; set; }
    public string TotalDuration { get; set; }
    public string ExtendedDuration { get; set; }
    public string Pic { get; set; }
    public string AlocationId { get; set; }
    public string AlocationName { get; set; }
    public string Note { get; set; }
    public string CanceledNote { get; set; }
    public string Participants { get; set; }
    public string ExternalLink { get; set; }
    public string ExternalLink365 { get; set; }
    public string ExternalLinkGoogle { get; set; }
    public string EndEarlyMeeting { get; set; }
    public string TextEarly { get; set; }
    public string IsDevice { get; set; }
    public string IsMeal { get; set; }
    public string IsEar { get; set; }
    public string IsRescheduled { get; set; }
    public string IsCanceled { get; set; }
    public string IsExpired { get; set; }
    public string CanceledBy { get; set; }
    public string CanceledAt { get; set; }
    public string ExpiredBy { get; set; }
    public string ExpiredAt { get; set; }
    public string RescheduledBy { get; set; }
    public string RescheduledAt { get; set; }
    public string EarlyEndedBy { get; set; }
    public string EarlyEndedAt { get; set; }
    public string IsAlive { get; set; }
    public string Timezone { get; set; }
    public string Comment { get; set; }
    public string CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public string UpdatedAt { get; set; }
    public string UpdatedBy { get; set; }
    public string IsNotifEndMeeting { get; set; }
    public string IsNotifBeforeEndMeeting { get; set; }
    public string IsAccessTrigger { get; set; }
    public string IsDeleted { get; set; }
    public string IsConfigSettingEnable { get; set; }
    public string IsEnableApproval { get; set; }
    public string IsEnablePermission { get; set; }
    public string IsEnableRecurring { get; set; }
    public string IsEnableCheckin { get; set; }
    public string IsRealeaseCheckinTimeout { get; set; }
    public string IsReleased { get; set; }
    public string IsEnableCheckinCount { get; set; }
    public string Category { get; set; }
    public string LastModifiedDateTime365 { get; set; }
    public string PermissionEnd { get; set; }
    public string PermissionCheckin { get; set; }
    public string ReleaseRoomCheckinTime { get; set; }
    public string CheckinCount { get; set; }
    public string IsVip { get; set; }
    public string IsApprove { get; set; }
    public string VipUser { get; set; }
    public string UserEndMeeting { get; set; }
    public string UserCheckin { get; set; }
    public string UserApproval { get; set; }
    public string UserApprovalDatetime { get; set; }
    public string RoomMeetingMove { get; set; }
    public string RoomMeetingOld { get; set; }
    public string IsMoved { get; set; }
    public string IsMovedAgree { get; set; }
    public string MovedDuration { get; set; }
    public string MeetingEndNote { get; set; }
    public string VipApproveBypass { get; set; }
    public string VipLimitCapBypass { get; set; }
    public string VipLockRoom { get; set; }
    public string VipForceMoved { get; set; }
    public string DurationSavedRelease { get; set; }
    public string IsCleaningNeed { get; set; }
    public string CleaningTime { get; set; }
    public string CleaningStart { get; set; }
    public string CleaningEnd { get; set; }
    public string UserCleaning { get; set; }
    public string ServerDate { get; set; }
    public string ServerStart { get; set; }
    public string ServerEnd { get; set; }
    public string RoomLocation { get; set; }
    public string MemoNo { get; set; }
    public string ReferensiNo { get; set; }
    public string InvoiceStatus { get; set; }
    public string AlocationType { get; set; }
    public string AlocationInvoiceStatus { get; set; }
    public string AlcoationTypeInvoiceStatus { get; set; }
    public string NameEmployee { get; set; }
    public string EmailEmployee { get; set; }
    public string PhoneEmployee { get; set; }
    public string ExtEmployee { get; set; }
}
