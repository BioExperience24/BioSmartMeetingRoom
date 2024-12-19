using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class LevelDescriptiion : BaseLongEntity
{
    // public int Id { get; set; }

    public int LevelId { get; set; }

    public string Description { get; set; } = null!;

    // public int IsDeleted { get; set; }
}
