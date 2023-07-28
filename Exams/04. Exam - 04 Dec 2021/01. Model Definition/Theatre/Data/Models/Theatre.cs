using System.ComponentModel.DataAnnotations;

namespace Theatre.Data.Models
{
    public class Theatre
    {
        public Theatre()
        {
            Tickets = new HashSet<Ticket>();
        }
        
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public sbyte NumberOfHalls { get; set; }
        
        [Required]
        public string Director { get; set; }
        
        public ICollection<Ticket> Tickets { get; set; }
    }
}