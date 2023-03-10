using CarDealer.Data;
using CarDealer.DTOs.Import;
using CarDealer.Models;
using System.Xml.Serialization;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main()
        {
            var context = new CarDealerContext();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            string suppliersFilePath = @"../../../Datasets/suppliers.xml";
            string suppliersXml = File.ReadAllText(suppliersFilePath);
            ImportSuppliers(context, suppliersXml);

            string partsFilePath = @"../../../Datasets/parts.xml";
            string partsXml = File.ReadAllText(partsFilePath);
            ImportParts(context, partsXml);

            string carsFilePath = @"../../../Datasets/cars.xml";
            string carsXml = File.ReadAllText(carsFilePath);
            ImportCars(context, carsXml);

            string customersFilePath = @"../../../Datasets/customers.xml";
            string customersXml = File.ReadAllText(customersFilePath);
            ImportCustomers(context, customersXml);

            string salesFilePath = @"../../../Datasets/sales.xml";
            string salesXml = File.ReadAllText(salesFilePath);
            string output = ImportSales(context, salesXml);

            Console.WriteLine(output);
        }

        // This is a generic XML deserializer
        private static T Deserializer<T>(string inputXml, string rootName)
        {
            XmlRootAttribute root = new XmlRootAttribute(rootName);
            XmlSerializer serializer = new XmlSerializer(typeof(T), root);

            using StringReader reader = new StringReader(inputXml);

            T dtos = (T)serializer.Deserialize(reader);
            return dtos;
        }

        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            ImportSuppliersDto[] suppliersDtos = Deserializer<ImportSuppliersDto[]>(inputXml, "Suppliers");

            Supplier[] suppliers = suppliersDtos
                .Select(s => new Supplier()
                {
                    Name = s.Name,
                    IsImporter = s.IsImporter
                })
                .ToArray();

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Count()}";
        }

        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            ImportPartsDto[] partsDtos = Deserializer<ImportPartsDto[]>(inputXml, "Parts");

            int[] supplierIds = context.Suppliers.Select(s => s.Id).ToArray();

            Part[] parts = partsDtos
                .Where(p => supplierIds.Contains(p.SupplierId))
                .Select(p => new Part()
                {
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    SupplierId = p.SupplierId
                })
                .ToArray();

            context.Parts.AddRange(parts);
            context.SaveChanges();

            return $"Successfully imported {parts.Count()}";
        }

        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            ImportCarsDto[] carsDtos = Deserializer<ImportCarsDto[]>(inputXml, "Cars");

            List<Car> cars = new List<Car>();
            List<PartCar> partCars = new List<PartCar>();
            int[] allPartIds = context.Parts.Select(p => p.Id).ToArray();
            int carId = 1;

            foreach (var dto in carsDtos)
            {
                Car car = new Car()
                {
                    Make = dto.Make,
                    Model = dto.Model,
                    TraveledDistance = dto.TraveledDistance
                };

                cars.Add(car);

                foreach (int partId in dto.Parts
                    .Where(p => allPartIds.Contains(p.PartId))
                    .Select(p => p.PartId)
                    .Distinct())
                {
                    PartCar partCar = new PartCar()
                    {
                        CarId = carId,
                        PartId = partId
                    };
                    partCars.Add(partCar);
                }
                carId++;
            }

            context.Cars.AddRange(cars);            
            context.PartsCars.AddRange(partCars);
            context.SaveChanges();

            return $"Successfully imported {cars.Count}";
        }

        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            ImportCustomersDto[] customersDtos = Deserializer<ImportCustomersDto[]>(inputXml, "Customers");

            Customer[] customers = customersDtos.Select(c => new Customer()
            {
                Name = c.Name,
                BirthDate = c.BirthDate,
                IsYoungDriver = c.isYoungDriver
            })
            .ToArray();

            context.Customers.AddRange(customers);
            context.SaveChanges();

            return $"Successfully imported {customers.Count()}";
        }

        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            ImportSalesDto[] salesDtos = Deserializer<ImportSalesDto[]>(inputXml, "Sales");

            int[] allCarIds = context.Cars.Select(c => c.Id).ToArray();

            Sale[] sales = salesDtos
                .Where(s => allCarIds.Contains(s.CarId))
                .Select(s => new Sale()
                {
                    CarId = s.CarId,
                    CustomerId = s.CustomerId,
                    Discount = s.Discount
                })
                .ToArray();

            context.Sales.AddRange(sales);
            context.SaveChanges();

            return $"Successfully imported {sales.Count()}";
        }
    }
}