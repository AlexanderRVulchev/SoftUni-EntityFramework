using SoftUni.Data;
using System;
using System.Linq;
using System.Text;

namespace SoftUni
{
    public class StartUp
    {
        static void Main()
        {
            var context = new SoftUniContext();
            string output = GetEmployeesWithSalaryOver50000(context);
            Console.WriteLine(output);
        }

        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            var employeesInfo = context.Employees
                .Where(x => x.Salary > 50000)
                .Select(x => new { x.FirstName, x.Salary })
                .OrderBy(x => x.FirstName)
                .ToList();

            StringBuilder sb = new StringBuilder();
            foreach (var e in employeesInfo)
            {
                sb.AppendLine($"{e.FirstName} - {e.Salary:f2}");
            }

            return sb.ToString().TrimEnd();
        }
    }

}
