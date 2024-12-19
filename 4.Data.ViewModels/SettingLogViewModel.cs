using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace _4.Data.ViewModels
{
    public class SettingLogConfigViewModel : BaseViewModel
    {
        public string? Text { get; set; }
    }

    public class SettingLogConfigVMResponse
    {
        [JsonPropertyName("error")]
        public string? Error { get; set; }

        [JsonPropertyName("data")]
        public List<SettingLogConfigVMProp>? Data { get; set; }
    }

    public class SettingLogConfigVMProp
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("text")]
        public string? Text { get; set; }
    }

    public class SettingLogConfigCreateViewModelFR
    {
        [BindProperty(Name = "text")]
        public string? Text { get; set; }
    }

    public class SettingLogConfigUpdateViewModelFR : SettingLogConfigCreateViewModelFR
    {
        [BindProperty(Name = "id", SupportsGet = false)]
        public string Id { get; set; }

        [BindProperty(Name = "updated_at", SupportsGet = false)]
        public DateTime? UpdatedAt { get; set; }

        [BindProperty(Name = "is_deleted", SupportsGet = false)]
        public bool IsDeleted { get; set; }
    }

    public class SettingLogConfigDeleteViewModelFR
    {
        [BindProperty(Name = "id")]
        public string Id { get; set; }

        [BindProperty(Name = "name")]
        public string Name { get; set; } = string.Empty;
    }
}