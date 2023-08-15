using System.ComponentModel.DataAnnotations;

namespace VaporStore.DataProcessor.ImportDto
{
    public class ImportUserDto
    {
        [Required, RegularExpression(@"^[A-Z][a-z]+ [A-Z][a-z]+$")]
        public string FullName { get; set; }

        [Required, MinLength(3), MaxLength(20)]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Range(3, 103)]
        public int Age { get; set; }

        [Required]
        public ImportCardDto[] Cards { get; set; }
    }

    public class ImportCardDto
    {        
        [Required, RegularExpression(@"^\d{4} \d{4} \d{4} \d{4}$")]
        public string Number { get; set; }
        
        [Required, RegularExpression(@"^\d{3}$")]
        public string Cvc { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
