using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class SettingInvoiceConfig : BaseEntity
{
    public long Id { get; set; }

    public string? DateFormat { get; set; }

    public string? DateText { get; set; }

    public string? ToText { get; set; }

    public string? UpText { get; set; }

    public string? NoInvText { get; set; }

    public string? NoProfitText { get; set; }

    public string? DescriptionText { get; set; }

    public string? AmountText { get; set; }

    public string? ContentText { get; set; }

    public string? AmountBillText { get; set; }

    public string? TaxText { get; set; }

    public int? TaxAmount { get; set; }

    public string? TotalText { get; set; }

    public string? FooterText { get; set; }

    public string? Footer2Text { get; set; }

    public string? Footer3Text { get; set; }

    public int? IsDeleted { get; set; }
}
