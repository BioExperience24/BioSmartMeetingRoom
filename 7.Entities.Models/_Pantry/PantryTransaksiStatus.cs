using System.ComponentModel.DataAnnotations;

namespace _7.Entities.Models;

public partial class PantryTransaksiStatus
{
    [Key]
    public long Id { get; set; }

    public string Name { get; set; } = null!;
}
