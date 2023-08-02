using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Theatre.DataProcessor.ImportDto
{
    [XmlType("Play")]
    public class ImportPlayDto
    {
        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        [XmlElement("Title")]
        public string Title { get; set; }
        
        [Required]
        [XmlElement("Duration")]
        public string Duration { get; set; }
        
        [Required]
        [Range(0.0, 10.0)]
        [XmlElement("Raiting")]
        public float Rating { get; set; }
        
        [Required]
        [XmlElement("Genre")]
        public string Genre { get; set; }
        
        [Required]
        [MaxLength(700)]
        [XmlElement("Description")]
        public string Description { get; set; }
        
        [Required]        
        [MinLength(4)]
        [MaxLength(30)]
        [XmlElement("Screenwriter")]
        public string Screenwriter { get; set; }
    }
}
