// ReSharper disable InconsistentNaming

namespace TeisterMask.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    using Data;
    using System.Xml.Serialization;
    using TeisterMask.DataProcessor.ImportDto;
    using System.Globalization;
    using System.Text;
    using TeisterMask.Data.Models;
    using TeisterMask.Data.Models.Enums;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedProject
            = "Successfully imported project - {0} with {1} tasks.";

        private const string SuccessfullyImportedEmployee
            = "Successfully imported employee - {0} with {1} tasks.";

        public static string ImportProjects(TeisterMaskContext context, string xmlString)
        {
            ImportProjectDto[] projectDtos = Deserialize<ImportProjectDto[]>(xmlString, "Projects");
            StringBuilder sb = new StringBuilder();
            List<Project> projects = new List<Project>();

            foreach (var projectDto in projectDtos)
            {
                bool isProjectOpenDateValid = DateTime.TryParseExact(projectDto.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime validProjectOpenDate);
                                
                bool isProjectDueDateValid = DateTime.TryParseExact(projectDto.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedProjectDueDate);
                
                if (!IsValid(projectDto) 
                    || !isProjectOpenDateValid 
                    || (!string.IsNullOrWhiteSpace(projectDto.DueDate) && !isProjectDueDateValid))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                DateTime? validProjectDueDate = string.IsNullOrWhiteSpace(projectDto.DueDate)
                    ? null
                    : parsedProjectDueDate;

                Project project = new Project()
                {
                    Name = projectDto.Name,
                    OpenDate = validProjectOpenDate,
                    DueDate = validProjectDueDate
                };

                List<Task> tasks = new List<Task>();
                foreach (var taskDto in projectDto.Tasks)
                {
                    bool isTaskOpenDateValid = DateTime.TryParseExact(taskDto.OpenDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime validTaskOpenDate);
                    bool isTaskDueDateValid = DateTime.TryParseExact(taskDto.DueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime validTaskDueDate);

                    if (!IsValid(taskDto) 
                        || !isTaskOpenDateValid 
                        || !isTaskDueDateValid 
                        || validTaskOpenDate < validProjectOpenDate
                        || (validProjectDueDate != null && validTaskDueDate > validProjectDueDate))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Task task = new Task()
                    {
                        Name = taskDto.Name,
                        OpenDate = validTaskOpenDate,
                        DueDate = validTaskDueDate,
                        ExecutionType = (ExecutionType)taskDto.ExecutionType,
                        LabelType = (LabelType)taskDto.LabelType,
                        ProjectId = project.Id
                    };
                    tasks.Add(task);
                }
                project.Tasks = tasks;
                projects.Add(project);
                sb.AppendLine(String.Format(SuccessfullyImportedProject, project.Name, project.Tasks.Count));
            }
            context.Projects.AddRange(projects);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportEmployees(TeisterMaskContext context, string jsonString)
        {
            ImportEmployeeDto[] employeeDtos = JsonConvert.DeserializeObject<ImportEmployeeDto[]>(jsonString);
            StringBuilder sb = new StringBuilder();
            List<Employee> employees = new List<Employee>();

            foreach (var employeeDto in employeeDtos)
            {
                if (!IsValid(employeeDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Employee employee = new Employee()
                {
                    Username = employeeDto.Username,
                    Email = employeeDto.Email,
                    Phone = employeeDto.Phone,
                };

                int[] validTaskIds = context.Tasks.Select(t => t.Id).ToArray();
                
                foreach (int taskId in employeeDto.Tasks.Distinct())
                {
                    if (!validTaskIds.Contains(taskId))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    employee.EmployeesTasks.Add(new EmployeeTask()
                    {
                        EmployeeId = employee.Id,
                        TaskId = taskId
                    });
                }
                employees.Add(employee);
                sb.AppendLine(String.Format(SuccessfullyImportedEmployee, employee.Username, employee.EmployeesTasks.Count));
            }
            context.Employees.AddRange(employees);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        private static T Deserialize<T>(string inputXml, string rootName)
        {
            XmlRootAttribute root = new XmlRootAttribute(rootName);
            XmlSerializer serializer = new XmlSerializer(typeof(T), root);

            using StringReader reader = new StringReader(inputXml);

            T dtos = (T)serializer.Deserialize(reader);
            return dtos;
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}