using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class LevelHeaderDetail
{
    public long Id { get; set; }

    public int LevelId { get; set; }

    public string MenuId { get; set; } = null!;

    public string? Coment { get; set; }
}
