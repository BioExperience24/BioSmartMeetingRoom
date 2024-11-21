using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class LogAccessRoom
{
    public long Id { get; set; }

    public string? BookingId { get; set; }

    public string? RoomId { get; set; }

    public int? IsDefault { get; set; }

    public string? Pin { get; set; }

    public string? Nik { get; set; }

    public DateTime? Datetime { get; set; }

    public string? Msg { get; set; }

    public int? Status { get; set; }
}
