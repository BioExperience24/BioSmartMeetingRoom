using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace _4.Data.ViewModels
{
    public class SettingPantryConfigViewModel : BaseViewModelId
    {
        public int Status { get; set; }
        public int PantryExpired { get; set; }
        public int MaxOrderQty { get; set; }
        public int BeforeOrderMeeting { get; set; }
    }

    public class SettingPantryConfigVMResponse
    {
        [JsonPropertyName("error")]
        public string? Error { get; set; }

        [JsonPropertyName("data")]
        public List<SettingPantryConfigVMProp>? Data { get; set; }
    }

    public class SettingPantryConfigVMProp
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("pantry_expired")]
        public int PantryExpired { get; set; }

        [JsonPropertyName("max_order_qty")]
        public int MaxOrderQty { get; set; }

        [JsonPropertyName("before_order_meeting")]
        public int BeforeOrderMeeting { get; set; }
    }

    public class SettingPantryConfigCreateViewModelFR
    {
        [BindProperty(Name = "status")]
        public int Status { get; set; }

        [BindProperty(Name = "pantry_expired")]
        public int PantryExpired { get; set; }

        [BindProperty(Name = "max_order_qty")]
        public int MaxOrderQty { get; set; }

        [BindProperty(Name = "before_order_meeting")]
        public int BeforeOrderMeeting { get; set; }
    }

    public class SettingPantryConfigUpdateViewModelFR : SettingPantryConfigCreateViewModelFR
    {
        [BindProperty(Name = "id", SupportsGet = false)]
        public int Id { get; set; }

        [BindProperty(Name = "updated_at", SupportsGet = false)]
        public DateTime? UpdatedAt { get; set; }

        [BindProperty(Name = "is_deleted", SupportsGet = false)]
        public bool IsDeleted { get; set; }
    }

    public class SettingPantryConfigDeleteViewModelFR
    {
        [BindProperty(Name = "id")]
        public int Id { get; set; }

        [BindProperty(Name = "name")]
        public string Name { get; set; } = string.Empty;
    }
}