using System.ComponentModel.DataAnnotations;

namespace Trucks.Data.Models
{
    public class Despatcher
    {
        public Despatcher()
        {
            Trucks = new HashSet<Truck>();
        }

        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public string Position { get; set; }
        
        public virtual ICollection<Truck> Trucks { get; set; }
    }
}
