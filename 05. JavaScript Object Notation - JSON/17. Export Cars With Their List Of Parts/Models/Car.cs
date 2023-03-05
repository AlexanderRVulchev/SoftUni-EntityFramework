

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealer.Models
{
    public class Car
    {
        public int Id { get; set; }

        public string Make { get; set; } = null!;

        public string Model { get; set; } = null!;

        [Column("TravelledDistance")]
        public long TraveledDistance { get; set; }

        public ICollection<Sale> Sales { get; set; } = new List<Sale>();    

        public ICollection<PartCar> PartsCars { get; set; } = new List<PartCar>();
    }
}
