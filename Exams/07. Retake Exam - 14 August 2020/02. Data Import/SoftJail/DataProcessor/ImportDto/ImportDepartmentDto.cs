using System.ComponentModel.DataAnnotations;

namespace SoftJail.DataProcessor.ImportDto
{
    public class ImportDepartmentDto
    {
        [Required, MinLength(3), MaxLength(25)]
        public string Name { get; set; }

        public ImportCellDto[] Cells { get; set; }
    }

    public class ImportCellDto
    {
        [Required, Range(1, 1000)]
        public int CellNumber { get; set; }

        [Required]
        public bool HasWindow { get; set; }
    }
}
