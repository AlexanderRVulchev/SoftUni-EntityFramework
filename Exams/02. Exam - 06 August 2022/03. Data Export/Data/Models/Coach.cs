﻿using System.ComponentModel.DataAnnotations;

namespace Footballers.Data.Models
{
    public class Coach
    {
        public Coach()
        {
            Footballers = new HashSet<Footballer>();
        }

        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Nationality { get; set; }
        
        public ICollection<Footballer> Footballers { get; set; }
    }
}
