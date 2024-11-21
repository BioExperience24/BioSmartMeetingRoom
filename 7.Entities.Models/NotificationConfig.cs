using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class NotificationConfig
{
    public long Id { get; set; }

    public string? Authorization { get; set; }

    public string? Url { get; set; }

    public string? Topics { get; set; }

    public int Active { get; set; }
}
