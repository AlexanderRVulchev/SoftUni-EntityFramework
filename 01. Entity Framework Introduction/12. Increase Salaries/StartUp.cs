using System;
using System.Linq;
using System.Text;


namespace SoftUni
{
    using Data;
    using Microsoft.VisualBasic;

    public class StartUp
    {
        static void Main()
        {
            var context = new SoftUniContext();
            string output = IncreaseSalaries(context);
            Console.WriteLine(output);
        }

        public static string IncreaseSalaries(SoftUniContext context)
        {
            decimal salaryModifier = 1.12m;
            string[] departmentNames = new string[] { "Engineering", "Tool Design", "Marketing", "Information Services" };

            var employeesForSalaryIncrease = context.Employees
                .Where(e => departmentNames.Contains(e.Department.Name))
                .ToArray();
                        
            foreach (var e in employeesForSalaryIncrease)
            {
                e.Salary *= salaryModifier;
            }

            context.SaveChanges();

            string[] emplyeesInfoText = context.Employees
                .Where(e => departmentNames.Contains(e.Department.Name))
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .Select(e => $"{e.FirstName} {e.LastName} (${e.Salary:f2})")
                .ToArray();

            return string.Join(Environment.NewLine, emplyeesInfoText);
        }
    }
}
