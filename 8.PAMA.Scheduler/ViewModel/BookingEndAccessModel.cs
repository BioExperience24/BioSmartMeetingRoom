namespace _8.PAMA.Scheduler.ViewModel
{
    public class BookingEndAccessModel
    {
        public string BookingId { get; set; } = null!;
        public string? RoomId { get; set; } = null!;
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public DateOnly Date { get; set; }
        public int EndEarlyMeeting { get; set; }
        public DateTime? EarlyEndedAt { get; set; }
        public int? IsDeleted { get; set; }
        public int IsExpired { get; set; }
        public int IsCanceled { get; set; }
        public int? IsAccessTrigger { get; set; }

        public string RoomName { get; set; } = string.Empty;
        public string? AccessGroup { get; set; }
        public string? IpController { get; set; }
        public int? Channel { get; set; }
        public string Type { get; set; } = string.Empty;
    }

}