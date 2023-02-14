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
            string output = GetEmployee147(context);
            Console.WriteLine(output);
        }

        public static string GetEmployee147(SoftUniContext context)
        {
            var employee147Info = context.Employees
                .Where(e => e.EmployeeId == 147)
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    x.JobTitle,
                    Projects = x.EmployeesProjects.Select(p => new { p.Project.Name }).OrderBy(p => p.Name).ToArray()
                })
                .FirstOrDefault();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{employee147Info.FirstName} {employee147Info.LastName} - {employee147Info.JobTitle}");
            sb.Append(string.Join(Environment.NewLine, employee147Info.Projects.Select(p => p.Name)));

            return sb.ToString().TrimEnd();
        }
    }
}
