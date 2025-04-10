using Microsoft.EntityFrameworkCore;

namespace WorkerAPICaller.Repository;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<ManualConfig> ManualConfigs { get; set; }
    public DbSet<SchedulerMaster> SchedulerMasters { get; set; }
    public DbSet<SchedulerEndpoint> SchedulerEndpoints { get; set; }
    public DbSet<SchedulerLog> SchedulerLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("dbo");

        // ManualConfig
        modelBuilder.Entity<ManualConfig>()
            .ToTable("manual_config")
            .HasKey(m => m.Id);

        // SchedulerMaster
        modelBuilder.Entity<SchedulerMaster>()
            .ToTable("scheduler_master")
            .HasKey(s => s.Id);

        // SchedulerEndpoint
        modelBuilder.Entity<SchedulerEndpoint>()
            .ToTable("scheduler_endpoint")
            .HasKey(e => e.Id);

        // SchedulerLog
        modelBuilder.Entity<SchedulerLog>()
            .ToTable("scheduler_log")
            .HasKey(l => l.Id);

    }
}
