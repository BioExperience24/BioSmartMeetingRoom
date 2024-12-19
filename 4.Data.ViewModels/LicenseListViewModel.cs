using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace _4.Data.ViewModels
{
    public class LicenseListViewModel : BaseViewModel
    {
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? Module { get; set; }
        public string? ExpiredAt { get; set; }
        public int? IsLifetime { get; set; }
        public string Status { get; set; } = null!;
        public int? Qty { get; set; }
        public string? PlatformSerial { get; set; }
    }

    public class LicenseListVMResponse
    {
        [JsonPropertyName("error")]
        public string? Error { get; set; }

        [JsonPropertyName("data")]
        public List<LicenseListVMProp>? Data { get; set; }
    }

    public class LicenseListVMProp
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("module")]
        public string? Module { get; set; }

        [JsonPropertyName("expired_at")]
        public string? ExpiredAt { get; set; }

        [JsonPropertyName("is_lifetime")]
        public int? IsLifetime { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; } = null!;

        [JsonPropertyName("qty")]
        public int? Qty { get; set; }

        [JsonPropertyName("platform_serial")]
        public string? PlatformSerial { get; set; }
    }

    public class LicenseListCreateViewModelFR
    {
        [BindProperty(Name = "name")]
        public string? Name { get; set; }

        [BindProperty(Name = "type")]
        public string? Type { get; set; }

        [BindProperty(Name = "module")]
        public string? Module { get; set; }

        [BindProperty(Name = "expired_at")]
        public string? ExpiredAt { get; set; }

        [BindProperty(Name = "is_lifetime")]
        public int? IsLifetime { get; set; }

        [BindProperty(Name = "status")]
        public string Status { get; set; } = string.Empty;

        [BindProperty(Name = "qty")]
        public int? Qty { get; set; }

        [BindProperty(Name = "platform_serial")]
        public string? PlatformSerial { get; set; }
    }

    public class LicenseListUpdateViewModelFR : LicenseListCreateViewModelFR
    {
        [BindProperty(Name = "updated_at", SupportsGet = false)]
        public DateTime? UpdatedAt { get; set; }

        [BindProperty(Name = "is_deleted", SupportsGet = false)]
        public short IsDeleted { get; set; }
    }

    public class LicenseListDeleteViewModelFR
    {
        [BindProperty(Name = "id")]
        public int Id { get; set; }

        [BindProperty(Name = "name")]
        public string? Name { get; set; }
    }
}