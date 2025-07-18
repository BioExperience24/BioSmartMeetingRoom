using _8.PAMA.Scheduler.Interface;
using Quartz;

namespace _8.PAMA.Scheduler.Jobs
{
    public class CheckReminderBeforeJob : IJob
    {
        private readonly ISchedulerService _scheduler;

        public CheckReminderBeforeJob(ISchedulerService scheduler)
        {
            _scheduler = scheduler;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _scheduler.CheckReminderBeforeAsync();
        }
    }
}