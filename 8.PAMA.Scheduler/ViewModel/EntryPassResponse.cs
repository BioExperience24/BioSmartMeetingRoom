namespace _8.PAMA.Scheduler.ViewModel
{
    public class EntryPassResponse
    {
        public string Status { get; set; } = null!;
        public string? Msg { get; set; }
        public string? Error { get; set; }
        public EntryPassData? Data { get; set; }
    }

    public class EntryPassData
    {
        public string Token { get; set; } = null!;
        public DateTimeOffset ExpiredIn { get; set; }
    }
}