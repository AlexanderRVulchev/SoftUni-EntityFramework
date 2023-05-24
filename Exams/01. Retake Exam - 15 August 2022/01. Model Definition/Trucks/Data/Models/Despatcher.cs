using System.ComponentModel.DataAnnotations;

namespace Trucks.Data.Models
{
    public class Despatcher
    {        
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public string Position { get; set; }
        
        public virtual ICollection<Truck> Trucks { get; set; }
    }
}
