using System.ComponentModel.DataAnnotations.Schema;

namespace _7.Entities.Models;

public partial class AlocationMatrix
{
    [Column("_generate")]
    public int Generate { get; set; }

    public string? AlocationId { get; set; }

    public string? Nik { get; set; }
}
