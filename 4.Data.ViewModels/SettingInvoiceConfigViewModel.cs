using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace _4.Data.ViewModels
{
    public class SettingInvoiceConfigViewModel : BaseViewModel
    {
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
    }

    public class SettingInvoiceConfigVMResponse
    {
        [JsonPropertyName("error")]
        public string? Error { get; set; }

        [JsonPropertyName("data")]
        public List<SettingInvoiceConfigVMProp>? Data { get; set; }
    }

    public class SettingInvoiceConfigVMProp
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("date_format")]
        public string? DateFormat { get; set; }

        [JsonPropertyName("date_text")]
        public string? DateText { get; set; }

        [JsonPropertyName("to_text")]
        public string? ToText { get; set; }

        [JsonPropertyName("up_text")]
        public string? UpText { get; set; }

        [JsonPropertyName("no_inv_text")]
        public string? NoInvText { get; set; }

        [JsonPropertyName("no_profit_text")]
        public string? NoProfitText { get; set; }

        [JsonPropertyName("description_text")]
        public string? DescriptionText { get; set; }

        [JsonPropertyName("amount_text")]
        public string? AmountText { get; set; }

        [JsonPropertyName("content_text")]
        public string? ContentText { get; set; }

        [JsonPropertyName("amount_bill_text")]
        public string? AmountBillText { get; set; }

        [JsonPropertyName("tax_text")]
        public string? TaxText { get; set; }

        [JsonPropertyName("tax_amount")]
        public int? TaxAmount { get; set; }

        [JsonPropertyName("total_text")]
        public string? TotalText { get; set; }

        [JsonPropertyName("footer_text")]
        public string? FooterText { get; set; }

        [JsonPropertyName("footer2_text")]
        public string? Footer2Text { get; set; }

        [JsonPropertyName("footer3_text")]
        public string? Footer3Text { get; set; }

        [JsonPropertyName("is_deleted")]
        public bool IsDeleted { get; set; }
    }

    public class SettingInvoiceConfigCreateViewModelFR
    {
        [BindProperty(Name = "date_format")]
        public string? DateFormat { get; set; }

        [BindProperty(Name = "date_text")]
        public string? DateText { get; set; }

        [BindProperty(Name = "to_text")]
        public string? ToText { get; set; }

        [BindProperty(Name = "up_text")]
        public string? UpText { get; set; }

        [BindProperty(Name = "no_inv_text")]
        public string? NoInvText { get; set; }

        [BindProperty(Name = "no_profit_text")]
        public string? NoProfitText { get; set; }

        [BindProperty(Name = "description_text")]
        public string? DescriptionText { get; set; }

        [BindProperty(Name = "amount_text")]
        public string? AmountText { get; set; }

        [BindProperty(Name = "content_text")]
        public string? ContentText { get; set; }

        [BindProperty(Name = "amount_bill_text")]
        public string? AmountBillText { get; set; }

        [BindProperty(Name = "tax_text")]
        public string? TaxText { get; set; }

        [BindProperty(Name = "tax_amount")]
        public int? TaxAmount { get; set; }

        [BindProperty(Name = "total_text")]
        public string? TotalText { get; set; }

        [BindProperty(Name = "footer_text")]
        public string? FooterText { get; set; }

        [BindProperty(Name = "footer2_text")]
        public string? Footer2Text { get; set; }

        [BindProperty(Name = "footer3_text")]
        public string? Footer3Text { get; set; }
    }

    public class SettingInvoiceConfigUpdateViewModelFR : SettingInvoiceConfigCreateViewModelFR
    {
        [BindProperty(Name = "id", SupportsGet = false)]
        public string Id { get; set; }

        [BindProperty(Name = "updated_at", SupportsGet = false)]
        public DateTime? UpdatedAt { get; set; }

        [BindProperty(Name = "is_deleted", SupportsGet = false)]
        public bool IsDeleted { get; set; }
    }

    public class SettingInvoiceConfigDeleteViewModelFR
    {
        [BindProperty(Name = "id")]
        public string Id { get; set; }

        [BindProperty(Name = "name")]
        public string Name { get; set; } = string.Empty;
    }
}