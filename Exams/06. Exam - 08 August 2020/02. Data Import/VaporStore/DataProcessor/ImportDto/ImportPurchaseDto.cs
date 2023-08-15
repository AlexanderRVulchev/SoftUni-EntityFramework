using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace VaporStore.DataProcessor.ImportDto
{
    [XmlType("Purchase")]
    public class ImportPurchaseDto
    {
        [Required]
        [XmlAttribute("title")]
        public string GameTitle { get; set; }
        
        [Required]
        [XmlElement("Type")]
        public string Type { get; set; }
        
        [Required]
        [RegularExpression(@"^[A-Z\d]{4}-[A-Z\d]{4}-[A-Z\d]{4}$")]
        [XmlElement("Key")]
        public string ProductKey { get; set; }
        
        [Required]
        [XmlElement("Card")]
        public string CardNumber { get; set; }
        
        [Required]
        [XmlElement("Date")]
        public string Date { get; set; }
    }
}
