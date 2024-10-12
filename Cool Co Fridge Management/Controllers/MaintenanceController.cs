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
    public class MaintenanceController : Controller
    {
        private readonly ApplicationDbContext _context;
        public MaintenanceController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var maintenanceBooking = _context.MaintenanceBookings.Include(b => b.User).ToList();
            return View(maintenanceBooking);
        }

        public IActionResult ConfirmBooking(int bookingID)
        {
            var maintenanceBooking = _context.MaintenanceBookings.Find(bookingID);
            if (maintenanceBooking != null && maintenanceBooking.IsApprovedByTechnician)
            {
                maintenanceBooking.UserConfirmationStatus = "Confirmed";
                _context.SaveChanges();

                NotifyTechnician(maintenanceBooking);
            }
            return RedirectToAction("CustomerDashboard");
        }
        public IActionResult CancelBooking(int bookingID)
        {
            var maintenanceBooking = _context.MaintenanceBookings.Find(bookingID);
            if (maintenanceBooking != null && maintenanceBooking.IsApprovedByTechnician)
            {
                maintenanceBooking.UserConfirmationStatus = "Canceled";
                _context.SaveChanges();

                NotifyTechnician(maintenanceBooking);
            }
            return RedirectToAction("CustomerDashboard");
        }

        private void NotifyTechnician(MaintenanceBooking maintenanceBooking)
        {
            var maintenancetechId = 1;
            var message = $"The service request for {maintenanceBooking.FirstName} has been {maintenanceBooking.UserConfirmationStatus.ToLower()}";

            var notification = new Notification
            {
                MaintenanceTechID = maintenancetechId, ///fix to maintenance
                Message = message,
                CreatedAt = DateTime.Now,
                IsRead = false
            };
            _context.Notifications.Add(notification);
            _context.SaveChanges();


        }

        public IActionResult MaintenancService()
        {
            return View();
        }

        public IActionResult MaintenanceTech()
        {
            return View();
        }


    }
}
