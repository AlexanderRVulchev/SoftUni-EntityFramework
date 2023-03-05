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

            string jsonOutput = GetCarsWithTheirListOfParts(context);
            string outputFilePath = @"../../../Results/cars-and-parts.json";
            File.WriteAllText(outputFilePath, jsonOutput);            
        }

        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var carsAndParts = context.Cars
                .Select(c => new
                {
                    car = new
                    {
                        c.Make,
                        c.Model,
                        c.TraveledDistance
                    },
                    parts = c.PartsCars
                        .Select(p => new
                        {
                            p.Part.Name,
                            Price = $"{p.Part.Price:f2}"
                        })                        
                })
                .ToArray();

            return JsonConvert.SerializeObject(carsAndParts, Formatting.Indented);
        }
    }
}