using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class RoomDetail
{
    public long? Id { get; set; }

    public long? RoomId { get; set; } 

    public long? FacilityId { get; set; }

    public DateTime? Datetime { get; set; }
}
