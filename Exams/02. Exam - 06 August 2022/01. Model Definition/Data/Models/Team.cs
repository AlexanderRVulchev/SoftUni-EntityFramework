using System.ComponentModel.DataAnnotations;

namespace Footballers.Data.Models
{
    public class Team
    {
        public Team()
        {
            TeamsFootballers = new HashSet<TeamFootballer>();
        }

        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Nationality { get; set; }
        
        [Required]
        public int Trophies { get; set; }

        public ICollection<TeamFootballer> TeamsFootballers { get; set; }        
    }
}
