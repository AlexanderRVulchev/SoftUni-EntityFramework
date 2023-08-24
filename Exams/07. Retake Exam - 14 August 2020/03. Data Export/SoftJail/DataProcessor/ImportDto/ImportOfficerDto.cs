using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace SoftJail.DataProcessor.ImportDto
{
    [XmlType("Officer")]
    public class ImportOfficerDto
    {        
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        [XmlElement("Name")]
        public string FullName { get; set; }
        
        [Required]
        [XmlElement("Money")]
        public decimal Salary { get; set; }
        
        [Required]
        [XmlElement("Position")]
        public string Position { get; set; }
        
        [Required]
        [XmlElement("Weapon")]
        public string Weapon { get; set; }
        
        [Required]
        [XmlElement("DepartmentId")]
        public int DepartmentId { get; set; }

        [XmlArray("Prisoners")]
        public ImportOfficerPrisonerDto[] Prisoners { get; set; }
    }

    [XmlType("Prisoner")]
    public class ImportOfficerPrisonerDto
    {
        [XmlAttribute("id")]
        public int PrisonerId { get; set; }        
    }
}
