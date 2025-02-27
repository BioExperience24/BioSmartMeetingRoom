using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class BookingInvoice : BaseLongEntity
{

    public string? InvoiceGenerateNo { get; set; }

    public string? InvoiceNo { get; set; }

    public string? InvoiceFormat { get; set; }

    public string? BookingId { get; set; }

    public long? RentCost { get; set; }

    public string? Alocation { get; set; }

    public string? MemoNo { get; set; }

    public string? ReferensiNo { get; set; }

    public DateTime? TimeBefore { get; set; }

    public DateTime? TimeSend { get; set; }

    public DateTime? TimePaid { get; set; }

    public string? InvoiceStatus { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }
}
