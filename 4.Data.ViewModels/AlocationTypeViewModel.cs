

using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace _4.Data.ViewModels
{
    public class AlocationTypeViewModel : BaseViewModel
    {
        
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("invoice_status")]
        public int? InvoiceStatus { get; set; }

        [JsonPropertyName("created_by")]
        public string? CreatedBy { get; set; }

        [JsonPropertyName("updated_by")]
        public string? UpdatedBy { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonPropertyName("is_permanent")]
        public int IsPermanent { get; set; }
    }

    public class AlocationTypeVMDefaultFR {
        [BindProperty(Name="id", SupportsGet = false)]
        public string Id { get; set; } = string.Empty;

        [BindProperty(Name="name", SupportsGet = false)]
        public string Name { get; set; } = string.Empty;
    }

    public class AlocationTypeVMUpdateFR : AlocationTypeVMDefaultFR {
        [BindProperty(Name = "invoice_status", SupportsGet = false)]
        public int InvoiceStatus { get; set; } = 0;
    }
}