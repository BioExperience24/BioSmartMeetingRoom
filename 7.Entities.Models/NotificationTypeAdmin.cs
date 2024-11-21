using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class NotificationTypeAdmin
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public string? Element { get; set; }

    public string? Route { get; set; }
}
