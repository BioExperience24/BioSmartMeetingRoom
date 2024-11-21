using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class NotificationAdmin
{
    public long Id { get; set; }

    public string? Nik { get; set; }

    public int? Type { get; set; }

    public DateTime? Datetime { get; set; }

    public string? Title { get; set; }

    public string? Body { get; set; }

    public int? IsRead { get; set; }

    public int? IsSending { get; set; }

    public int? IsDeleted { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
