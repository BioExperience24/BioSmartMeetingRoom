using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class Locker
{
    public int Generate { get; set; }

    public string? Id { get; set; }

    public string? Name { get; set; }

    public string? IpLocker { get; set; }

    public string? AutoReserve { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? IsDeleted { get; set; }
}
