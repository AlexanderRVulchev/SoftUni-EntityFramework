using System.ComponentModel.DataAnnotations;

namespace SoftJail.Data.Models
{
    public class Department
    {
        public Department()
        {
            Cells = new HashSet<Cell>();
        }
        
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public ICollection<Cell> Cells { get; set; }
    }
}
