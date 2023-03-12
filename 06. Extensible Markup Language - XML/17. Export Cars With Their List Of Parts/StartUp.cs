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

            string xmlOutput = GetCarsWithTheirListOfParts(context);
            File.WriteAllText(@"../../../Results/cars-and-parts.xml", xmlOutput);
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

        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            CarsWithPartsListDto[] carsPartsDtos = context.Cars
                .OrderByDescending(c => c.TraveledDistance)
                .ThenBy(c => c.Model)
                .Take(5)
                .Select(c => new CarsWithPartsListDto()
                {
                    Make = c.Make,
                    Model = c.Model,
                    TraveledDistance = c.TraveledDistance,
                    CarParts = c.PartsCars.Select(pc => new CarPartsDto()
                    {
                        Name = pc.Part.Name,
                        Price = pc.Part.Price
                    })
                    .OrderByDescending(p => p.Price)
                    .ToArray()
                })
                .ToArray();

            return Serializer<CarsWithPartsListDto[]>(carsPartsDtos, "cars");
        }
    }
}