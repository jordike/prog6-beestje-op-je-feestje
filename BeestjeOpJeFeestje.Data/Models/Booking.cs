using System.ComponentModel.DataAnnotations;

namespace BeestjeOpJeFeestje.Data.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public List<Animal> animals { get; set; }
        public Account Account { get; set; }
        [Required]
        public string ContactName { get; set; }
        [Required]
        public string ContactAddress { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhoneNumber { get; set; }
        public int Discount { get; set; }
        public bool IsConfirmed { get; set; }

    }
}
