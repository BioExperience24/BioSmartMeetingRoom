namespace _8.PAMA.Scheduler.ViewModel
{
    public class BookingReminder : BookingReminderBeforeStart
    {
        public int? IsConfigSettingEnable { get; set; }
        public int? IsEnableCheckin { get; set; }
        public int? IsReleaseCheckinTimeout { get; set; }
        public int? ConfigReleaseRoomCheckinTimeout { get; set; }
        public int? ConfigPermissionCheckin { get; set; }
        public int? ConfigPermissionEnd { get; set; }
        public int? IsEnableCheckinCount { get; set; }
        public int? ExtendedDuration { get; set; }
        public DateTime? EndDur { get; set; }
        public string? RoomLocation { get; set; }
        public int? IsConfigSettingEnableRoom { get; set; }
        public int? IsEnableCheckinRoom { get; set; }
        public int? IsReleaseCheckinTimeoutRoom { get; set; }
        public int? ConfigReleaseRoomCheckinTimeoutRoom { get; set; }
        public string? ConfigPermissionCheckinRoom { get; set; }
        public string? ConfigPermissionEndRoom { get; set; }
        public int? IsEnableCheckinCountRoom { get; set; }
        public int? IsRealeaseCheckinTimeoutRoom { get; set; }
    }
}