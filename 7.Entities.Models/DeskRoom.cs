using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class DeskRoom
{
    public long Generate { get; set; }

    public string Id { get; set; } = null!;

    public int? BuildingId { get; set; }

    public string Name { get; set; } = null!;

    public int Capacity { get; set; }

    public string Description { get; set; } = null!;

    public int AutomationId { get; set; }

    public string FacilityRoom { get; set; } = null!;

    public string WorkDay { get; set; } = null!;

    public string WorkTime { get; set; } = null!;

    public TimeOnly WorkStart { get; set; }

    public TimeOnly WorkEnd { get; set; }

    public string? GoogleMap { get; set; }

    public string? Image { get; set; }

    public string? Image2 { get; set; }

    public string RoomMap { get; set; } = null!;

    public long Price { get; set; }

    public string? Location { get; set; }

    public short? IsDisabled { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public short IsDeleted { get; set; }
}
