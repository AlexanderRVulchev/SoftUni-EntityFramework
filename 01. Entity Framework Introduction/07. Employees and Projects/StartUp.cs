﻿using System;
using System.Linq;
using System.Text;

namespace SoftUni
{
    using Data;
    using Models;
    using System.Globalization;

    public class StartUp
    {
        static void Main()
        {
            var context = new SoftUniContext();
            string output = GetEmployeesInPeriod(context);
            Console.WriteLine(output);
        }

        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            var EmployeeInfo = context.Employees
                .Where(e => e.EmployeesProjects
                    .Any(ep => ep.Project.StartDate.Year >= 2001 & ep.Project.StartDate.Year <= 2003))
                .Take(10)
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    ManagerFirstName = e.Manager.FirstName,
                    ManagerLastName = e.Manager.LastName,
                    Projects = e.EmployeesProjects.Select(ep => new
                    {
                        ProjectName = ep.Project.Name,
                        StartDate = ep.Project.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture),
                        EndDate = ep.Project.EndDate != null
                            ? ep.Project.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)
                            : "not finished"
                    })
                })
                .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var e in EmployeeInfo)
            {
                sb.AppendLine($"{e.FirstName} {e.LastName} - Manager: {e.ManagerFirstName} {e.ManagerLastName}");
                sb.AppendLine(String.Join(Environment.NewLine, e.Projects
                    .Select(p => $"--{p.ProjectName} - {p.StartDate} - {p.EndDate}")));
            }

            return sb.ToString().TrimEnd();
        }
    }

}
