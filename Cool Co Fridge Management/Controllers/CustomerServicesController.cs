using Cool_Co_Fridge_Management.Data;
using Microsoft.AspNetCore.Mvc;
using Cool_Co_Fridge_Management.Models;

namespace Cool_Co_Fridge_Management.Controllers
{
    public class CustomerServicesController : Controller
    {
       private readonly ApplicationDbContext _context;
        public CustomerServicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult CustomerFridgeReq()
        {
            return View();
        }
        public IActionResult ChatPanelStatus()
        {
            return View();
        }


        public IActionResult UsersProfile()
        {
            return View();
        }

        public IActionResult Availablefridges()
        {
            return View();
        }

        public IActionResult Allocatedfridges()
        {
            return View();
        }

        public IActionResult EmployeeChatPanelStatus()
        {
            return View();
        }
        public IActionResult AssignRoleToCustomers(int ID) 
        {
            var user = _context.users.Find(ID);
            if (user == null)
            {
                return NotFound();
            }
            var customerRole = _context.roles.SingleOrDefault(r => r.RoleName == "Customer");
            if (customerRole != null)
            {
                user.RoleId = customerRole.RoleID;
                _context.SaveChanges();
            }

            //code for sending customer log in details would come here
            return RedirectToAction("Index");
        }
        
    }
}
