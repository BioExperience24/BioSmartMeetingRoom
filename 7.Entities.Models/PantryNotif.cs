using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class PantryNotif
{
    public int Id { get; set; }

    public string NotifId { get; set; } = null!;

    public string BookingId { get; set; } = null!;

    public long PantryTrsId { get; set; }

    public string Title { get; set; } = null!;

    public string Message { get; set; } = null!;

    public int IsNotifhandler { get; set; }
}
