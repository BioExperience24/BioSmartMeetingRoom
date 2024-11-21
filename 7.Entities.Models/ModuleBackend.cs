using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class ModuleBackend
{
    public int ModuleId { get; set; }

    public string ModuleText { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? ModuleSerial { get; set; }

    public int IsEnabled { get; set; }
}
