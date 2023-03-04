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

            string jsonOutput = GetCarsFromMakeToyota(context);
            string outputFilePath = @"../../../Results/toyota-cars.json";
            File.WriteAllText(outputFilePath, jsonOutput);            
        }

        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            var carsFromMakeToyota = context.Cars
                .Where(c => c.Make == "Toyota")
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TraveledDistance)
                .Select(c => new 
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    TraveledDistance = c.TraveledDistance
                })
                .ToArray();

            return JsonConvert.SerializeObject(carsFromMakeToyota, Formatting.Indented);
        }
    }
}