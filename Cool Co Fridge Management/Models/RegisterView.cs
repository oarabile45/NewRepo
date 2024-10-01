using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

namespace Cool_Co_Fridge_Management.Models
{
    public class RegisterView
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name ="Last Name")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name ="Address")]
        public string Address { get; set; }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }
        public string ReturnUrl { get; set; }
    }
}
