using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class SettingSmtp : BaseEntity
{
    public int Id { get; set; }

    public int? SelectedEmail { get; set; }

    public int? IsEnabled { get; set; }

    public string? Name { get; set; }

    public string? TitleEmail { get; set; }

    public string Host { get; set; } = null!;

    public string User { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Port { get; set; }

    public short Secure { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public short IsDeleted { get; set; }
}
