using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class DeskController
{
    public int Generate { get; set; }

    public string? Id { get; set; }

    public string? Name { get; set; }

    public string? IpAddress { get; set; }

    public int? Capacity { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public int? IsDeleted { get; set; }
}
