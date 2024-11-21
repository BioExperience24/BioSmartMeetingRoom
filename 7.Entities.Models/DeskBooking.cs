using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class DeskBooking
{
    public long Id { get; set; }

    public string BookingId { get; set; } = null!;

    public string? NoOrder { get; set; }

    public string Title { get; set; } = null!;

    public DateOnly Date { get; set; }

    public string? RoomId { get; set; }

    public string DeskId { get; set; } = null!;

    public string? DeskName { get; set; }

    public string? RoomName { get; set; }

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

    public string Participants { get; set; } = null!;

    public int? EndEarlyMeeting { get; set; }

    public string? TextEarly { get; set; }

    public short IsMeal { get; set; }

    public int? IsEar { get; set; }

    public int IsRescheduled { get; set; }

    public int IsCanceled { get; set; }

    public int IsExpired { get; set; }

    public string? CanceledNote { get; set; }

    public string CanceledBy { get; set; } = null!;

    public DateTime CanceledAt { get; set; }

    public string ExpiredBy { get; set; } = null!;

    public DateTime ExpiredAt { get; set; }

    public string RescheduledBy { get; set; } = null!;

    public DateTime RescheduledAt { get; set; }

    public string EarlyEndedBy { get; set; } = null!;

    public DateTime EarlyEndedAt { get; set; }

    public int IsAlive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int IsNotifBeforeEndMeeting { get; set; }

    public int IsNotifEndMeeting { get; set; }

    public int? IsDevice { get; set; }

    public int? IsAccessTrigger { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public short IsDeleted { get; set; }
}
