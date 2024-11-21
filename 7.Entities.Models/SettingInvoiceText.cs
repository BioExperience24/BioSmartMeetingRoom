using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class SettingInvoiceText
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? IsDeleted { get; set; }
}
