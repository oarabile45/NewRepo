using Microsoft.AspNetCore.Mvc;
using Cool_Co_Fridge_Management.Data;
using Cool_Co_Fridge_Management.Models;
using System.Threading.Tasks;


namespace Cool_Co_Fridge_Management.Controllers
{
    public class AccountController : Controller //use this as customer controller
    {
        private readonly ApplicationDbContext _context;
        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Users user)
        {
            if (ModelState.IsValid)
            {
                _context.users.Add(user);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "The operation was successful";
                return RedirectToAction("Success");
            }
            else
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Validation error: {error.ErrorMessage}");
                }
            }
            
            return View(user);
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
