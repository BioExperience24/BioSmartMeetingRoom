using _8.PAMA.Scheduler.Interface;
using Quartz;

namespace _8.PAMA.Scheduler.Jobs
{
    public class CheckReminderMeetingUnusedJob : IJob
    {
        private readonly ISchedulerService _scheduler;

        public CheckReminderMeetingUnusedJob(ISchedulerService scheduler)
        {
            _scheduler = scheduler;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _scheduler.CheckReminderMeetingUnusedAsync();
        }
    }
}