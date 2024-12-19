using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

// public partial class UserConfig : BaseEntity
public partial class UserConfig
{
    public int Id { get; set; }

    public string DefaultPassword { get; set; } = null!;

    public int PasswordLength { get; set; }
}
