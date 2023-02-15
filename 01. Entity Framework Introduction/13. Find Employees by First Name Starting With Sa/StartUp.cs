using System;
using System.Linq;
using System.Text;


namespace SoftUni
{
    using Data;
    
    public class StartUp
    {
        static void Main()
        {
            var context = new SoftUniContext();
            string output = GetEmployeesByFirstNameStartingWithSa(context);
            Console.WriteLine(output);
        }

        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            string[] employeesInfoText = context.Employees
                .Where(e => e.FirstName.Substring(0, 2).ToLower() == "sa")
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .Select(e => $"{e.FirstName} {e.LastName} - {e.JobTitle} - (${e.Salary:f2})")
                .ToArray();

            return string.Join(Environment.NewLine, employeesInfoText);
        }
    }
}
