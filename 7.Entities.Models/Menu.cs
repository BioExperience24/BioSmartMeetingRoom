using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class Menu
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Url { get; set; } = null!;

    public string TypeIcon { get; set; } = null!;

    public string Icon { get; set; } = null!;

    public int Sort { get; set; }

    public int IsChild { get; set; }

    public int MenuGroupId { get; set; }

    public string ModuleText { get; set; } = null!;

    public int? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public short IsDeleted { get; set; }
}
