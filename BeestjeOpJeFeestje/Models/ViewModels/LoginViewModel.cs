using System.ComponentModel.DataAnnotations;

namespace BeestjeOpJeFeestje.Models.ViewModels;

public class LoginViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(2)]
    [MaxLength(50)]
    public string Password { get; set; }
}
