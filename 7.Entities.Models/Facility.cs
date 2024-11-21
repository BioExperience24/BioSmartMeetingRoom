using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class Facility : BaseEntity
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string GoogleIcon { get; set; } = null!;

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public short IsDeleted { get; set; }
}
