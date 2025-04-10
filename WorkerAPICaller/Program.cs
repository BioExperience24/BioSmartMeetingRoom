using Microsoft.EntityFrameworkCore;
using WorkerAPICaller;
using WorkerAPICaller.Repository;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
    {
        options.ServiceName = "Worker Service API Caller";
    })
    .ConfigureServices((hostContext, services) =>
    {
        // Ambil konfigurasi dari appsettings.json
        IConfiguration configuration = hostContext.Configuration;

        // Tambahkan DbContext dengan SQL Server
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddHostedService<Worker>(); // Pastikan Worker bisa akses DbContext
    })
    .Build();

await host.RunAsync();