using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class NotificationType
{
    public long Id { get; set; }

    public string? Type { get; set; }

    public string? Cololr { get; set; }

    public string? Name { get; set; }

    public string? Route { get; set; }

    public string? Table { get; set; }

    public string? Where { get; set; }

    public string? Topics { get; set; }
}
