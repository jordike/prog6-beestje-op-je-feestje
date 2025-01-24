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

        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }

        public MembershipLevel MembershipLevel { get; set; }
    }
}
