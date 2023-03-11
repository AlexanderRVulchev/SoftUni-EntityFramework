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

            string xmlOutput = GetLocalSuppliers(context);
            File.WriteAllText(@"../../../Results/local-suppliers.xml", xmlOutput);
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

        public static string GetLocalSuppliers(CarDealerContext context)
        {
            LocalSuppliersDto[] localSuppliersDtos = context.Suppliers
                .Where(s => !s.IsImporter)
                .Select(s => new LocalSuppliersDto()
                {
                    Name = s.Name,
                    Id = s.Id,
                    PartsCount = s.Parts.Count()
                })                
                .ToArray();

            return Serializer<LocalSuppliersDto[]>(localSuppliersDtos, "suppliers");
        }

    }
}