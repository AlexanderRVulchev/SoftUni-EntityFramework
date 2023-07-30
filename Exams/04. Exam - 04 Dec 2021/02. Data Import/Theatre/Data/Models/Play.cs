using System.ComponentModel.DataAnnotations;
using Theatre.Data.Models.Enums;

namespace Theatre.Data.Models
{
    public class Play
    {
        public Play()
        {
            Casts = new HashSet<Cast>();
            Tickets = new HashSet<Ticket>();
        }

        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        [Required]
        public TimeSpan Duration { get; set; }
        
        [Required]
        public float Rating { get; set; }
        
        [Required]
        public Genre Genre { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        [Required]
        public string Screenwriter { get; set; }
        
        public ICollection<Cast> Casts { get; set; }
        
        public ICollection<Ticket> Tickets { get; set; }
    }
}
