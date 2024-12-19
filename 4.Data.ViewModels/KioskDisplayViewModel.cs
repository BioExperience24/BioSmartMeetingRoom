

using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace _4.Data.ViewModels
{
    public class KioskDisplayViewModel : BaseLongViewModel
    {
        [JsonPropertyName("display_serial")]
        public string DisplaySerial { get; set; } = string.Empty;

        [JsonPropertyName("display_type")]
        public string DisplayType { get; set; } = string.Empty;

        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; } = string.Empty;

        [JsonPropertyName("background")]
        public string Background { get; set; } = string.Empty;

        [JsonPropertyName("running_text")]
        public string RunningText { get; set; } = string.Empty;

        [JsonPropertyName("title_kiosk")]
        public string TitleKiosk { get; set; } = string.Empty;

        [JsonPropertyName("display_uuid")]
        public string DisplayUuid { get; set; } = string.Empty;

        [JsonPropertyName("display_hw_serial")]
        public string DisplayHwSerial { get; set; } = string.Empty;

        [JsonPropertyName("koordinate")]
        public string Koordinate { get; set; } = string.Empty;

        [JsonPropertyName("is_logged")]
        public short IsLogged { get; set; }

        [JsonPropertyName("last_logged")]
        public string LastLogged { get; set; } = string.Empty;

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }

    public class KioskDisplayVMCreateFR
    {
        [BindProperty(Name = "display_type")]
        public string DisplayType { get; set; } = string.Empty;

        [BindProperty(Name = "display_name")]
        public string DisplayName { get; set; } = string.Empty;
    }

    public class KioskDisplayVMUpdateFR : KioskDisplayVMCreateFR
    {
        [BindProperty(Name = "id")]
        public long Id { get; set; }
    }
}