using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class NotifBooking
{
    public int Id { get; set; }

    public string NotifId { get; set; } = null!;

    public string BookingId { get; set; } = null!;

    public long EmployeeId { get; set; }

    public int IsReschedule { get; set; }

    public int IsInvited { get; set; }

    public int IsNotifhandler { get; set; }

    public int IsNotifSend { get; set; }
}
