namespace _8.PAMA.Scheduler.ViewModel
{
    public class BookingCheckExpiredMeeting
    {
        public string? BookingId { get; set; }
        public string? Title { get; set; }
        public DateOnly? Date { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
    }
}