using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftJail.Data.Models
{
    public class OfficerPrisoner
    {
        [Required, ForeignKey(nameof(Prisoner))]
        public int PrisonerId { get; set; }
        public Prisoner Prisoner { get; set; }
        
        [Required, ForeignKey(nameof(Officer))]
        public int OfficerId { get; set; }
        public Officer Officer { get; set; }
    }
}
