using System;
using System.Text.Json.Serialization;

namespace _4.Data.ViewModels
{
    public class BookingInvitationViewModel : BaseLongViewModel
    {
        [JsonPropertyName("booking_id")]
        public string BookingId { get; set; } = null!;

        [JsonPropertyName("nik")]
        public string Nik { get; set; } = null!;

        [JsonPropertyName("internal")]
        public int Internal { get; set; }

        [JsonPropertyName("attendance_status")]
        public int AttendanceStatus { get; set; }

        [JsonPropertyName("attendance_reason")]
        public string? AttendanceReason { get; set; }

        [JsonPropertyName("execute_attendance")]
        public int? ExecuteAttendance { get; set; }

        [JsonPropertyName("execute_door_access")]
        public int ExecuteDoorAccess { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; } = null!;

        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("company")]
        public string Company { get; set; } = null!;

        [JsonPropertyName("position")]
        public string? Position { get; set; }

        [JsonPropertyName("is_pic")]
        public short IsPic { get; set; }

        [JsonPropertyName("is_vip")]
        public int? IsVip { get; set; }

        [JsonPropertyName("pin_room")]
        public string? PinRoom { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("created_by")]
        public string? CreatedBy { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyName("updated_by")]
        public string? UpdatedBy { get; set; }

        [JsonPropertyName("last_update_365")]
        public string? LastUpdate365 { get; set; }

        [JsonPropertyName("checkin")]
        public int? Checkin { get; set; }

        [JsonPropertyName("end_meeting")]
        public int? EndMeeting { get; set; }
    }
}