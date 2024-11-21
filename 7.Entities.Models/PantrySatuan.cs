using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class PantrySatuan
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int IsDeleted { get; set; }
}
