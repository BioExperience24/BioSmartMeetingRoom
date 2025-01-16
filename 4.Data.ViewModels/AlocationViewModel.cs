using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace _4.Data.ViewModels
{
    public class AlocationViewModel : BaseViewModel
    {
        [JsonPropertyName("department_code")]
        public string DepartmentCode { get; set; } = null!;

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("invoice_type")]
        public int? InvoiceType { get; set; }

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

        [JsonPropertyName("show_in_invitation")]
        public int? ShowInInvitation { get; set; }

        [JsonPropertyName("type_name")]
        public string? TypeName { get; set; }

        [JsonPropertyName("invoice")]
        public int? Invoice { get; set; }
    }

    public class AlocationVMDefaultFR {
        [BindProperty(Name = "id", SupportsGet = false)]
        public string Id { get; set; } = string.Empty;

        [BindProperty(Name = "name", SupportsGet = false)]
        public string Name { get; set; } = string.Empty;
    }

    public class AlocationVMCreateFR : AlocationVMDefaultFR
    {
        [BindProperty(Name = "type", SupportsGet = false)]
        public string Type { get; set; } = string.Empty;
    }

    public class AlocationVMUpdateFR : AlocationVMCreateFR
    {
        [BindProperty(Name = "department_code", SupportsGet = false)]
        public string DepartmentCode { get; set; } = string.Empty;

        [BindProperty(Name = "invoice_status", SupportsGet = false)]
        public int? InvoiceStatus { get; set; }
    }
}