using System.ComponentModel.DataAnnotations;

namespace _7.Entities.Models;

public class BaseLongEntity
{
    [Key]
    public long? Id { get; set; } = null!;
    public int? IsDeleted { get; set; }
}