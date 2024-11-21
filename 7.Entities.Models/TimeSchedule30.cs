using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class TimeSchedule30
{
    public int Timeid { get; set; }

    public string Time { get; set; } = null!;

    public int IsDeleted { get; set; }
}
