using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Artillery.DataProcessor.ImportDto
{
    [XmlType("Country")]
    public class ImportCountryDto
    {
        [XmlElement("CountryName")]
        [MinLength(4)]
        [MaxLength(60)]
        public string CountryName { get; set; }

        [XmlElement("ArmySize")]        
        [Range(50_000, 10_000_000)]
        public int ArmySize { get; set; }
    }
}
