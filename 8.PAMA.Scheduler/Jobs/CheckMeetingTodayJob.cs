using _8.PAMA.Scheduler.Interface;
using Quartz;

namespace _8.PAMA.Scheduler.Jobs
{
    public class CheckMeetingTodayJob : IJob
    {
        private readonly ISchedulerService _scheduler;

        public CheckMeetingTodayJob(ISchedulerService scheduler)
        {
            _scheduler = scheduler;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _scheduler.CheckMeetingTodayAccessAsync();
        }
    }
}