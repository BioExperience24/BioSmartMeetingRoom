using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkerAPICaller.Repository;

[Table("scheduler_endpoint", Schema = "dbo")]
public class SchedulerEndpoint
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public long Id { get; set; }

    [Column("identifier")]
    public string? Identifier { get; set; }

    [Column("status")]
    public string? Status { get; set; }

    [Column("master_id")]
    public long MasterId { get; set; } // Relasi ke SchedulerMaster

    [Column("path")]
    public string? Path { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Column("next_run")]
    public DateTimeOffset NextRun { get; set; }

    [Column("interval_looping")]
    public long IntervalLooping { get; set; } // Dalam detik

    [Column("last_run")]
    public DateTimeOffset? LastRun { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; } = false;

    [Column("created_date")]
    public DateTimeOffset? CreatedDate { get; set; }

    [Column("created_by"), MaxLength(255)]
    public string? CreatedBy { get; set; }

    [Column("modified_date")]
    public DateTimeOffset? ModifiedDate { get; set; }

    [Column("modified_by"), MaxLength(255)]
    public string? ModifiedBy { get; set; }

    // Properti navigasi ke SchedulerMaster
    [ForeignKey("MasterId")]
    public virtual SchedulerMaster Master { get; set; } = null!;
}
