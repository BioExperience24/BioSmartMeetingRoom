using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class PantryDetailMenuVariant
{
    public string Id { get; set; } = null!;

    public int MenuId { get; set; }

    public string Name { get; set; } = null!;

    public int Multiple { get; set; }

    public int Min { get; set; }

    public int Max { get; set; }

    public int IsDeleted { get; set; }
}
