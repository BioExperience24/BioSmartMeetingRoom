using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _8.PAMA.Scheduler.ViewModel
{
    public class BookingReminderBeforeStart
    {
        public string? BookingId { get; set; }
        public string? Title { get; set; }
        public DateOnly? Date { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public string? RoomId { get; set; }
        public string? RoomName { get; set; }
    }
}