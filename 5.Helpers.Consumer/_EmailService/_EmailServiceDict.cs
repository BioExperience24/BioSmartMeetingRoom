namespace _5.Helpers.Consumer._EmailService
{
    public static class _EmailServiceDict
    {
        public static readonly Dictionary<string, string> EmailTemplate = new Dictionary<string, string>
        {
            { _EmailServiceTypeOfMail.INVITATION, "undangan-meeting.html" },
            { _EmailServiceTypeOfMail.RESCHEDULE, "reschedule-meeting.html" },
            { _EmailServiceTypeOfMail.CANCELLATION, "pembatalan-meeting.html" },
            { _EmailServiceTypeOfMail.ATTENDANCE, "attend-not-attend.html" },
            { _EmailServiceTypeOfMail.ORDER, "list-order.html" },
            { _EmailServiceTypeOfMail.ORDER_RECURRING, "list-order-recurring.html" },
        };
    }
}