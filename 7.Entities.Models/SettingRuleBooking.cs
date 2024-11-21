using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class SettingRuleBooking
{
    public long Id { get; set; }

    public int? Duration { get; set; }

    public int IfUnusedRoom { get; set; }

    public int? MaxEndMeeting { get; set; }

    public int? NotifUnusedMeeting { get; set; }

    public int NotifUnuseBeforeMeeting { get; set; }

    public int UnuseCancelFee { get; set; }

    public int? MaxDisplayDuration { get; set; }

    public short? RoomPin { get; set; }

    public string? RoomPinNumber { get; set; }

    public TimeOnly? RoomPinRefresh { get; set; }

    public int? ExtendMeeting { get; set; }

    public int? ExtendMeetingMax { get; set; }

    public int? ExtendCountTime { get; set; }

    public int? ExtendMeetingNotification { get; set; }

    public int? EndEarlyMeeting { get; set; }

    public int? LimitTimeBooking { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? IsConfigSettingEnable { get; set; }

    public string? ConfigRoomForUsage { get; set; }

    public int? IsEnableApproval { get; set; }

    public string? ConfigApprovalUser { get; set; }

    public int? IsEnablePermission { get; set; }

    public string? ConfigPermissionUser { get; set; }

    public string? ConfigPermissionCheckin { get; set; }

    public string? ConfigPermissionEnd { get; set; }

    public int? ConfigMinDuration { get; set; }

    public int? ConfigMaxDuration { get; set; }

    public int? ConfigAdvanceBooking { get; set; }

    public int? IsEnableRecurring { get; set; }

    public int? IsEnableCheckin { get; set; }

    public int? ConfigAdvanceCheckin { get; set; }

    public int? IsRealeaseCheckinTimeout { get; set; }

    public int? ConfigReleaseRoomCheckinTimeout { get; set; }

    public int? ConfigParticipantCheckinCount { get; set; }

    public int? IsEnableCheckinCount { get; set; }
}
