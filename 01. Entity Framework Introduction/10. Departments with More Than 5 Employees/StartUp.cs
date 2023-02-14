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
            string output = GetDepartmentsWithMoreThan5Employees(context);
            Console.WriteLine(output);
        }

        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            var departmentsInfo = context.Departments
                .Where(d => d.Employees.Count > 5)
                .OrderBy(d => d.Employees.Count)
                .ThenBy(d => d.Name)
                .Select(d => new
                {
                    DepartmentName = d.Name,
                    ManagerName = d.Manager.FirstName + " " + d.Manager.LastName,
                    Employees = d.Employees
                        .OrderBy(e => e.FirstName)
                        .ThenBy(e => e.LastName)
                        .Select(e => new
                        {
                            EmployeeData = $"{e.FirstName} {e.LastName} - {e.JobTitle}"
                        })
                        .ToArray()
                })
                .ToArray();

            StringBuilder sb = new StringBuilder();
            foreach (var d in departmentsInfo)
            {
                sb.AppendLine($"{d.DepartmentName} - {d.ManagerName}");
                sb.Append(string.Join(Environment.NewLine, d.Employees.Select(e => e.EmployeeData)));
            }

            return sb.ToString().TrimEnd();
        }
    }
}
