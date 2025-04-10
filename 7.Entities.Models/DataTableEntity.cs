
using System.ComponentModel.DataAnnotations.Schema;

namespace _7.Entities.Models
{
    public class DataTableEntity<E>
    {
        [NotMapped]
        public IEnumerable<E>? Collections { get; set; }
        
        [NotMapped]
        public int RecordsTotal { get; set; }
        
        [NotMapped]
        public int RecordsFiltered { get; set; }
    }
}