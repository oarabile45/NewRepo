using Microsoft.AspNetCore.Identity;
namespace Cool_Co_Fridge_Management.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address {  get; set; }    
    }
}
