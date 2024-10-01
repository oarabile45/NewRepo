using Cool_Co_Fridge_Management.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Cool_Co_Fridge_Management.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index() //landing page before any login
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult PManager() 
        {
            return View();
        }

        public IActionResult EmployeeLanding()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult MaintenanceTech()
        {
            return View();
        }

        public IActionResult MaintenanceService()
        {
            return View();
        }

        

    }
}
