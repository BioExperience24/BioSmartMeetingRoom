
namespace _3.BusinessLogic.Services.EmailService
{
    public interface IEmailService
    {
        Task SendMailAttendanceConfirmation(string bookingId, string nik, int attendanceStatus);
        Task SendMailInvitation(string bookingId, List<string>? emails = null);
        Task SendMailInvitationRecurring(string recurringId, List<string>? emails = null);
        Task SendMailReschedule(string bookingId);
        Task SendMailCancellation(string bookingId);
        Task SendMailCancellationRecurring(string recurringId);
        Task SendMailNotifApprovalOrder(string bookingId, string pantryTransaksiId);
        Task SendMailNotifApprovalOrderRecurring(string recurringId);
    }
}