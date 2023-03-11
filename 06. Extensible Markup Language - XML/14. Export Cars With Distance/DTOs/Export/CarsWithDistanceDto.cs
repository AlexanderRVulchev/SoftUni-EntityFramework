using System.Xml.Serialization;

namespace CarDealer.DTOs.Export
{
    [XmlType("car")]
    public class CarsWithDistanceDto
    {        
        [XmlElement("make")]
        public string Make { get; set; }

        [XmlElement("model")]
        public string Model { get; set; }

        [XmlElement("traveled-distance")]
        public long TravelledDistance { get; set; }
    }
}
