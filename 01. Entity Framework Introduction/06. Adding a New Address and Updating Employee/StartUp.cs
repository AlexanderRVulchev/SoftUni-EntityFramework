using SoftUni.Data;
using System;
using System.Linq;
using System.Text;

namespace SoftUni
{
    using Models;

    public class StartUp
    {
        static void Main()
        {
            var context = new SoftUniContext();
            string output = AddNewAddressToEmployee(context);
            Console.WriteLine(output);
        }

        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            string newAddressText = "Vitoshka 15";
            int newTownId = 4;

            var newAddressEntry = new Address()
            { 
                AddressText = newAddressText, 
                TownId = newTownId 
            };

            context.Addresses.Add(newAddressEntry);
            context.SaveChanges();

            var employee = context.Employees
                .Where(x => x.LastName == "Nakov")
                .FirstOrDefault();

            employee.Address = newAddressEntry;
            context.SaveChanges();

            string[] employeesAddressTexts = context.Employees
                .OrderByDescending(x => x.AddressId)
                .Take(10)
                .Select(x => (string)x.Address.AddressText)
                .ToArray();

            return String.Join(Environment.NewLine, employeesAddressTexts);
        }
    }

}
