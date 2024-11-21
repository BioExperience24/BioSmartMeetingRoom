using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class PantryDetailMenuVariantDetail
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string VariantId { get; set; } = null!;

    public int? Price { get; set; }

    public int IsDeleted { get; set; }
}
