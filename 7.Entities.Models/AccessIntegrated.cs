using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class AccessIntegrated
{
    public long Id { get; set; }

    public string? AccessId { get; set; }

    public string? RoomId { get; set; }

    public int? IsDeleted { get; set; }
}
