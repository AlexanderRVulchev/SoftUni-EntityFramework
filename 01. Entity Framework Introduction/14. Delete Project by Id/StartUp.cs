using System;
using System.Linq;
using System.Text;


namespace SoftUni
{
    using Data;
    using SoftUni.Models;

    public class StartUp
    {
        static void Main()
        {
            var context = new SoftUniContext();
            string output = DeleteProjectById(context);
            Console.WriteLine(output);
        }

        public static string DeleteProjectById(SoftUniContext context)
        {
            var employeesProjectsToDelete = context.EmployeesProjects.Where(ep => ep.ProjectId == 2);
            context.EmployeesProjects.RemoveRange(employeesProjectsToDelete);

            var projectToDelete = context.Projects.Where(p => p.ProjectId == 2);
            context.Projects.RemoveRange(projectToDelete);

            context.SaveChanges();

            string[] projectsNames = context.Projects
                .Take(10)
                .Select(p => p.Name)
                .ToArray();

            return string.Join(Environment.NewLine, projectsNames);
        }
    }
}
