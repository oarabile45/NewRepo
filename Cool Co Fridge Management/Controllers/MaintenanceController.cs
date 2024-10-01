//using Cool_Co_Fridge_Management.Data;
//using Cool_Co_Fridge_Management.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using System.Collections.Generic;

//namespace Cool_Co_Fridge_Management.Controllers
//{
//    public class MaintenanceController : Controller
//    {
//        private readonly ApplicationDbContext _context;
//        public MaintenanceController(ApplicationDbContext context)
//        {
//            _context = context;
//        }
        
//        public IActionResult ServiceList()
//        {
//            IEnumerable<MaintenanceBooking> objList = _context.MaintenanceBooking;
//            return View(objList);
//            //var MaintenanceBooking = _context.maintenancebookings.ToList();
//            //return View(MaintenanceBooking);
//        }
//        public IActionResult MaintenanceBooking()
//        {
//            return View();
//        }
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult MaintenanceBooking(MaintenanceBooking bookings)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.MaintenanceBooking.Add(bookings);
//                _context.SaveChanges();
//                return RedirectToAction("ServiceList");
//            }
//            return View(bookings);
//        }
//        public IActionResult MaintenanceTech()
//        {
//            return View();
//        }

//        public IActionResult MaintenanceService()
//        {
//            return View();
//        }

//    }
//}
