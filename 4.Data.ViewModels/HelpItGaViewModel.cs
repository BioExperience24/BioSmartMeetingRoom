using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace _4.Data.ViewModels
{
    public class HelpItGaViewModel : BaseViewModel
    {
        [JsonIgnore]
        public DateTime Datetime { get; set; }

        [JsonPropertyName("datetime")]
        public string DatetimeFormatted { get; set; } = string.Empty;

        [JsonPropertyName("booking_id")]
        public string BookingId { get; set; } = string.Empty;

        [JsonPropertyName("room_id")]
        public string RoomId { get; set; } = string.Empty;

        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("subject")]
        public string Subject { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("problem_facility")]
        public string ProblemFacility { get; set; } = string.Empty;

        [JsonPropertyName("problem_reason")]
        public string ProblemReason { get; set; } = string.Empty;
        
        [JsonIgnore]
        public DateTime ProcessAt { get; set; }

        [JsonPropertyName("process_at")]
        public string ProcessAtFormatted { get; set; } = string.Empty;
        
        [JsonIgnore]
        public DateTime DoneAt { get; set; }
        
        [JsonPropertyName("done_at")]
        public string DoneAtFormatted { get; set; } = string.Empty;

        [JsonIgnore]
        public DateTime RejectAt { get; set; }
        
        [JsonPropertyName("reject_at")]
        public string RejectAtFormatted { get; set; } = string.Empty;

        [JsonPropertyName("response_done")]
        public string ResponseDone { get; set; } = string.Empty;

        [JsonPropertyName("response_reject")]
        public string ResponseReject { get; set; } = string.Empty;

        [JsonPropertyName("time_until_done_at")]
        public int TimeUntilDoneAt { get; set; }

        [JsonPropertyName("process_by")]
        public string ProcessBy { get; set; } = string.Empty;

        [JsonPropertyName("done_by")]
        public string DoneBy { get; set; } = string.Empty;

        [JsonPropertyName("reject_by")]
        public string RejectBy { get; set; } = string.Empty;

        [JsonIgnore]
        public DateTime CreatedAt { get; set; }
        
        [JsonPropertyName("created_at")]
        public string CreatedAtFormatted { get; set; } = string.Empty;

        [JsonPropertyName("created_by")]
        public string CreatedBy { get; set; } = string.Empty;

        [JsonIgnore]
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public string UpdatedAtFormatted { get; set; } = string.Empty;

        [JsonPropertyName("updated_by")]
        public string UpdatedBy { get; set; } = string.Empty;
    }

    public class HelpItGaVMDataTable : DataTableViewModel
    {
        [FromQuery(Name = "type")]
        public string Type { get; set; } = string.Empty;
    }

    public class HelpItGaVMDataTableResponse : HelpItGaViewModel
    {
        [JsonPropertyName("no")]
        public int No { get; set; }

        [JsonPropertyName("room_name")]
        public string RoomName { get; set; } = string.Empty;

        [JsonPropertyName("booking_name")]
        public string BookingName { get; set; } = string.Empty;

        [JsonPropertyName("user_approved")]
        public string UserApproved { get; set; } = string.Empty;
    }

    public class HelpItGaVMChangeStatus
    {
        [FromForm(Name = "id")]
        public string Id { get; set; } = string.Empty;

        [FromForm(Name = "st")]
        public string Status { get; set; } = string.Empty;
        
        [FromForm(Name = "note")]
        public string? Note { get; set; }
    }

    public class HelpItGaVMRequest
    {
        [BindProperty(Name = "date")]
        [JsonPropertyName("date")]
        public DateOnly Date { get; set; }

        [BindProperty(Name = "time")]
        [JsonPropertyName("time")]
        public string Time { get; set; } = string.Empty;

        [BindProperty(Name = "type")]
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [BindProperty(Name = "subject")]
        [JsonPropertyName("subject")]
        public string Subject { get; set; } = string.Empty;

        [BindProperty(Name = "description")]
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [BindProperty(Name = "room_id")]
        [JsonPropertyName("room_id")]
        public string RoomId { get; set; } = string.Empty;

        [BindProperty(Name = "problem_facility")]
        [JsonPropertyName("problem_facility")]
        public string ProblemFacility { get; set; } = string.Empty;

        [BindProperty(Name = "problem_reason")]
        [JsonPropertyName("problem_reason")]
        public string ProblemReason { get; set; } = string.Empty;
    }

    public class HelpItGaVMFilterList
    {
        [BindProperty(Name = "room_id")]
        [JsonPropertyName("room_id")]
        public string? RoomId { get; set; }

        [BindProperty(Name = "filter_type")]
        [JsonPropertyName("filter_type")]
        public string? Type { get; set; }

        [BindProperty(Name = "filter_date1")]
        [JsonPropertyName("filter_date1")]
        public DateOnly? Start { get; set; }

        [BindProperty(Name = "filter_date2")]
        [JsonPropertyName("filter_date2")]
        public DateOnly? End { get; set; }
    }
}