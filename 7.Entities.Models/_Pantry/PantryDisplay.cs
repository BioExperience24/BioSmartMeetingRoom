using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class PantryDisplay
{
    public long Id { get; set; }

    public string? DisplaySerial { get; set; }

    public string? Type { get; set; }

    public string? Background { get; set; }

    public int? BackgroundUpdate { get; set; }

    public string? ColorOccupied { get; set; }

    public string? ColorAvailable { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? IsDeleted { get; set; }

    public int? StatusSync { get; set; }

    public int? Enabled { get; set; }

    public string? HardwareUuid { get; set; }

    public string? HardwareInfo { get; set; }

    public DateTime? HardwareLastsync { get; set; }

    public string? RoomSelect { get; set; }

    public string? DisableMsg { get; set; }
}
