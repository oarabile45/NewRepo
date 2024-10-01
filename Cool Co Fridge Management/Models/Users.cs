using System.ComponentModel.DataAnnotations;
using System;
using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace Cool_Co_Fridge_Management.Models
{
    public class Users
    {
        
        public int ID { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        [Required] public int RoleId { get; set; }
        public Roles? Roles { get; set; }
    }
}
