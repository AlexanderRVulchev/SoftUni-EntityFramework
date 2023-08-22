namespace SoftJail.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using SoftJail.Data.Models;
    using SoftJail.Data.Models.Enums;
    using SoftJail.DataProcessor.ImportDto;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid Data";

        private const string SuccessfullyImportedDepartment = "Imported {0} with {1} cells";

        private const string SuccessfullyImportedPrisoner = "Imported {0} {1} years old";

        private const string SuccessfullyImportedOfficer = "Imported {0} ({1} prisoners)";

        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {
            ImportDepartmentDto[] departmentDtos = JsonConvert.DeserializeObject<ImportDepartmentDto[]>(jsonString);
            StringBuilder sb = new StringBuilder();
            List<Department> departments = new List<Department>();

            foreach (var departmentDto in departmentDtos)
            {
                if (!IsValid(departmentDto) || !departmentDto.Cells.Any())
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Department department = new Department()
                {
                    Name = departmentDto.Name
                };

                List<Cell> cells = new List<Cell>();
                foreach (var cellDto in departmentDto.Cells)
                {
                    if (!IsValid(cellDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        cells = null;
                        break;
                    }

                    Cell cell = new Cell()
                    {
                        CellNumber = cellDto.CellNumber,
                        HasWindow = cellDto.HasWindow,
                        DepartmentId = department.Id
                    };

                    cells.Add(cell);
                }

                if (cells == null)
                {
                    continue;
                }

                department.Cells = cells;
                departments.Add(department);
                sb.AppendLine(String.Format(SuccessfullyImportedDepartment, department.Name, department.Cells.Count));
            }
            context.Departments.AddRange(departments);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {
            ImportPrisonerDto[] prisonerDtos = JsonConvert.DeserializeObject<ImportPrisonerDto[]>(jsonString);
            StringBuilder sb = new StringBuilder();
            List<Prisoner> prisoners = new List<Prisoner>();            

            foreach (var prisonerDto in prisonerDtos)
            {
                DateTime validIncarcerationDate;
                bool isIncarcerationDateValid = DateTime.TryParseExact(prisonerDto.IncarcerationDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out validIncarcerationDate);

                DateTime? validReleaseDate = null;
                bool isReleaseDateValid = true;
                if (prisonerDto.ReleaseDate != null)
                {
                    isReleaseDateValid = DateTime.TryParseExact(prisonerDto.ReleaseDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedReleaseDate);
                    validReleaseDate = parsedReleaseDate;                    
                }

                if (!IsValid(prisonerDto)
                    || (prisonerDto.Bail != null && prisonerDto.Bail < 0)                    
                    || !isIncarcerationDateValid
                    || !isReleaseDateValid)                    
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Prisoner prisoner = new Prisoner()
                {
                    Age = prisonerDto.Age,
                    FullName = prisonerDto.FullName,
                    Bail = prisonerDto.Bail,
                    IncarcerationDate = validIncarcerationDate,
                    ReleaseDate = validReleaseDate,
                    Nickname = prisonerDto.Nickname,
                    CellId = prisonerDto.CellId
                };

                List<Mail> mails = new List<Mail>();
                foreach (var mailDto in prisonerDto.Mails)
                {
                    if (!IsValid(mailDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        mails = null;
                        break;
                    }

                    Mail mail = new Mail()
                    {
                        Description = mailDto.Description,
                        Address = mailDto.Address,
                        Sender = mailDto.Sender,
                        PrisonerId = prisoner.Id
                    };

                    mails.Add(mail);
                }

                if (mails == null)
                {                     
                    continue; 
                }

                prisoner.Mails = mails.ToArray();
                prisoners.Add(prisoner);
                sb.AppendLine(String.Format(SuccessfullyImportedPrisoner, prisoner.FullName, prisoner.Age));
            }
            context.Prisoners.AddRange(prisoners);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {            
            ImportOfficerDto[] officerDtos = Deserialize<ImportOfficerDto[]>(xmlString, "Officers");
            StringBuilder sb = new StringBuilder();
            List<Officer> officers = new List<Officer>();
            
            foreach (var officerDto in officerDtos)
            {
                bool isSalaryValid = officerDto.Salary >= 0;            
                bool isPositionValid = Enum.TryParse<Position>(officerDto.Position, out Position validPosition);
                bool isWeaponValid = Enum.TryParse<Weapon>(officerDto.Weapon, out Weapon validWeapon);

                if (!IsValid(officerDto)
                    || !isSalaryValid                    
                    || !isPositionValid
                    || !isWeaponValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Officer officer = new Officer()
                {
                    FullName = officerDto.FullName,
                    Salary = officerDto.Salary,
                    Position = validPosition,
                    Weapon = validWeapon,
                    DepartmentId = officerDto.DepartmentId
                };

                foreach (int prisonerId in officerDto.Prisoners.Select(p => p.PrisonerId))
                {
                    officer.OfficerPrisoners.Add(new OfficerPrisoner()
                    {
                        Officer = officer,
                        PrisonerId = prisonerId
                    });
                }
                officers.Add(officer);
                sb.AppendLine(String.Format(SuccessfullyImportedOfficer, officer.FullName, officer.OfficerPrisoners.Count));
            }
            context.Officers.AddRange(officers);
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

        private static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationResult = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResult, true);
            return isValid;
        }
    }
}