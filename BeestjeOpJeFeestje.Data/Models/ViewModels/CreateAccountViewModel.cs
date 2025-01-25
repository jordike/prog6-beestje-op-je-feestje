using System.ComponentModel.DataAnnotations;

namespace BeestjeOpJeFeestje.Data.Models.ViewModels;

public class CreateAccountViewModel
{
    [Required]
    public string Name { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Address { get; set; }

    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; }

    public MembershipLevel MembershipLevel { get; set; }
}
