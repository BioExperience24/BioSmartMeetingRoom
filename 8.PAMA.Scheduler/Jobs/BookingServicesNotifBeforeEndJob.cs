using _8.PAMA.Scheduler.Interface;
using Quartz;

namespace _8.PAMA.Scheduler.Jobs
{
    public class BookingServicesNotifBeforeEndJob : IJob
    {
        private readonly ISchedulerService _scheduler;

        public BookingServicesNotifBeforeEndJob(ISchedulerService scheduler)
        {
            _scheduler = scheduler;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _scheduler.BookingServicesNotifBeforeEndAsync();
        }
    }
}