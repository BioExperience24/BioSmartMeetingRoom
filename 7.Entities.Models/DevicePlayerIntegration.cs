using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class DevicePlayerIntegration
{
    public int Generate { get; set; }

    public string Id { get; set; } = null!;

    public string? Serial { get; set; }

    public string? Type { get; set; }

    public string? HardwareId { get; set; }

    public string? Uuid { get; set; }

    public string? Mac { get; set; }

    public string? Os { get; set; }

    public string? Info { get; set; }

    public string? Version { get; set; }

    public int? IsDeleted { get; set; }

    public int? IsActived { get; set; }

    public DateTime? CreatedAt { get; set; }
}
