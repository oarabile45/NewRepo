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
            var maintenanceRequest = _context.MaintenanceRequests.Include(b => b.User).ToList();
            return View(maintenanceRequest);
        }

        public IActionResult ConfirmBooking(int bookingID)
        {
            var maintenanceRequest = _context.MaintenanceRequests.Find(bookingID);
            if (maintenanceRequest != null && maintenanceRequest.IsApprovedByTechnician)
            {
                maintenanceRequest.UserConfirmationStatus = "Confirmed";
                _context.SaveChanges();

                NotifyTechnician(maintenanceRequest);
            }
            return RedirectToAction("Index");
        }
        public IActionResult CancelBooking(int bookingID)
        {
            var maintenanceRequest = _context.MaintenanceRequests.Find(bookingID);
            if (maintenanceRequest != null && maintenanceRequest.IsApprovedByTechnician)
            {
                maintenanceRequest.UserConfirmationStatus = "Canceled";
                _context.SaveChanges();

                NotifyTechnician(maintenanceRequest);
            }
            return RedirectToAction("Index");
        }

        private void NotifyTechnician(MaintenanceRequest maintenanceRequest)
        {
            var maintenanceTech = _context.MaintenanceTech.FirstOrDefault();
            if (maintenanceTech == null)
            {
                throw new Exception("No Maintenance Technician available");
            }

            var message = $"The service request for {maintenanceRequest.FirstName} has been {maintenanceRequest.UserConfirmationStatus.ToLower()}";

            var notification = new Notification
            {
                MaintenanceTechID = maintenanceTech.MaintenanceTechID, ///fix to maintenance
                Message = message,
                CreatedAt = DateTime.Now,
                IsRead = false
            };
            _context.Notifications.Add(notification);
            _context.SaveChanges();


        }

        public IActionResult MaintenanceService()
        {
            return View();
        }

        public IActionResult MaintenanceTech()
        {
            return View();
        }


    }
}
