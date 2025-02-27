using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class TimeSchedule15
{
    public int Timeid { get; set; }

    public string Time { get; set; } = null!;

    public int IsDeleted { get; set; }
}

public class TimeSchedule : TimeSchedule15
{ }
