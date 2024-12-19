using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class RoomAutomation : BaseLongEntity
{
    // public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string IpAddress { get; set; } = null!;

    public string Serial { get; set; } = null!;

    public string Room { get; set; } = null!;

    public string Devices { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    // public int IsDeleted { get; set; }
}
