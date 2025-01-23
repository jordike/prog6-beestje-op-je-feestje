using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BeestjeOpJeFeestje.Data.Models
{
    public class Account : IdentityUser
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public Membershiplevel MembershipLevel { get; set; }
    }
}
