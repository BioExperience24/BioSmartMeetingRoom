using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class BookingInvitation : BaseLongEntity
{
    // public int Id { get; set; }

    public string BookingId { get; set; } = null!;

    public string Nik { get; set; } = null!;

    public int Internal { get; set; }

    public int AttendanceStatus { get; set; }

    public string? AttendanceReason { get; set; }

    public int? ExecuteAttendance { get; set; }

    public int ExecuteDoorAccess { get; set; }

    public string Email { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Company { get; set; } = null!;

    public string? Position { get; set; }

    public short IsPic { get; set; }

    public int? IsVip { get; set; }

    public string? PinRoom { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    // public int IsDeleted { get; set; }

    public string? LastUpdate365 { get; set; }

    public int? Checkin { get; set; }

    public int? EndMeeting { get; set; }
}

public class BookingInvitationFilter : BookingInvitation
{
    public DateOnly? DateStart { get; set; }
    public DateOnly? DateEnd { get; set; }
    public long BuildingId { get; set; }
    public string? RoomId { get; set; }
}

public class BookingInvitationEmployee : BookingInvitation
{
    public string? EmployeeName { get; set; }
    public string? EmployeeNoPhone { get; set; }
    public string? EmployeeEmail { get; set; }
    public string? EmployeeId { get; set; }
    public string? EmployeeNrp { get; set; }
    public string? EmployeeNikDisplay { get; set; }
}

public class BookingInvitationPinAccess : BookingInvitation
{
    public int? IsConfigSettingEnable { get; set; }
    public int? IsEnableCheckin { get; set; }
    public int? IsRealeaseCheckinTimeout { get; set; }
    public int? ConfigReleaseRoomCheckinTimeout { get; set; }
    public string? ConfigPermissionCheckin { get; set; }
    public string? ConfigPermissionEnd { get; set; }
}