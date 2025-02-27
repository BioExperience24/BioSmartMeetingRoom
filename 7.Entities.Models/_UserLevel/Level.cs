using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _7.Entities.Models;

// public partial class Level : BaseEntity
public partial class Level : BaseLongEntity
{
    // public long Id { get; set; }

    public string Name { get; set; } = null!;

    public int DefaultMenu { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    // public short IsDeleted { get; set; }

    public int? SortLevel { get; set; }
}

public class MenuHeaderLevel
{
    public string LevelName { get; set; }
    public string MenuName { get; set; }
    public string Url { get; set; }
    public string Icon { get; set; }
}

public class LevelMenu
{
    public string MenuName { get; set; } = string.Empty;
    public string MenuIcon { get; set; } = string.Empty;
    public string MenuUrl { get; set; } = string.Empty;
    public int MenuSort { get; set; }
    public int IsChild { get; set; }
    public int MenuGroupId { get; set; }
    public string ModuleText { get; set; } = string.Empty;
    public string? GroupName { get; set; }
    public string? GroupIcon { get; set; }
}
