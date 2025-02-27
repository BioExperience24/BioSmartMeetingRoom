using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class LevelDetail
{
    public long Id { get; set; }

    public int LevelId { get; set; }

    public int MenuId { get; set; }

    public string? Coment { get; set; }
}
