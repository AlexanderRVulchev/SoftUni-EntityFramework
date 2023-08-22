using System.ComponentModel.DataAnnotations;

namespace SoftJail.DataProcessor.ImportDto
{
    public class ImportPrisonerDto
    {
        [Required, MinLength(3), MaxLength(20)]
        public string FullName { get; set; }

        [Required, RegularExpression(@"^The [A-Z][a-z]+$")]
        public string Nickname { get; set; }

        [Required, Range(18, 65)]
        public int Age { get; set; }

        [Required]
        public string IncarcerationDate { get; set; }

        public string? ReleaseDate { get; set; }

        public decimal? Bail { get; set; }

        public int? CellId { get; set; }

        public ImportMailDto[] Mails { get; set; }
    }

    public class ImportMailDto
    {        
        [Required]
        public string Description { get; set; }
        
        [Required]
        public string Sender { get; set; }
        
        [Required, RegularExpression(@"^[a-zA-Z0-9 ]+str\.$")]
        public string Address { get; set; }
    }
}
