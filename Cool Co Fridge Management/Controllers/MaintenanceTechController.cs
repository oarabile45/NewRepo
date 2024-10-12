using System;
using System.Linq;
using Cool_Co_Fridge_Management.Data;
using Cool_Co_Fridge_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Cool_Co_Fridge_Management.Controllers
{
    public class MaintenanceTechController : Controller
    {
        private readonly ApplicationDbContext _context;
        public MaintenanceTechController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var requests = _context.MaintenanceBookings.Where(r => r.status == RequestStatus.Pending).ToList();
            return View(requests);
        }
        public IActionResult Approve(int id)
        {
            var request = _context.MaintenanceBookings.Find(id);
            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ApproveBookingConfirmed(int id)
        {
            var request = _context.MaintenanceBookings.Find(id);
            if (request == null)
            {
                return NotFound();
            }

            request.IsApprovedByTechnician = true;
            _context.SaveChanges();

            return RedirectToAction("MaintenanceTechDashboard");
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Approve(MaintenanceBooking request)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var existingRequest = _context.MaintenanceBookings.Find(request.BookingID);
        //        if (existingRequest == null)
        //        {
        //            existingRequest.ApprovedDate = DateTime.Now;
        //            existingRequest.status = RequestStatus.Approved;
        //            _context.SaveChanges();
        //            return RedirectToAction("Index");
        //        }

        //    }
        //    return View(request);
        //}

        public IActionResult UpdateService(int id)
        {
            var request = _context.MaintenanceBookings.Find(id);
            if (request == null)
            {
                return NotFound();
            }
            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateService(int id, MaintenanceBooking updatedBooking)
        {
            var request = _context.MaintenanceBookings.Find(id);
            if (request == null)
            {
                return NotFound();
            }

            request.FaultDescription = updatedBooking.FaultDescription;
            _context.SaveChanges();

            return RedirectToAction("MaintenanceTechDashboard");
        }
    }
}
