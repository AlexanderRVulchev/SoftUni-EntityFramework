using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Theatre.Data.Models;

namespace Theatre.DataProcessor.ImportDto
{
    [XmlType("Cast")]
    public class ImportCastDto
    {        
        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        [XmlElement("FullName")]
        public string FullName { get; set; }
        
        [Required]
        [XmlElement("IsMainCharacter")]
        public bool IsMainCharacter { get; set; }
        
        [Required]
        [RegularExpression(@"^\+44-\d{2}-\d{3}-\d{4}$")]
        [XmlElement("PhoneNumber")]
        public string PhoneNumber { get; set; }
        
        [Required]
        [XmlElement("PlayId")]
        public int PlayId { get; set; }
    }
}
