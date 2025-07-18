using _8.PAMA.Scheduler.Interface;
using Quartz;

namespace _8.PAMA.Scheduler.ViewModel
{
    public class GenericFunctionJob : IJob
    {
        private readonly IServiceProvider _serviceProvider;

        public GenericFunctionJob(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var jobName = context.JobDetail.Key.Name;

            using var scope = _serviceProvider.CreateScope();
            var schedulerService = scope.ServiceProvider.GetRequiredService<ISchedulerService>();

            switch (jobName.ToLowerInvariant())
            {
                case "GetTokenEntrypass":
                    await schedulerService.GetTokenEntrypassAsync();
                    break;

                case "CheckMeetingTodayAccess":
                    await schedulerService.CheckMeetingTodayAccessAsync();
                    break;

                case "CheckMeetingAfterTodayAccess":
                    await schedulerService.CheckMeetingAfterTodayAccessAsync();
                    break;

                case "CheckReminderBefore":
                    await schedulerService.CheckReminderBeforeAsync();
                    break;

                case "CheckReminderMeetingUnused":
                    await schedulerService.CheckReminderMeetingUnusedAsync();
                    break;

                case "BookingServicesExpires":
                    await schedulerService.BookingServicesExpiresAsync();
                    break;

                case "BookingServicesNotifBeforeEnd":
                    await schedulerService.BookingServicesNotifBeforeEndAsync();
                    break;

                default:
                    throw new Exception($"Unknown job: {jobName}");
            }
        }
    }

}