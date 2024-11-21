using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class DeskRoomTable
{
    public int Generate { get; set; }

    public string? DeskId { get; set; }

    public string? DeskRoomId { get; set; }

    public string? ZoneId { get; set; }

    public int? BlockNumber { get; set; }

    public string? PointerDeskX { get; set; }

    public string? PointerDeskY { get; set; }

    public DateTime? Datetime { get; set; }

    public int? IsDeleted { get; set; }
}
