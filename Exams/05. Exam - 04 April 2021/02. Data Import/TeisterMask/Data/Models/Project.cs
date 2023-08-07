using System.ComponentModel.DataAnnotations;

namespace TeisterMask.Data.Models
{
    public class Project
    {
        public Project()
        {
            Tasks = new HashSet<Task>();
        }

        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public DateTime OpenDate { get; set; }
        
        public DateTime? DueDate { get; set; }
        
        public ICollection<Task> Tasks { get; set; }
    }
}
