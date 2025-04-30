namespace _5.Helpers.Consumer._EmailService;

public class _EmailServiceVMAttendance
{
    public string ParticipantName { get; set; } = string.Empty;
    public string MeetingDate { get; set; } = string.Empty;
    public string MeetingTime { get; set; } = string.Empty;
    public string MeetingLocation { get; set; } = string.Empty;
    public string MeetingRoom { get; set; } = string.Empty;
    public string MeetingBuilding { get; set; } = string.Empty;
    public string MeetingHost { get; set; } = string.Empty;
    public string MeetingAgenda { get; set; } = string.Empty;
    public string AttendanceStatus { get; set; } = string.Empty;
    public string SincerelyName { get; set; } = string.Empty;
}

public class _EmailServiceVMInvitation
{
    public string Kepada { get; set; } = string.Empty;
    public string Tanggal { get; set; } = string.Empty;
    public string Waktu { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Room { get; set; } = string.Empty;
    public string LinkMap { get; set; } = string.Empty;
    public string Organizer { get; set; } = string.Empty;
    public string Agenda { get; set; } = string.Empty;
    public string Pin { get; set; } = string.Empty;
    public string HormatKami { get; set; } = string.Empty;
    
    public string Qrtattendance { get; set; } = string.Empty;
    public string TanggalText { get; set; } = string.Empty;
    public string LocationText { get; set; } = string.Empty;
    public string RoomText { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string AgendaText { get; set; } = string.Empty;
}