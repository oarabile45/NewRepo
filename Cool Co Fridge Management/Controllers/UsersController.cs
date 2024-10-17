using Cool_Co_Fridge_Management.Data;
using Cool_Co_Fridge_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Cool_Co_Fridge_Management.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var requests = _context.MaintenanceRequests.ToList();
            return View(requests);
        }

        public IActionResult RequestBooking()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RequestBooking(MaintenanceRequest request)
        {
            if (ModelState.IsValid)
            {
                request.status = RequestStatus.Pending;
                _context.MaintenanceRequests.Add(request);
                _context.SaveChanges();
                return RedirectToAction("RequestConfirmation");
            }
            return View(request);
        }
        public IActionResult RequestConfirmation()
        {
            return View();
        }
    }
}
