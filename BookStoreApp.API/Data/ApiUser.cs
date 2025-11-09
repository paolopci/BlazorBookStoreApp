using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BookStoreApp.API.Data
{
    public class ApiUser : IdentityUser
    {
        [PersonalData]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;
        [PersonalData]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

    }
}
