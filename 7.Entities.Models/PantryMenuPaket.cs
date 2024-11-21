using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class PantryMenuPaket
{
    public int Generate { get; set; }

    public string Id { get; set; } = null!;

    public int PantryId { get; set; }

    public string Name { get; set; } = null!;

    public int CreatedBy { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int IsDeleted { get; set; }
}
