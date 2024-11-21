using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class HelpdeskMonitor
{
    public int Generate { get; set; }

    public string Id { get; set; } = null!;

    public string RoomId { get; set; } = null!;

    public DateTime Datetime { get; set; }

    public int Action { get; set; }

    public int Response { get; set; }

    public string Comment { get; set; } = null!;

    public string ReasonResponse { get; set; } = null!;

    public int IsDeleted { get; set; }
}
