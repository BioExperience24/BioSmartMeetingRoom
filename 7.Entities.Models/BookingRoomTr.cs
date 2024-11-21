using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class BookingRoomTr
{
    public string? RoomId { get; set; }

    public string? BookingId { get; set; }

    public DateOnly? Date { get; set; }
}
