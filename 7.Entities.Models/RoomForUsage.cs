using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class RoomForUsage
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public int? IsDeleted { get; set; }
}
