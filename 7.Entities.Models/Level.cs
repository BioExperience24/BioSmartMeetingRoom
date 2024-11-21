using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class Level
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public int DefaultMenu { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public short IsDeleted { get; set; }
}
