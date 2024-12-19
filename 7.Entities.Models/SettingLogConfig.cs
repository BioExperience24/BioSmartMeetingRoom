using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class SettingLogConfig : BaseEntity
{
    public new long Id { get; set; }

    public string? Text { get; set; }
}
