using Microsoft.AspNetCore.Mvc;

namespace Cool_Co_Fridge_Management.Controllers
{
    public class MaintenanceReportViewModelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
