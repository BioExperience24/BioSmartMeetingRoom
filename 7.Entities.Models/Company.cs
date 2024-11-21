using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class Company : BaseEntity
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string City { get; set; } = null!;

    public string State { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Picture { get; set; } = null!;

    public string? Icon { get; set; }

    public string? Logo { get; set; }

    public string? MenuBar { get; set; }

    public string? UrlAddress { get; set; }

    public int CreatedBy { get; set; }

    public int CreatedAt { get; set; }

    public int UpdateAt { get; set; }

    public int IsDeleted { get; set; }
}
