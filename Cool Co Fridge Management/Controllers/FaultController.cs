using Cool_Co_Fridge_Management.Data;
using Cool_Co_Fridge_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cool_Co_Fridge_Management.Controllers
{
    public class FaultController : Controller
    {
        private readonly ApplicationDbContext _context;
        public FaultController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult CreateFault(int bookingID)
        {
            var techs = _context.FaultTech.ToList();
            ViewBag.FaultTech = new SelectList(techs, "FaultTechId", "Name");

            var fault = new Fault { FaultId = bookingID };
            return View(fault);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateFault(Fault fault)
        {
            if (ModelState.IsValid)
            {
                _context.Faults.Add(fault);
                _context.SaveChanges();

                // Add a notification for fault technician
                CreateNotification(fault.FaultTechId, $"A new fault has been assigned to you: {fault.Description}");
                return RedirectToAction("Index", "FaultTech");
            }
            var techs = _context.FaultTech.ToList();
            ViewBag.FaultTech = new SelectList(techs, "FaultTechId", "Name", fault.FaultTechId);
            return View(fault);
        }

        private void CreateNotification(int faultTechId, string message)
        {
            var notification = new Notification
            {
                FaultTechId = faultTechId,
                Message = message,
                CreatedAt = DateTime.Now,
                IsRead = false,
            };
            _context.Notifications.Add(notification);
            _context.SaveChanges();
        }
    }
}
