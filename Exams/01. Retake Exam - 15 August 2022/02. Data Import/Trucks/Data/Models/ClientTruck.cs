using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trucks.Data.Models
{
    public class ClientTruck
    {
        [Required, ForeignKey(nameof(Client))]
        public int ClientId { get; set; }
        public Client Client { get; set; }

        [Required, ForeignKey(nameof(Truck))]
        public int TruckId { get; set; }
        public Truck Truck { get; set; }
    }
}
