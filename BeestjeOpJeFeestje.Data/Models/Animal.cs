﻿using System.ComponentModel.DataAnnotations;

namespace BeestjeOpJeFeestje.Data.Models
{
    public class Animal
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public AnimalTypes Type { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public string ImageURL { get; set; }

        public virtual List<Booking>? Bookings { get; set; }
    }
}
