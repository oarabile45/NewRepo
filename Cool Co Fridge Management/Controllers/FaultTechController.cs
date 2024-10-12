using Cool_Co_Fridge_Management.Data;
using System;
using System.Linq;
using Cool_Co_Fridge_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Cool_Co_Fridge_Management.Controllers
{
    public class FaultTechController : Controller
    {
        private readonly ApplicationDbContext _context;
        public FaultTechController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult ViewAssignedFaults()
        {
            // Assuming technician authentication is in place
            var faultTechId = 1;
            var faults = _context.Faults.Where(f => f.FaultTechId == faultTechId).ToList();
            return View(faults);
        }
        public IActionResult Notifications()
        {
            var faultTechId = 1;
            var notifications = _context.Notifications.Where(n => n.FaultTechId == faultTechId && !n.IsRead).ToList();

            return View(notifications);
        }

        public IActionResult MarkAsRead(int NotificationId)
        {
            var notification = _context.Notifications.Find(NotificationId);
            if (notification != null)
            {
                notification.IsRead = true;
                _context.SaveChanges();
            }
            return RedirectToAction("Notifications");
        }
    }
}
