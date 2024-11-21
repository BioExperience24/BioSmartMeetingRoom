using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class MenuGroup
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Icon { get; set; } = null!;
}
