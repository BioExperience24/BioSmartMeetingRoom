using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class Room
{
    public long Id { get; set; }

    public string Radid { get; set; } = null!;

    public int? BuildingId { get; set; }

    public int? FloorId { get; set; }

    public string? TypeRoom { get; set; }

    public string Name { get; set; } = null!;

    public int Capacity { get; set; }

    public string Description { get; set; } = null!;

    public string? GoogleMap { get; set; }

    public short IsAutomation { get; set; }

    public int AutomationId { get; set; }

    public string FacilityRoom { get; set; } = null!;

    public string WorkDay { get; set; } = null!;

    public string WorkTime { get; set; } = null!;

    public TimeOnly WorkStart { get; set; }

    public TimeOnly WorkEnd { get; set; }

    public string? Image { get; set; }

    public string Image2 { get; set; } = null!;

    public string MultipleImage { get; set; } = null!;

    public long Price { get; set; }

    public string? Location { get; set; }

    public short? IsDisabled { get; set; }

    public int? IsBeacon { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public short IsDeleted { get; set; }

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

    public int ConfigParticipantCheckinCount { get; set; }

    public int? IsEnableCheckinCount { get; set; }

    public string? ConfigGoogle { get; set; }

    public string? ConfigMicrosoft { get; set; }
}
