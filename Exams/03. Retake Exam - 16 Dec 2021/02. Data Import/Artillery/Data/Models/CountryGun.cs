using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Artillery.Data.Models
{
    public class CountryGun
    {        
        [Required, ForeignKey(nameof(Country))]
        public int CountryId { get; set; }
        public Country Country { get; set; }
        
        [Required, ForeignKey(nameof(Gun))]
        public int GunId { get; set; }
        public Gun Gun { get; set; }
    }
}
