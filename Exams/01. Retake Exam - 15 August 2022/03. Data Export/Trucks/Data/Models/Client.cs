using System.ComponentModel.DataAnnotations;

namespace Trucks.Data.Models
{
    public class Client
    {
        public Client()
        {
            ClientsTrucks = new HashSet<ClientTruck>();
        }

        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required, MaxLength(40)]
        public string Nationality { get; set; }
        
        [Required]
        public string Type { get; set; }
        
        public virtual ICollection<ClientTruck> ClientsTrucks { get; set; }
    }
}
