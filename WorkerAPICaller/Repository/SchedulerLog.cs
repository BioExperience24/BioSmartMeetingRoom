using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkerAPICaller.Repository;

[Table("scheduler_log", Schema = "dbo")]
public class SchedulerLog
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public long Id { get; set; }

    //[Column("scheduler_id")]//gk perlu ada
    //public long SchedulerId { get; set; } // Relasi ke SchedulerMaster (tanpa FK)

    [Column("execution_time")]
    public DateTimeOffset ExecutionTime { get; set; } = DateTimeOffset.UtcNow;

    [Column("status"), MaxLength(50)]
    public string Status { get; set; } = "PENDING"; // Default Status

    [Column("message")]
    public string? Message { get; set; }

    [Column("created_date")]
    public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;
}
