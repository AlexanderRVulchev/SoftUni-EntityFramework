namespace Artillery.DataProcessor
{
    using Artillery.Data;
    using Artillery.Data.Models;
    using Artillery.Data.Models.Enums;
    using Artillery.DataProcessor.ImportDto;
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using System.Xml.Serialization;

    public class Deserializer
    {
        private const string ErrorMessage =
            "Invalid data.";
        private const string SuccessfulImportCountry =
            "Successfully import {0} with {1} army personnel.";
        private const string SuccessfulImportManufacturer =
            "Successfully import manufacturer {0} founded in {1}.";
        private const string SuccessfulImportShell =
            "Successfully import shell caliber #{0} weight {1} kg.";
        private const string SuccessfulImportGun =
            "Successfully import gun {0} with a total weight of {1} kg. and barrel length of {2} m.";

        public static string ImportCountries(ArtilleryContext context, string xmlString)
        {
            ImportCountryDto[] countryDtos = Deserialize<ImportCountryDto[]>(xmlString, "Countries");
            StringBuilder sb = new StringBuilder();
            List<Country> countries = new List<Country>();

            foreach (var countryDto in countryDtos)
            {
                if (!IsValid(countryDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Country country = new Country()
                {
                    CountryName = countryDto.CountryName,
                    ArmySize = countryDto.ArmySize,
                };

                countries.Add(country);
                sb.AppendLine(string.Format(SuccessfulImportCountry, country.CountryName, country.ArmySize));
            }
            context.Countries.AddRange(countries);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportManufacturers(ArtilleryContext context, string xmlString)
        {
            ImportManufacturerDto[] manufacturerDtos = Deserialize<ImportManufacturerDto[]>(xmlString, "Manufacturers");
            StringBuilder sb = new StringBuilder();
            List<Manufacturer> manufacturers = new List<Manufacturer>();

            foreach (var manufacturerDto in manufacturerDtos)
            {
                if (!IsValid(manufacturerDto) || manufacturers.Any(c => c.ManufacturerName == manufacturerDto.ManufacturerName))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Manufacturer manufacturer = new Manufacturer()
                {
                    ManufacturerName = manufacturerDto.ManufacturerName,
                    Founded = manufacturerDto.Founded
                };

                string[] tokens = manufacturer.Founded.Split(", ").ToArray();
                string townName = tokens[tokens.Length - 2];
                string countryName = tokens[tokens.Length - 1];

                manufacturers.Add(manufacturer);
                sb.AppendLine(String.Format(SuccessfulImportManufacturer, manufacturer.ManufacturerName, $"{townName}, {countryName}"));
            }
            context.Manufacturers.AddRange(manufacturers);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportShells(ArtilleryContext context, string xmlString)
        {
            ImportShellDto[] shellDtos = Deserialize<ImportShellDto[]>(xmlString, "Shells");
            StringBuilder sb = new StringBuilder();
            List<Shell> shells = new List<Shell>();

            foreach (var shellDto in shellDtos)
            {
                if (!IsValid(shellDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Shell shell = new Shell()
                {
                    ShellWeight = shellDto.ShellWeight,
                    Caliber = shellDto.Caliber
                };

                shells.Add(shell);
                sb.AppendLine(String.Format(SuccessfulImportShell, shell.Caliber, shell.ShellWeight));
            }
            context.Shells.AddRange(shells);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportGuns(ArtilleryContext context, string jsonString)
        {
            ImportGunDto[] gunDtos = JsonConvert.DeserializeObject<ImportGunDto[]>(jsonString);
            StringBuilder sb = new StringBuilder();
            List<Gun> guns = new List<Gun>();

            foreach (var gunDto in gunDtos)
            {                
                bool isGunTypeValid = Enum.TryParse<GunType>(gunDto.GunType, out GunType validGunType);
                
                if (!IsValid(gunDto) || !isGunTypeValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Gun gun = new Gun()
                {
                    BarrelLength = gunDto.BarrelLength,
                    GunType = validGunType,
                    GunWeight = gunDto.GunWeight,
                    NumberBuild = gunDto.NumberBuild,
                    Range = gunDto.Range,
                    ShellId = gunDto.ShellId,
                    ManufacturerId = gunDto.ManufacturerId
                };

                foreach (var countryDto in gunDto.Countries)
                {                    
                    gun.CountriesGuns.Add(new CountryGun()
                    {
                        CountryId = countryDto.Id,
                        Gun = gun
                    });
                }
                guns.Add(gun);
                sb.AppendLine(string.Format(SuccessfulImportGun, validGunType, gun.GunWeight, gun.BarrelLength));
            }
            context.Guns.AddRange(guns);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        private static T Deserialize<T>(string inputXml, string rootName)
        {
            XmlRootAttribute root = new XmlRootAttribute(rootName);
            XmlSerializer serializer = new XmlSerializer(typeof(T), root);

            using StringReader reader = new StringReader(inputXml);

            T dtos = (T)serializer.Deserialize(reader);
            return dtos;
        }

        private static bool IsValid(object obj)
        {
            var validator = new ValidationContext(obj);
            var validationRes = new List<ValidationResult>();

            var result = Validator.TryValidateObject(obj, validator, validationRes, true);
            return result;
        }
    }
}