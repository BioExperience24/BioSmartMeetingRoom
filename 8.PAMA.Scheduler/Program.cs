using Quartz;
using _6.Repositories.DB;
using Microsoft.EntityFrameworkCore;
using _8.PAMA.Scheduler.Configuration;
using _8.PAMA.Scheduler.Interface;

namespace _8.PAMA.SCHEDULER;
public class Program
{
    static async Task Main(string[] args)
    {
        var builder = Microsoft.Extensions.Hosting.Host.CreateApplicationBuilder(args);

        builder.Services.AddHttpClient();
        
        // connect to DB
        builder.Services.AddDbContext<MyDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);


        // DI
        builder.Services.AddServices();
        builder.Services.AddQuartzJobs();
        
        // Console.ReadLine();
        var app = builder.Build();

        if (args.Length > 0)
        {
            using var scope = app.Services.CreateScope();
            var scheduler = scope.ServiceProvider.GetRequiredService<ISchedulerService>();

            var jobName = args[0].ToLowerInvariant();
            Console.WriteLine($"[Runner] Running job: {jobName}");

            switch (jobName)
            {
                case "checkmeetingtoday":
                    var a = await scheduler.CheckMeetingTodayAccessAsync();
                    Console.WriteLine($"Result: {System.Text.Json.JsonSerializer.Serialize(a)}");
                    break;
                case "checkmeetingaftertoday":
                    await scheduler.CheckMeetingAfterTodayAccessAsync();
                    break;
                case "checkreminderbefore":
                    await scheduler.CheckReminderBeforeAsync();
                    break;
                case "checkremindermeetingunused":
                    await scheduler.CheckReminderMeetingUnusedAsync();
                    break;
                case "bookingservicesexpires":
                    await scheduler.BookingServicesExpiresAsync();
                    break;
                case "bookingservicesnotifbeforeend":
                    await scheduler.BookingServicesNotifBeforeEndAsync();
                    break;
                case "gettokenentrypass":
                    var b = await scheduler.GetTokenEntrypassAsync();
                    Console.WriteLine($"Result: {System.Text.Json.JsonSerializer.Serialize(b)}");
                    break;
                default:
                    Console.WriteLine($"[Runner] Unknown job: {jobName}");
                    break;
            }

            return; // exit setelah selesai eksekusi CLI
        }
        await app.RunAsync();
    }
}