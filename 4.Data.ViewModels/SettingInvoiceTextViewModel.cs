using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace _4.Data.ViewModels
{
    public class SettingInvoiceTextViewModel : BaseViewModel
    {
        public string? Name { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class SettingInvoiceTextVMResponse
    {
        [JsonPropertyName("error")]
        public string? Error { get; set; }

        [JsonPropertyName("data")]
        public List<SettingInvoiceTextVMProp>? Data { get; set; }
    }

    public class SettingInvoiceTextVMProp
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = null!;

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("created_by")]
        public string? CreatedBy { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonPropertyName("updated_by")]
        public string? UpdatedBy { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonPropertyName("is_deleted")]
        public bool IsDeleted { get; set; }
    }

    public class SettingInvoiceTextCreateViewModelFR
    {
        [BindProperty(Name = "name")]
        public string? Name { get; set; }

        [BindProperty(Name = "created_by")]
        public string? CreatedBy { get; set; }

        [BindProperty(Name = "created_at")]
        public DateTime? CreatedAt { get; set; }

        [BindProperty(Name = "updated_by")]
        public string? UpdatedBy { get; set; }

        [BindProperty(Name = "updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }

    public class SettingInvoiceTextUpdateViewModelFR : SettingInvoiceTextCreateViewModelFR
    {
        [BindProperty(Name = "id", SupportsGet = false)]
        public string Id { get; set; }

        [BindProperty(Name = "is_deleted", SupportsGet = false)]
        public bool IsDeleted { get; set; }
    }

    public class SettingInvoiceTextDeleteViewModelFR
    {
        [BindProperty(Name = "id")]
        public string Id { get; set; }

        [BindProperty(Name = "name")]
        public string Name { get; set; } = string.Empty;
    }
}