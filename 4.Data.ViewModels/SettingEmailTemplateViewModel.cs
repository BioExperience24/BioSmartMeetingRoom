using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace _4.Data.ViewModels
{
    public class SettingEmailTemplateViewModel : BaseViewModel
    {
        public int? IsEnabled { get; set; }
        public string? Type { get; set; }
        public string? TitleOfText { get; set; }
        public string? ToText { get; set; }
        public string? TitleAgendaText { get; set; }
        public string? DateText { get; set; }
        public string? Room { get; set; }
        public string? DetailLocation { get; set; }
        public string? GreetingText { get; set; }
        public string? ContentText { get; set; }
        public string? AttendanceText { get; set; }
        public string? AttendanceNoText { get; set; }
        public string? CloseText { get; set; }
        public string? SupportText { get; set; }
        public string? FootText { get; set; }
        public string? Link { get; set; }
        public string? MapLinkText { get; set; }
    }

    public class SettingEmailTemplateVMResponse
    {
        [JsonPropertyName("error")]
        public string? Error { get; set; }

        [JsonPropertyName("data")]
        public List<SettingEmailTemplateVMProp>? Data { get; set; }
    }

    public class SettingEmailTemplateVMProp
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("is_enabled")]
        public int? IsEnabled { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("title_of_text")]
        public string? TitleOfText { get; set; }

        [JsonPropertyName("to_text")]
        public string? ToText { get; set; }

        [JsonPropertyName("title_agenda_text")]
        public string? TitleAgendaText { get; set; }

        [JsonPropertyName("date_text")]
        public string? DateText { get; set; }

        [JsonPropertyName("room")]
        public string? Room { get; set; }

        [JsonPropertyName("detail_location")]
        public string? DetailLocation { get; set; }

        [JsonPropertyName("greeting_text")]
        public string? GreetingText { get; set; }

        [JsonPropertyName("content_text")]
        public string? ContentText { get; set; }

        [JsonPropertyName("attendance_text")]
        public string? AttendanceText { get; set; }

        [JsonPropertyName("attendance_no_text")]
        public string? AttendanceNoText { get; set; }

        [JsonPropertyName("close_text")]
        public string? CloseText { get; set; }

        [JsonPropertyName("support_text")]
        public string? SupportText { get; set; }

        [JsonPropertyName("foot_text")]
        public string? FootText { get; set; }

        [JsonPropertyName("link")]
        public string? Link { get; set; }

        [JsonPropertyName("map_link_text")]
        public string? MapLinkText { get; set; }
    }

    public class SettingEmailTemplateCreateViewModelFR
    {
        [BindProperty(Name = "is_enabled")]
        public int? IsEnabled { get; set; }

        [BindProperty(Name = "type")]
        public string? Type { get; set; }

        [BindProperty(Name = "title_of_text")]
        public string? TitleOfText { get; set; }

        [BindProperty(Name = "to_text")]
        public string? ToText { get; set; }

        [BindProperty(Name = "title_agenda_text")]
        public string? TitleAgendaText { get; set; }

        [BindProperty(Name = "date_text")]
        public string? DateText { get; set; }

        [BindProperty(Name = "room")]
        public string? Room { get; set; }

        [BindProperty(Name = "detail_location")]
        public string? DetailLocation { get; set; }

        [BindProperty(Name = "greeting_text")]
        public string? GreetingText { get; set; }

        [BindProperty(Name = "content_text")]
        public string? ContentText { get; set; }

        [BindProperty(Name = "attendance_text")]
        public string? AttendanceText { get; set; }

        [BindProperty(Name = "attendance_no_text")]
        public string? AttendanceNoText { get; set; }

        [BindProperty(Name = "close_text")]
        public string? CloseText { get; set; }

        [BindProperty(Name = "support_text")]
        public string? SupportText { get; set; }

        [BindProperty(Name = "foot_text")]
        public string? FootText { get; set; }

        [BindProperty(Name = "link")]
        public string? Link { get; set; }

        [BindProperty(Name = "map_link_text")]
        public string? MapLinkText { get; set; }
    }

    public class SettingEmailTemplateUpdateViewModelFR : SettingEmailTemplateCreateViewModelFR
    {
        //[BindProperty(Name = "id", SupportsGet = false)]
        //public string Id { get; set; }

        //[BindProperty(Name = "updated_at", SupportsGet = false)]
        //public DateTime? UpdatedAt { get; set; }

        [BindProperty(Name = "is_deleted", SupportsGet = false)]
        public bool IsDeleted { get; set; }
    }

    public class SettingEmailTemplateDeleteViewModelFR
    {
        [BindProperty(Name = "id")]
        public string Id { get; set; }

        [BindProperty(Name = "name")]
        public string Name { get; set; } = string.Empty;
    }
}