namespace _8.PAMA.Scheduler.ViewModel
{
    public class MeetingAccess
    {
        public string? BookingId { get; set; }
        public string? Title { get; set; }
        public DateOnly Date { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string? RoomName { get; set; }
        public string? AccessGroup { get; set; }
        public string? IpController { get; set; }
        public int Channel { get; set; }
        public string? Type { get; set; }
    }
}
