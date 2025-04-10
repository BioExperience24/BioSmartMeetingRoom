using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkerAPICaller.Repository;

[Table("manual_config", Schema = "dbo")]
public class ManualConfig
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public long Id { get; set; }

    [MaxLength(255)]
    [Column("config_name")]
    public string? ConfigName { get; set; }

    [MaxLength(255)]
    [Column("config_unit")]
    public string? ConfigUnit { get; set; }

    [Column("config_value")]
    public string? ConfigValue { get; set; }

    [Column("note")]
    public string? Note { get; set; }

    [Column("is_deleted")]
    public bool? IsDeleted { get; set; }

    [Column("created_date")]
    public DateTimeOffset? CreatedDate { get; set; }

    [MaxLength(255)]
    [Column("created_by")]
    public string? CreatedBy { get; set; }

    [Column("modified_date")]
    public DateTimeOffset? ModifiedDate { get; set; }

    [MaxLength(255)]
    [Column("modified_by")]
    public string? ModifiedBy { get; set; }
}
