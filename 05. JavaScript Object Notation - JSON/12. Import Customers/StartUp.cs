using CarDealer.Data;
using CarDealer.DTOs;
using CarDealer.Models;
using Newtonsoft.Json;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main()
        {
            CarDealerContext context = new CarDealerContext();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            string suppliersFilePath = @"../../../Datasets/suppliers.json";
            string suppliersJsonInput = File.ReadAllText(suppliersFilePath);
            ImportSuppliers(context, suppliersJsonInput);

            string partsFilePath = @"../../../Datasets/parts.json";
            string partsJsonInput = File.ReadAllText(partsFilePath);
            ImportParts(context, partsJsonInput);

            string carsFilePath = @"../../../Datasets/cars.json";
            string carsJsonInput = File.ReadAllText(carsFilePath);
            ImportCars(context, carsJsonInput);

            string customersFilePath = @"../../../Datasets/customers.json";
            string customersJsonInput = File.ReadAllText(customersFilePath);
            string output = ImportCustomers(context, customersJsonInput);

            Console.WriteLine(output);
        }

        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            var suppliers = JsonConvert.DeserializeObject<List<Supplier>>(inputJson);
            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();
            return $"Successfully imported {suppliers.Count}.";
        }

        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            int[] suppliersIds = context.Suppliers.Select(x => x.Id).ToArray();

            var parts = JsonConvert
                .DeserializeObject<List<Part>>(inputJson)
                .Where(p => suppliersIds.Contains(p.SupplierId))
                .ToList();

            context.Parts.AddRange(parts);
            context.SaveChanges();
            return $"Successfully imported {parts.Count}.";
        }

        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            var carsAndPartsDTO = JsonConvert.DeserializeObject<List<CarDTO>>(inputJson);
            
            List<PartCar> parts = new List<PartCar>();
            List<Car> cars = new List<Car>();
                        
            foreach (var dto in carsAndPartsDTO)
            {
                Car car = new Car()
                {
                    Make = dto.Make, 
                    Model = dto.Model,
                    TravelledDistance = dto.TravelledDistance
                };
                cars.Add(car);

                foreach (var part in dto.PartsId.Distinct())
                {
                    PartCar partCar = new PartCar()
                    {
                        Car = car,
                        PartId = part,
                    };
                    parts.Add(partCar);
                }                
            }

            context.Cars.AddRange(cars);
            context.PartsCars.AddRange(parts);
            context.SaveChanges();
            return $"Successfully imported {cars.Count}.";
        }

        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            var customers = JsonConvert.DeserializeObject<List<Customer>>(inputJson);

            context.Customers.AddRange(customers);
            context.SaveChanges();
            return $"Successfully imported {customers.Count}.";
        }
    }
}