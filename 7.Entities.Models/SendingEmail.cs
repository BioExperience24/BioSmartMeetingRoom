using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class SendingEmail
{
    public int Id { get; set; }

    public string Batch { get; set; } = null!;

    public int? Type { get; set; }

    public string BookingId { get; set; } = null!;

    public string Pending { get; set; } = null!;

    public int ErrorSending { get; set; }

    public int Success { get; set; }

    public int? IsStatus { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public short IsDeleted { get; set; }
}
