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
                TempData["SuccessMessage"] = "The operation was successful";
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

                if (userRole == "7" || userRole == "6" || userRole == "5" || userRole == "4" || userRole == "3" || userRole == "2")
                {
                    return RedirectToAction("EmployeeLanding", "Home");
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

        //public async Task<IActionResult>Register(Users users)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(users);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return View(users);
        //}
        //private readonly UserManager<ApplicationUser> _userManager;
        //private readonly SignInManager<ApplicationUser> _signInManager;

        //public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        //{
        //    _userManager = userManager;
        //    _signInManager = signInManager;
        //}
        //[HttpGet]
        //public IActionResult Register() //customer requesting log in details
        //{
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task< IActionResult> Register(RegisterView model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = new ApplicationUser
        //        {
        //            //Username = model.Email,
        //            Email = model.Email,
        //            FirstName = model.FirstName,
        //            LastName = model.LastName,
        //            Address = model.Address
        //        };
        //        //user.EmailConfirmed = true;
        //        var result = await _userManager.CreateAsync(user);
        //        if (result.Succeeded)
        //        {
        //            await _signInManager.SignInAsync(user, isPersistent: false);
        //            ViewBag.Message = "Your request was sent successfully";
        //            //return RedirectToAction("Index", "Home");
        //        }
        //        else
        //        {
        //            ViewBag.Message = "Error. Your request was unsuccessful.";
        //        }
        //        foreach (var error in result.Errors)
        //        {
        //            ModelState.AddModelError(string.Empty, error.Description);
        //        }

        //    }
        //    return View(model);
            
        //}
    }
}
