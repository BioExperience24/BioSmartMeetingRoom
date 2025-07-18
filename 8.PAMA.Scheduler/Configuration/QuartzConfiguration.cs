using Quartz;
using _8.PAMA.Scheduler.Jobs;

namespace _8.PAMA.Scheduler.Configuration
{
    public static class QuartzConfiguration
    {
        public static void AddQuartzJobs(this IServiceCollection services)
        {
            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();

                // Job: Setiap 3 detik
                q.AddJob<CheckMeetingTodayJob>(opts => opts.WithIdentity("CheckMeetingTodayJob"));
                q.AddTrigger(t => t
                    .ForJob("CheckMeetingTodayJob")
                    .WithIdentity("CheckMeetingTodayTrigger")
                    .WithCronSchedule("*/3 * * * * ?"));

                // Job: Setiap 5 detik
                q.AddJob<CheckMeetingAfterTodayJob>(opts => opts.WithIdentity("CheckMeetingAfterTodayJob"));
                q.AddTrigger(t => t
                    .ForJob("CheckMeetingAfterTodayJob")
                    .WithIdentity("CheckMeetingAfterTodayTrigger")
                    .WithCronSchedule("*/5 * * * * ?"));

                // Job: Setiap 10 detik
                q.AddJob<CheckReminderBeforeJob>(opts => opts.WithIdentity("ReminderBeforeJob"));
                q.AddTrigger(t => t
                    .ForJob("ReminderBeforeJob")
                    .WithIdentity("ReminderBeforeTrigger")
                    .WithCronSchedule("*/10 * * * * ?"));

                q.AddJob<CheckReminderMeetingUnusedJob>(opts => opts.WithIdentity("ReminderMeetingUnusedJob"));
                q.AddTrigger(t => t
                    .ForJob("ReminderMeetingUnusedJob")
                    .WithIdentity("ReminderMeetingUnusedTrigger")
                    .WithCronSchedule("*/5 * * * * ?"));

                q.AddJob<BookingServicesNotifBeforeEndJob>(opts => opts.WithIdentity("BookingServicesNotifBeforeEndJob"));
                q.AddTrigger(t => t
                    .ForJob("BookingServicesNotifBeforeEndJob")
                    .WithIdentity("BookingServicesNotifBeforeEndTrigger")
                    .WithCronSchedule("*/5 * * * * ?"));

                q.AddJob<BookingServicesExpiresJob>(opts => opts.WithIdentity("BookingServicesExpiresJob"));
                q.AddTrigger(t => t
                    .ForJob("BookingServicesExpiresJob")
                    .WithIdentity("BookingServicesExpiresTrigger")
                    .WithCronSchedule("*/5 * * * * ?"));
            });

            services.AddQuartzHostedService(opt => opt.WaitForJobsToComplete = true);
        }
    }
}
