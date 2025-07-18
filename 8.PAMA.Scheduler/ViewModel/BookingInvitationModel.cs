namespace _8.PAMA.Scheduler.ViewModel
{
    public class BookingInvitationModel
    {
        public string Id { get; set; }
        public string BookingId { get; set; } = null!;
        public string Nik { get; set; } = null!;
        public string CardNumber { get; set; } = null!;
        public string Name { get; set; } = string.Empty;
        public string StaffNo { get; set; } = string.Empty;
        public bool ExecuteDoorAccess { get; set; }
        public string? PinRoom { get; set; }
    }
}