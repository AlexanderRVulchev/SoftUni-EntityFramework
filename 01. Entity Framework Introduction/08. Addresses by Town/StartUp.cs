using System;
using System.Linq;
using System.Text;

namespace SoftUni
{
    using Data;
    using Models;
    
    public class StartUp
    {
        static void Main()
        {
            var context = new SoftUniContext();
            string output = GetAddressesByTown(context);
            Console.WriteLine(output);
        }

        public static string GetAddressesByTown(SoftUniContext context)
        {
            string[] addressesInfo = context.Addresses
                .OrderByDescending(a => a.Employees.Count)
                .ThenBy(a => a.Town.Name)
                .ThenBy(a => a.AddressText)
                .Take(10)
                .Select(a => $"{a.AddressText}, {a.Town.Name} - {a.Employees.Count} employees")
                .ToArray();

            return string.Join(Environment.NewLine, addressesInfo);
        }
    }
}
