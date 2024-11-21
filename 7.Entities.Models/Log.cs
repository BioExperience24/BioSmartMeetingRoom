using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class Log
{
    public long Id { get; set; }

    public int UserId { get; set; }

    public DateTime Datetime { get; set; }

    public string Activity { get; set; } = null!;

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public short IsDeleted { get; set; }
}
