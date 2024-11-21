using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class RoomMergeDetail
{
    public long Id { get; set; }

    public string MergeRoomId { get; set; } = null!;

    public string RoomId { get; set; } = null!;
}
