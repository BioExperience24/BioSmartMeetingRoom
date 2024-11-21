using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class BeaconFloor
{
    public int Id { get; set; }

    public int? BuildingId { get; set; }

    public string? Name { get; set; }

    public string? Image { get; set; }

    public string? Pixel { get; set; }

    public double? FloorLength { get; set; }

    public double? FloorWidth { get; set; }

    public string? MeterPerPx { get; set; }

    public string? MeterPerPx2 { get; set; }

    public double? PlusWidth { get; set; }

    public double? PlusHeight { get; set; }

    public int? CenterX { get; set; }

    public int? CenterY { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? IsDeleted { get; set; }
}
