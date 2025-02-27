using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace _4.Data.ViewModels
{
    public class IntegrationViewModel
    {
        [JsonPropertyName("alarm_integration")]
        public string AlarmIntegration { get; set; } = string.Empty;

        [JsonPropertyName("m365_integration")]
        public string M365Integration { get; set; } = string.Empty;

        [JsonPropertyName("m365_devices")]
        public M365DevicesViewModel M365Devices { get; set; } = new();

        [JsonPropertyName("modules")]
        public Dictionary<string, object> Modules { get; set; } = new();
    }

    public class M365DevicesViewModel
    {
        [JsonPropertyName("url_callback")]
        public string UrlCallback { get; set; } = string.Empty;

        [JsonPropertyName("url_dis_m365")]
        public string UrlDisM365 { get; set; } = string.Empty;

        [JsonPropertyName("url_open_m365")]
        public string UrlOpenM365 { get; set; } = string.Empty;
    }

}
