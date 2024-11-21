using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class RoomDetail
{
    public long Id { get; set; }

    public string RoomId { get; set; } = null!;

    public string FacilityId { get; set; } = null!;

    public DateTime? Datetime { get; set; }
}
