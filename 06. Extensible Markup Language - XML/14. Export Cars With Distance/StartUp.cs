using CarDealer.Data;
using CarDealer.DTOs.Export;
using System.Text;
using System.Xml.Serialization;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main()
        {
            var context = new CarDealerContext();

            string xmlOutput = GetCarsWithDistance(context);            
            File.WriteAllText(@"../../../Results/cars.xml", xmlOutput);
        }

        // this is a generic XML Serializer
        private static string Serializer<T>(T dataTransferObjects, string xmlRootAttributeName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T), new XmlRootAttribute(xmlRootAttributeName));

            StringBuilder sb = new StringBuilder();
            using var write = new StringWriter(sb);

            XmlSerializerNamespaces xmlNamespaces = new XmlSerializerNamespaces();
            xmlNamespaces.Add(string.Empty, string.Empty);

            serializer.Serialize(write, dataTransferObjects, xmlNamespaces);

            return sb.ToString();
        }

        public static string GetCarsWithDistance(CarDealerContext context)
        {
            CarsWithDistanceDto[] carsDtos = context.Cars
                .Where(c => c.TraveledDistance > 2_000_000)
                .OrderBy(c => c.Make)
                .ThenBy(c => c.Model)
                .Take(10)
                .Select(c => new CarsWithDistanceDto()
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TraveledDistance,
                })
                .ToArray();

            return Serializer<CarsWithDistanceDto[]>(carsDtos, "cars");
        }
    }
}