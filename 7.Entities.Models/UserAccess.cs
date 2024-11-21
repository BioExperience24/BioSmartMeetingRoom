using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class UserAccess
{
    public int AccessId { get; set; }

    public string? AccessName { get; set; }

    public int? IsActive { get; set; }
}
