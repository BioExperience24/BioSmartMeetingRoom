using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class BookingInvoiceDetail
{
    public int Id { get; set; }

    public string? InvoiceId { get; set; }

    public string? NoUrut { get; set; }

    public string? NoInvoice { get; set; }

    public string? AlocationId { get; set; }

    public string? AlocationName { get; set; }

    public long? TotalCost { get; set; }

    public int? TotalDuration { get; set; }

    public int? TotalMeeting { get; set; }

    public int? OutstandingStatus { get; set; }

    public string? InvoiceStatus { get; set; }

    public string? AlocationType { get; set; }

    public string? CostCode { get; set; }

    public string? CreatedBy { get; set; }

    public string? SentBy { get; set; }

    public string? PaidBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? SentAt { get; set; }

    public DateTime? PaidAt { get; set; }
}
