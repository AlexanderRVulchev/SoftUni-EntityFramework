using System.ComponentModel.DataAnnotations;

namespace Theatre.DataProcessor.ImportDto
{
    public class ImportTheaterDto
    {
        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [Range(1, 10)]
        public sbyte NumberOfHalls { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        public string Director { get; set; }

        public ImportTicketDto[] Tickets { get; set; }
    }

    public class ImportTicketDto
    {        
        [Required]
        [Range(1.00, 100.00)]
        public decimal Price { get; set; }
        
        [Required]
        [Range(1, 10)]
        public sbyte RowNumber { get; set; }

        [Required]        
        public int PlayId { get; set; }
    }
}
