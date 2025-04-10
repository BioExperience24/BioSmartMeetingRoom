
namespace _7.Entities.Models
{
    public class HelpItGa : BaseEntity
    {
        public DateTime Datetime { get; set; }
        public string BookingId { get; set; } = null!;
        public string RoomId { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ProblemFacility { get; set; } = null!;
        public string ProblemReason { get; set; } = null!;
        public DateTime? ProcessAt { get; set; }
        public DateTime? DoneAt { get; set; }
        public DateTime? RejectAt { get; set; }
        public string? ResponseDone { get; set; }
        public string? ResponseReject { get; set; }
        public int? TimeUntilDoneAt { get; set; }
        public string? ProcessBy { get; set; }
        public string? DoneBy { get; set; }
        public string? RejectBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }

    public class HelpItGaFilter : HelpItGa
    {
        public string? Search { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
    }
}