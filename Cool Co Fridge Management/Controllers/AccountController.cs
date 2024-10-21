using Microsoft.AspNetCore.Mvc;
using Cool_Co_Fridge_Management.Data;
using Cool_Co_Fridge_Management.Models;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using Cool_Co_Fridge_Management.Services;


namespace Cool_Co_Fridge_Management.Controllers
{
    public class AccountController : Controller //use this as customer controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserService _userService;
        public AccountController(ApplicationDbContext context, UserService userService)
        {
            _context = context;
            _userService = userService;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Users user)
        {
            if (ModelState.IsValid)
            {
                await _userService.NewRegisterAsync(user);
                TempData["SuccessMessage"] = "You have successfully created an account";
                return RedirectToAction("Success");
            }
            
            return View(user);
        }
        public IActionResult NewLogin()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewLogin(string email, string password)
        {
            var (isSuccess, userRole, userName) = await _userService.LoginUserAsync(email, password);
            if (isSuccess)
            {
                HttpContext.Session.SetString("UserRole", userRole);
                HttpContext.Session.SetString("UserName", userName);

                if (userRole == "5" || userRole == "7" )
                {
                    return RedirectToAction("EmployeeLanding","Home");
                }
                else if(userRole == "2")
                {
                    return RedirectToAction("FaultTechIndex", "FridgeFaults");
                }
                else if(userRole == "6")
                {
                    return RedirectToAction("PManager", "Home");
                }
                else if(userRole == "3")
                {
                    return RedirectToAction("MaintenanceTech", "Home");
                }
                else if(userRole == "4")
                {
                    return RedirectToAction("StockControllerIndex", "StockController");
                }
                
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Invalid Login");
            return View();
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
        public IActionResult SomeProtectedAction()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(userEmail))
            {
                return RedirectToAction("NewLogin");
            }
            return View();
        }
        public IActionResult Manage()
        {
            // Retrieve the user from the session
            var userName = HttpContext.Session.GetString("UserName");

            if (string.IsNullOrEmpty(userName))
            {
                // If no user is logged in, redirect to the login page
                return RedirectToAction("NewLogin", "Account");
            }

            // You can also retrieve more information from the database if needed
            var dbUser = _context.users.FirstOrDefault(u => u.FirstName == userName); // Or use email if you store that instead

            if (dbUser == null)
            {
                // If the user is not found, log out and redirect to the login page
                return RedirectToAction("Logout");
            }

            // Return a view to manage account (you will need a Manage.cshtml view)
            return View(dbUser);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            // Clear the session
            HttpContext.Session.Clear();

            // Redirect to the Home page after logout
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
