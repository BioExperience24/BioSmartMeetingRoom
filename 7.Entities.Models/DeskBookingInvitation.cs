using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class DeskBookingInvitation
{
    public int Id { get; set; }

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

    public string? PinRoom { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public int IsDeleted { get; set; }
}
