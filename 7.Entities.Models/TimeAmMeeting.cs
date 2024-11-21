using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class TimeAmMeeting
{
    public long Id { get; set; }

    public int? Time { get; set; }

    public int? Day { get; set; }

    public string? Desc { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? IsDisactivated { get; set; }

    public int? IsDeleted { get; set; }
}
