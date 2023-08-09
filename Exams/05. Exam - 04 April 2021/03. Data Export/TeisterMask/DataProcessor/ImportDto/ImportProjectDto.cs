using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace TeisterMask.DataProcessor.ImportDto
{
    [XmlType("Project")]
    public class ImportProjectDto
    {
        [Required, XmlElement("Name"), MinLength(2), MaxLength(40)]
        public string Name { get; set; }

        [Required, XmlElement("OpenDate")]
        public string OpenDate { get; set; }

        [XmlElement("DueDate")]
        public string DueDate { get; set; }

        public ImportTaskDto[] Tasks { get; set; }
    }

    [XmlType("Task")]
    public class ImportTaskDto
    {
        [Required, XmlElement("Name"), MinLength(2), MaxLength(40)]
        public string Name { get; set; }

        [Required, XmlElement("OpenDate")]
        public string OpenDate { get; set; }

        [Required, XmlElement("DueDate")]
        public string DueDate { get; set; }

        [Required]
        public int ExecutionType { get; set; }

        [Required]
        public int LabelType { get; set; }
    }
}
