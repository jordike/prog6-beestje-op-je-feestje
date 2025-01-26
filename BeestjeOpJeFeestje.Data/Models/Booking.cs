using System.ComponentModel.DataAnnotations;
using BeestjeOpJeFeestje.Data.Validations;

namespace BeestjeOpJeFeestje.Data.Models;

public class Booking
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime Date { get; set; }

    public List<Animal> Animals { get; set; }

    public Account? Account { get; set; }

    public string? ContactName { get; set; }

    public string? ContactAddress { get; set; }

    public string? ContactEmail { get; set; }

    public string? ContactPhoneNumber { get; set; }

    public int? Discount { get; set; }

    public bool IsConfirmed { get; set; }
}
