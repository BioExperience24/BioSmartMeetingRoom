using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class RoomForUsageDetail
{
    public long? RoomId { get; set; }

    public int? RoomUsageId { get; set; }

    public int? MinCap { get; set; }

    public int? Internal { get; set; }

    public int? External { get; set; }
}
