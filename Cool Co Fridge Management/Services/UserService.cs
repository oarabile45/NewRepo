using Cool_Co_Fridge_Management.Data;
using Cool_Co_Fridge_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace Cool_Co_Fridge_Management.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> NewRegisterAsync(Users user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _context.users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<(bool isSuccess, string userRole, string userName)> LoginUserAsync(string email, string password)
        {
            if (email == "admin@example.com" && password == "@Admin123")
            {
                return (true, "7", "Administrator"); // Admin role
            }
            else if (email == "faultTech@cool.com" && password == "@FaultTech234")
            {
                return (true, "2", "Fault Technician");
            }
            else if (email == "maintenancetech@cool.com" && password == "@MainTech456")
            {
                return (true, "3", "Maintenance Technician");
            }
            else if (email == "stockcontroller@cool.com" && password == "@StockC789")
            {
                return (true, "5", "Stock Controller");
            }
            else if (email == "purchasingM@cool.com" && password == "@PManager951")
            {
                return (true, "6", "Purchasing Manager");
            }
            else if (email == "customerService@cool.com" && password == "@CustomerS741")
            {
                return (true, "5", "Customer Service");
            }

            // Check the database for the user
            var dbUser = _context.users.FirstOrDefault(u => u.Email == email);
            if (dbUser != null)
            {
                if (BCrypt.Net.BCrypt.Verify(password, dbUser.Password))
                {
                    return (true, dbUser.RoleId.ToString(), dbUser.FirstName);
                }
            }

            // Return failure if no user was found or the password is incorrect
            return (false, null, null);
        }
    }
}
    

