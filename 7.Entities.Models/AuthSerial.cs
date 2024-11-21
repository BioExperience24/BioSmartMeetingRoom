using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class AuthSerial
{
    public int Id { get; set; }

    public string Serial { get; set; } = null!;

    public int IsDeleted { get; set; }
}
