

namespace P02_FootballBetting.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Country
    {
        [Key]
        public int CountryId { get; set; }

        public string Name { get; set; }
                 
        public virtual ICollection<Town> Towns { get; set; }
    }
}
