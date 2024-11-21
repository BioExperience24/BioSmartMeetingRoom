using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class LogActivity
{
    public long Generate { get; set; }

    public string Nik { get; set; } = null!;

    public string AccessIp { get; set; } = null!;

    public string AccessAction { get; set; } = null!;

    public string AccessUrl { get; set; } = null!;

    public DateTime AccessTime { get; set; }

    public string AccessDescription { get; set; } = null!;

    public int AccessQuery { get; set; }
}
