namespace P03_FootballBetting.Data.Models
{    
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Town
    {
        [Key]
        public int TownId { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }

        public virtual ICollection<Team> Teams { get; set; }
    }
}
