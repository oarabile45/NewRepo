﻿using Cool_Co_Fridge_Management.Data;
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
            var maintenanceRequest = _context.MaintenanceRequests
                .Include(m => m.User)
                .Include(m => m.MaintenanceTech)
                .ToList();
            return View(maintenanceRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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

        [HttpPost]
        [ValidateAntiForgeryToken]
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
            var maintenancetechId = 1;
            var message = $"The service request for {maintenanceRequest.FirstName} has been {maintenanceRequest.UserConfirmationStatus.ToLower()}";

            var notification = new Notification
            {
                MaintenanceTechID = maintenancetechId, ///fix to maintenance
                Message = message,
                CreatedAt = DateTime.Now,
                IsRead = false
            };
            _context.Notifications.Add(notification);
            _context.SaveChanges();
            //var maintenanceTech = _context.MaintenanceTech.FirstOrDefault();
            //if (maintenanceTech == null)
            //{
            //    throw new Exception("No Maintenance Technician available");
            //}

            //var message = $"The service request for {maintenanceRequest.FirstName} has been {maintenanceRequest.UserConfirmationStatus.ToLower()}";

            //var notification = new Notification
            //{
            //    MaintenanceTechID = maintenanceTech.MaintenanceTechID, ///fix to maintenance
            //    Message = message,
            //    CreatedAt = DateTime.Now,
            //    IsRead = false
            //};
            //_context.Notifications.Add(notification);
            //_context.SaveChanges();


        }

        public IActionResult MaintenanceService()
        {
            return View();
        }

        public IActionResult MaintenanceTech()
        {
            return View();
        }

        //public IActionResult Reports(DateTime? startDate, DateTime? endDate, string faultStatus = null)
        //{
        //    var query = _context.MaintenanceRequests.Include(m => m.User)
        //        .Select(m => new MaintenanceReportViewModel
        //        {
        //            BookingID = m.BookingID,
        //            CustomerName = $"{m.FirstName} {m.LastName}",
        //            Address = m.Address,
        //            RequestedDate = m.RequestedDate,
        //            ApprovedDate = m.ApprovedDate,
        //            RequestStatus = m.status.ToString(),
        //            FaultDescription = m.FaultDescription,
        //            IsApprovedByTechnician = m.IsApprovedByTechnician
        //        })
        //        .AsQueryable();


            
        //}
    }
}
