using System.ComponentModel.DataAnnotations;
using VaporStore.Data.Models;

namespace VaporStore.DataProcessor.ImportDto
{
    public class ImportGameDto
    {        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public decimal Price { get; set; }
        
        [Required]
        public string ReleaseDate { get; set; }
        
        [Required]
        public string Developer { get; set; }
        
        [Required]
        public string Genre { get; set; }

        [Required]
        public string[] Tags { get; set; }
    }
}
