using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class AccessControllerType
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int IsDeleted { get; set; }
}
