using System.ComponentModel.DataAnnotations;
using Trucks.Data.Models;

namespace Trucks.DataProcessor.ImportDto
{
    public class ImportJsonClient
    {
        [Required, MinLength(3), MaxLength(40)]
        public string Name { get; set; }

        [Required, MinLength(2), MaxLength(40)]
        public string Nationality { get; set; }

        [Required]
        public string Type { get; set; }

        public int[] Trucks { get; set; }
    }
}
