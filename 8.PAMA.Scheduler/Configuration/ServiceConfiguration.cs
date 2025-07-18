using _8.PAMA.Scheduler.Interface;
using _8.PAMA.Scheduler.Repositories;
using _8.PAMA.Scheduler.Services;

namespace _8.PAMA.Scheduler.Configuration
{
    public static class ServiceConfiguration
    {
        public static void AddServices(this IServiceCollection services)
        {
            // Services
            services.AddScoped<ISchedulerService, SchedulerService>();
            services.AddScoped<EntrypassService>();

            services.AddScoped<BookingRepository>();
            services.AddScoped<QueryRepository>();
            services.AddScoped<SettingRuleBookingRepository>();
            services.AddScoped<NotificationConfigRepository>();
        }
    }
}
