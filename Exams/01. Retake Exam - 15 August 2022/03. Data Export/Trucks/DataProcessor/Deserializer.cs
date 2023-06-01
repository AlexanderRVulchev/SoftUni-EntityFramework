namespace Trucks.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using Trucks.Data.Models;
    using Trucks.Data.Models.Enums;
    using Trucks.DataProcessor.ImportDto;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedDespatcher
            = "Successfully imported despatcher - {0} with {1} trucks.";

        private const string SuccessfullyImportedClient
            = "Successfully imported client - {0} with {1} trucks.";

        public static string ImportDespatcher(TrucksContext context, string xmlString)
        {            
            ImportXmlDespatcher[] despatchersDtos = Deserialize<ImportXmlDespatcher[]>(xmlString, "Despatchers");
            StringBuilder sb = new StringBuilder();
            List<Despatcher> despatchers = new List<Despatcher>();

            foreach (ImportXmlDespatcher dto in despatchersDtos)
            {
                if (!IsValid(dto) || string.IsNullOrEmpty(dto.Position))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Despatcher despatcher = new Despatcher()
                {
                    Name = dto.Name,
                    Position = dto.Position
                };

                List<Truck> trucks = new List<Truck>();
                foreach (var truckDto in dto.Truck)
                {
                    if (!IsValid(truckDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Truck truck = new Truck()
                    {
                        CargoCapacity = truckDto.CargoCapacity,
                        CategoryType = (CategoryType)truckDto.CategoryType,
                        TankCapacity = truckDto.TankCapacity,
                        MakeType = (MakeType)truckDto.MakeType,
                        RegistrationNumber = truckDto.RegistrationNumber,
                        VinNumber = truckDto.VinNumber,                        
                        Despatcher = despatcher
                    };
                    trucks.Add(truck);
                }
                despatcher.Trucks = trucks;                

                despatcher.Trucks = trucks;
                despatchers.Add(despatcher);
                sb.AppendLine(String.Format(SuccessfullyImportedDespatcher, despatcher.Name, despatcher.Trucks.Count));                
            }
            context.AddRange(despatchers);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportClient(TrucksContext context, string jsonString)
        {
            ImportJsonClient[] clientDtos = JsonConvert.DeserializeObject<ImportJsonClient[]>(jsonString);
            StringBuilder sb = new StringBuilder();

            List<Client> clients = new List<Client>();
            List<ClientTruck> clientsTrucks = new List<ClientTruck>();

            foreach (var clientDto in clientDtos)
            {
                if (!IsValid(clientDto) || clientDto.Type == "usual")
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Client client = new Client()
                {
                    Name = clientDto.Name,
                    Nationality = clientDto.Nationality,
                    Type = clientDto.Type
                };
                                                

                foreach (int truckId in clientDto.Trucks.Distinct())
                {
                    Truck truck = context.Trucks.FirstOrDefault(t => t.Id == truckId);
                    if (truck == null)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    };
                    client.ClientsTrucks.Add(new ClientTruck()
                    {
                        Client = client,
                        Truck = truck
                    });
                }                
                clients.Add(client);
                sb.AppendLine(string.Format(SuccessfullyImportedClient, client.Name, client.ClientsTrucks.Count));
            }
            context.AddRange(clients);
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

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}