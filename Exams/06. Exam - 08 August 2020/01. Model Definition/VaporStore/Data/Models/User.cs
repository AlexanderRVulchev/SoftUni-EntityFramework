using System.ComponentModel.DataAnnotations;

namespace VaporStore.Data.Models
{
    public class User
    {
        public User()
        {
            Cards = new HashSet<Card>();
        }

        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Username { get; set; }
        
        [Required]
        public string FullName { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        [Required]
        public int Age { get; set; }
        
        public ICollection<Card> Cards { get; set; }
    }
}
