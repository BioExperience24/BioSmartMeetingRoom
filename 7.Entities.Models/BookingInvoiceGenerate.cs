using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class BookingInvoiceGenerate
{
    public long Id { get; set; }

    public string? InvoiceId { get; set; }

    public string? InvoiceFormat { get; set; }

    public int? InvoiceMonth1 { get; set; }

    public int? InvoiceMonth2 { get; set; }

    public long? InvoiceYears { get; set; }

    public string? MemoNo { get; set; }

    public string? ReferensiNo { get; set; }

    public string? AlocationId { get; set; }

    public string? TotalCost { get; set; }

    public int? TotalMeeting { get; set; }

    public long? TotalDuration { get; set; }

    public string? Status { get; set; }

    public DateTime? DateGenerate { get; set; }

    public DateTime? DateSending { get; set; }

    public DateTime? DateConfirm { get; set; }

    public string? GenerateBy { get; set; }

    public string? SendingBy { get; set; }

    public string? ConfirmBy { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public int? IsDeleted { get; set; }
}
