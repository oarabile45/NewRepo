using System.ComponentModel.DataAnnotations;

namespace Cool_Co_Fridge_Management.Models
{
    public class Roles
    {
        [Key] 
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public ICollection<Users> Users { get; set; }
    }
}
