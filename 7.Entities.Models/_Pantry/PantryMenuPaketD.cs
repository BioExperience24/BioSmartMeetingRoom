using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace _7.Entities.Models;

public partial class PantryMenuPaketD
{
    //[Key] // Jika ini adalah primary key
    //[DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-generate oleh database
    //public int? Generate;

    public required long MenuId { get; set; }
    public virtual PantryDetail? PantryDetail { get; set; }

    public required string PackageId { get; set; }
    public virtual PantryMenuPaket? Package { get; set; }

    public int IsDeleted { get; set; }
}
