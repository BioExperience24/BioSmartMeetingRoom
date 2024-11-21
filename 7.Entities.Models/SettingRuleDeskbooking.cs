using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class SettingRuleDeskbooking
{
    public long Id { get; set; }

    public int? Duration { get; set; }

    public int? IfUnusedRoom { get; set; }

    public int? MaxEndMeeting { get; set; }

    public int? NotifUnusedMeeting { get; set; }

    public int? NotifUnuseBeforeMeeting { get; set; }

    public int? UnuseCancelFee { get; set; }

    public int? MaxDisplayDuration { get; set; }

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

    public int? ConfigMinDuration { get; set; }

    public int? ConfigMaxDuration { get; set; }

    public int? ConfigAdvanceBooking { get; set; }

    public int? IsConfigCheckinEnable { get; set; }

    public int? ConfigEnableCheckin { get; set; }

    public int? ConfigPermissionCheckin { get; set; }

    public int? ConfigPermissionEnd { get; set; }

    public int? ConfigAdvanceCheckin { get; set; }

    public int? ConfigReleaseRoomCheckinEnable { get; set; }

    public int? ConfigReleaseRoomCheckinTime { get; set; }

    public int? ConfigParticipantCheckinCount { get; set; }
}
