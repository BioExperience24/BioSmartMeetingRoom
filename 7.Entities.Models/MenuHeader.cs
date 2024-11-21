using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class MenuHeader
{
    public int Generate { get; set; }

    public string? Id { get; set; }

    public string? Name { get; set; }

    public int? Sort { get; set; }

    public string? Url { get; set; }

    public string? Icon { get; set; }

    public string? ModuleText { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? IsDeleted { get; set; }
}
