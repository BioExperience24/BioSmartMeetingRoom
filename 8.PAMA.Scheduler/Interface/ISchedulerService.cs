using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _8.PAMA.Scheduler.ViewModel;

namespace _8.PAMA.Scheduler.Interface
{
    public interface ISchedulerService
    {
        Task<bool> CheckReminderBeforeAsync();
        Task<bool> CheckMeetingTodayAccessAsync();
        Task<bool> CheckMeetingAfterTodayAccessAsync();
        Task<bool> CheckReminderMeetingUnusedAsync();
        Task<bool> BookingServicesExpiresAsync();
        Task<bool> BookingServicesNotifBeforeEndAsync();
        Task<EntryPassResponse> GetTokenEntrypassAsync();
    }
}