using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class PantryMenuPaketD
{
    public int Generate { get; set; }

    public int MenuId { get; set; }

    public int PackageId { get; set; }

    public int IsDeleted { get; set; }
}
