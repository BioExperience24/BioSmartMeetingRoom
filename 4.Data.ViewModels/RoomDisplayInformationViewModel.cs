using System.Text.Json.Serialization;

namespace _4.Data.ViewModels
{

    public class RoomDisplayInformationViewModel
    {
        // [JsonPropertyName("display_id")]
        [JsonIgnore]
        public long DisplayId { get; set; }

        [JsonPropertyName("room_id")]
        public string RoomId { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string RoomName { get; set; } = string.Empty;

        [JsonPropertyName("distance")]
        public int Distance { get; set; }

        [JsonPropertyName("icon")]
        public string Icon { get; set; } = string.Empty;

        [JsonPropertyName("work_day")]
        public List<string>? WorkDay { get; set; } = new List<string>();

        [JsonPropertyName("work_time")]
        public string? WorkTime { get; set; }

        [JsonPropertyName("work_start")]
        public string? WorkStart { get; set; }

        [JsonPropertyName("work_end")]
        public string? WorkEnd { get; set; }
    }

    public class RoomDisplayInformationMeetingViewModel : RoomDisplayInformationViewModel
    {
        [JsonPropertyName("room_name")]
        public new string RoomName { get; set; } = string.Empty;

        [JsonPropertyName("capacity")]
        public int? Capacity { get; set; }

        [JsonPropertyName("booking_id")]
        public string BookingId { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("date")]
        public DateOnly Date { get; set; }

        [JsonPropertyName("start")]
        public DateTime Start { get; set; }

        [JsonPropertyName("end")]
        public DateTime End { get; set; }

        [JsonPropertyName("end_meeting")]
        public TimeSpan EndMeeting { get; set; }

        [JsonPropertyName("is_alive")]
        public int IsAlive { get; set; }

        [JsonPropertyName("is_approve")]
        public int? IsApprove { get; set; }

        [JsonPropertyName("is_expired")]
        public int? IsExpired { get; set; }

        [JsonPropertyName("is_canceled")]
        public int? IsCanceled { get; set; }

        [JsonPropertyName("is_private")]
        public int? IsPrivate { get; set; }

        [JsonPropertyName("organizer_name")]
        public string OrganizerName { get; set; } = string.Empty;

        [JsonPropertyName("pin_room")]
        public string? PinRoom { get; set; } = string.Empty;

        [JsonPropertyName("building_name")]
        public string? BuildingName { get; set; } = string.Empty;

        [JsonPropertyName("floor_name")]
        public string? FloorName { get; set; } = string.Empty;

        [JsonPropertyName("work_day")]
        public List<string>? WorkDay { get; set; } = new List<string>();

        [JsonPropertyName("work_time")]
        public string? WorkTime { get; set; }

        [JsonPropertyName("work_start")]
        public string? WorkStart { get; set; }

        [JsonPropertyName("work_end")]
        public string? WorkEnd { get; set; }
    }

    public class RoomAvailableDisplayInformationMeetingViewModel
    {
        [JsonPropertyName("serial")]
        public new string Serial { get; set; } = string.Empty;

        [JsonPropertyName("total_room")]
        public int? TotalRoom { get; set; }

        [JsonPropertyName("available_room")]
        public int? AvailableRoom { get; set; }

        [JsonPropertyName("rooms")]
        public List<string>? Rooms { get; set; }
    }
}

