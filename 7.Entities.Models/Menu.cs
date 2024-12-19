using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace _7.Entities.Models;

public partial class Menu : BaseLongEntity
{

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

}

public class MenuDto
{
    public int IsChild { get; set; }

    [JsonIgnore]
    public bool IsChildBool => IsChild == 1;
    public string MgIcon { get; set; } = string.Empty;
    public int MenuGroupId { get; set; }
    public string GMenuName { get; set; } = string.Empty;
    public string LevelName { get; set; } = string.Empty;
    public string MenuName { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public bool Active { get; set; }
}