using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class LicenseList
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Type { get; set; }

    public string? Module { get; set; }

    public string? ExpiredAt { get; set; }

    public int? IsLifetime { get; set; }

    public string Status { get; set; } = null!;

    public int? Qty { get; set; }

    public string? PlatformSerial { get; set; }
}
