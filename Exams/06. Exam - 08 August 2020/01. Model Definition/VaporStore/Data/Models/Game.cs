using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaporStore.Data.Models
{
    public class Game
    {
        public Game()
        {
            Purchases = new HashSet<Purchase>();
            GameTags = new HashSet<GameTag>();
        }

        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public decimal Price { get; set; }
        
        [Required]
        public DateTime ReleaseDate { get; set; }
        
        [Required, ForeignKey(nameof(Developer))]
        public int DeveloperId { get; set; }
        public Developer Developer { get; set; }

        [Required, ForeignKey(nameof(Genre))]
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        
        public ICollection<Purchase> Purchases { get; set; }
        public ICollection<GameTag> GameTags { get; set; }
    }
}
