using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class Pantry
{
    public long Id { get; set; }

    public int? BuildingId { get; set; }

    public string Name { get; set; } = null!;

    public string Detail { get; set; } = null!;

    public string Location { get; set; } = null!;

    public int Days { get; set; }

    public TimeOnly OpeningHoursStart { get; set; }

    public TimeOnly OpeningHoursEnd { get; set; }

    public int? IsShowPrice { get; set; }

    public string Pic { get; set; } = null!;

    public string EmployeeId { get; set; } = null!;

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public short IsDeleted { get; set; }

    public int? IsApproval { get; set; }

    public int? IsInternal { get; set; }

    public string? OwnerPantry { get; set; }
}
