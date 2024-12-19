using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace _4.Data.ViewModels
{
    public class SettingSmtpViewModel : BaseViewModel
    {
        public int? SelectedEmail { get; set; }
        public int? IsEnabled { get; set; }
        public string? Name { get; set; }
        public string? TitleEmail { get; set; }
        public string Host { get; set; } = null!;
        public string User { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Port { get; set; }
        public short Secure { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class SettingSmtpVMResponse
    {
        [JsonPropertyName("error")]
        public string? Error { get; set; }

        [JsonPropertyName("data")]
        public List<SettingSmtpVMProp>? Data { get; set; }
    }

    public class SettingSmtpVMProp
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("selected_email")]
        public int? SelectedEmail { get; set; }

        [JsonPropertyName("is_enabled")]
        public int? IsEnabled { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("title_email")]
        public string? TitleEmail { get; set; }

        [JsonPropertyName("host")]
        public string Host { get; set; } = null!;

        [JsonPropertyName("user")]
        public string User { get; set; } = null!;

        [JsonPropertyName("password")]
        public string Password { get; set; } = null!;

        [JsonPropertyName("port")]
        public int Port { get; set; }

        [JsonPropertyName("secure")]
        public short Secure { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyName("is_deleted")]
        public short IsDeleted { get; set; }
    }

    public class SettingSmtpCreateViewModelFR
    {
        [BindProperty(Name = "selected_email")]
        public int? SelectedEmail { get; set; }

        [BindProperty(Name = "is_enabled")]
        public int? IsEnabled { get; set; }

        [BindProperty(Name = "name")]
        public string? Name { get; set; }

        [BindProperty(Name = "title_email")]
        public string? TitleEmail { get; set; }

        [BindProperty(Name = "host")]
        public string Host { get; set; } = string.Empty;

        [BindProperty(Name = "user")]
        public string User { get; set; } = string.Empty;

        [BindProperty(Name = "password")]
        public string Password { get; set; } = string.Empty;

        [BindProperty(Name = "port")]
        public int Port { get; set; }

        [BindProperty(Name = "secure")]
        public short Secure { get; set; }
    }

    public class SettingSmtpUpdateViewModelFR : SettingSmtpCreateViewModelFR
    {
        //[BindProperty(Name = "id", SupportsGet = false)]
        //public int Id { get; set; }

        [BindProperty(Name = "updated_at", SupportsGet = false)]
        public DateTime? UpdatedAt { get; set; }

        [BindProperty(Name = "is_deleted", SupportsGet = false)]
        public short IsDeleted { get; set; }
    }

    public class SettingSmtpDeleteViewModelFR
    {
        [BindProperty(Name = "id")]
        public int Id { get; set; }

        [BindProperty(Name = "name")]
        public string? Name { get; set; }
    }
}