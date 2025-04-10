using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkerAPICaller.Repository;

[Table("scheduler_master", Schema = "dbo")]
public class SchedulerMaster
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public long Id { get; set; }

    [Column("base_url")]
    public string BaseUrl { get; set; } = string.Empty;

    [Column("username"), MaxLength(255)]
    public string? Username { get; set; }

    [Column("password"), MaxLength(255)]
    public string? Password { get; set; }

    [Column("login_path")]
    public string LoginPath { get; set; } = string.Empty;

    [Column("token_property")]
    public string TokenProperty { get; set; } = string.Empty;

    [Column("created_date")]
    public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;

    [Column("created_by"), MaxLength(255)]
    public string? CreatedBy { get; set; }

    [Column("modified_date")]
    public DateTimeOffset? ModifiedDate { get; set; }

    [Column("modified_by"), MaxLength(255)]
    public string? ModifiedBy { get; set; }

    [NotMapped]
    public string? Token { get; set; }

    // Relasi ke SchedulerEndpoint
    public virtual ICollection<SchedulerEndpoint> Endpoints { get; set; } = new List<SchedulerEndpoint>();
}
