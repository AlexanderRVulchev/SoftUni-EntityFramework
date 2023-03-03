using CarDealer.Data;
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
            string output = ImportParts(context, partsJsonInput);

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
    }
}