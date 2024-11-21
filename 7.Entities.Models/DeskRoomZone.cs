using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class DeskRoomZone
{
    public int Generate { get; set; }

    public string? DeskRoomId { get; set; }

    public string? ZoneId { get; set; }

    public string? Name { get; set; }

    public string? Pointer { get; set; }

    public double? Size { get; set; }

    public string? Color { get; set; }
}
