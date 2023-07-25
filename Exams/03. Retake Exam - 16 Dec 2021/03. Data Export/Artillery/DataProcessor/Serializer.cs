
namespace Artillery.DataProcessor
{
    using Artillery.Data;
    using Artillery.Data.Models;
    using Artillery.Data.Models.Enums;
    using Artillery.DataProcessor.ExportDto;
    using Newtonsoft.Json;
    using System.Text;
    using System.Xml.Serialization;

    public class Serializer
    {
        public static string ExportShells(ArtilleryContext context, double shellWeight)
        {
            var exportShells = context
                .Shells
                .Where(s => s.ShellWeight > shellWeight)
                .ToArray()
                .Select(s => new
                {
                    ShellWeight = s.ShellWeight,
                    Caliber = s.Caliber,
                    Guns = s.Guns.Select(g => new
                    {
                        GunType = g.GunType.ToString(),
                        GunWeight = g.GunWeight,
                        BarrelLength = g.BarrelLength,
                        Range = g.Range > 3000 ? "Long-range" : "Regular range"
                    })
                    .Where(g => g.GunType == "AntiAircraftGun")
                    .OrderByDescending(g => g.GunWeight)
                    .ToArray()
                })
                .OrderBy(s => s.ShellWeight)
                .ToArray();

            return JsonConvert.SerializeObject(exportShells, Formatting.Indented);
        }

        public static string ExportGuns(ArtilleryContext context, string manufacturer)
        {
            ExportGunDto[] exportGunsDtos = context
                .Guns
                .Where(g => g.Manufacturer.ManufacturerName == manufacturer)
                .ToArray()
                .Select(g => new ExportGunDto()
                {
                    GunType = g.GunType.ToString(),
                    BarrelLength = g.BarrelLength,
                    GunWeight = g.GunWeight,
                    Manufacturer = g.Manufacturer.ManufacturerName,
                    Range = g.Range,
                    Countries = g.CountriesGuns.Select(cg => new ExportCountryDto()
                        {
                            Country = cg.Country.CountryName,
                            ArmySize = cg.Country.ArmySize
                        })
                        .Where(c => c.ArmySize > 4_500_000)
                        .OrderBy(c => c.ArmySize)
                        .ToArray()
                })
                .OrderBy(g => g.BarrelLength)
                .ToArray();

            return Serialize<ExportGunDto[]>(exportGunsDtos, "Guns");
        }

        private static string Serialize<T>(T dataTransferObjects, string xmlRootAttributeName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T), new XmlRootAttribute(xmlRootAttributeName));

            StringBuilder sb = new StringBuilder();
            using var write = new StringWriter(sb);

            XmlSerializerNamespaces xmlNamespaces = new XmlSerializerNamespaces();
            xmlNamespaces.Add(string.Empty, string.Empty);

            serializer.Serialize(write, dataTransferObjects, xmlNamespaces);

            return sb.ToString();
        }
    }
}
