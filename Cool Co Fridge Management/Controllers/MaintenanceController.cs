using Cool_Co_Fridge_Management.Data;
using Cool_Co_Fridge_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
//using iTextSharp.text;
//using iTextSharp.text.pdf;
using System.IO;

namespace Cool_Co_Fridge_Management.Controllers
{
    public class MaintenanceController : Controller
    {
        private readonly ApplicationDbContext _context;
        public MaintenanceController(ApplicationDbContext context)
        {
            _context = context;
        }

        //GET: MaintenanceRequest
        public IActionResult Index()
        {
            // Fetch all maintenance bookings from the database
            var maintenanceRequests = _context.MaintenanceRequests
                .Include(m => m.User)
                .Include(m => m.MaintenanceTech)
                .ToList();
            return View(maintenanceRequests); // Pass the list to the view
        }

        //GET: MaintenanceRequest/Details
        public IActionResult Details(int? bookingID)
        {
            if (bookingID == null)
            {
                return NotFound();
            }
            var maintenanceRequest = _context.MaintenanceRequests
                .Include(m => m.User)
                .FirstOrDefault(b => b.BookingID == bookingID);

            if (maintenanceRequest == null)
            {
                return NotFound();
            }
            return View(maintenanceRequest);
        }
        //GET: MaintenanceRequest/Create
        public IActionResult Create()
        {
            return View();
        }

        //POST: MaintenanceRequest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MaintenanceRequest maintenanceRequest)
        {
            if (ModelState.IsValid)
            {
                _context.MaintenanceRequests.Add(maintenanceRequest);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(maintenanceRequest);
        }

        //GET: MiantenanceRequest/Edit
        public IActionResult Edit(int? bookingID)
        {
            if (bookingID == null)
            {
                return NotFound();
            }

            var maintenanceRequest = _context.MaintenanceRequests.Find(bookingID);
            if (maintenanceRequest == null)
            {
                return NotFound();
            }
            return View(maintenanceRequest);
        }

        //POST: MaintenanceRequest/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int bookingID, MaintenanceRequest maintenanceRequest)
        {
            if (bookingID != maintenanceRequest.BookingID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(maintenanceRequest);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(maintenanceRequest);
        }

        //GET: MaintenanceRquest/Delete
        public IActionResult Delete(int? bookingID)
        {
            if (bookingID == null)
            {
                return NotFound();
            }

            var maintenanceRequest = _context.MaintenanceRequests.FirstOrDefault(m => m.BookingID == bookingID);
            if (maintenanceRequest == null)
            {
                return NotFound();
            }

            return View(maintenanceRequest);
        }

        //POST: MaintenanceRequest/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int bookingID)
        {
            var maintenanceRequest = _context.MaintenanceRequests.Find(bookingID);
            _context.MaintenanceRequests.Remove(maintenanceRequest);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        //POST: MaintemamceRequest/Approve
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Approve(int bookingID)
        {
            var maintenanceRequest = _context.MaintenanceRequests.Find(bookingID);
            if (maintenanceRequest != null)
            {
                maintenanceRequest.IsApprovedByTechnician = true;
                maintenanceRequest.ApprovedDate = DateTime.Now;
                maintenanceRequest.status = RequestStatus.Approved;

                _context.Update(maintenanceRequest);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        //POST: MaintenanceRequest/UserConfirmation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UserConfirmation(int bookingID, string confirmationStatus)
        {
            var maintenanceRequest = _context.MaintenanceRequests.Find(bookingID);
            if (maintenanceRequest != null)
            {
                maintenanceRequest.UserConfirmationStatus = confirmationStatus;
                maintenanceRequest.status = confirmationStatus == "Confirmed" ? RequestStatus.Completed : RequestStatus.Rejected;

                _context.Update(maintenanceRequest);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool BookingExits(int bookingID)
        {
            return _context.MaintenanceRequests.Any(e => e.BookingID == bookingID);
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
        
        // Report generation method
        public IActionResult GenerateReport()
        {
            // Fetch Maintenance Requests
            var reportData = _context.MaintenanceRequests
                .Where(m => m.IsApprovedByTechnician) // Optional: Filter approved bookings
                .Select(m => new
                {
                    m.BookingID,
                    TechnicianName = m.User.FirstName + " " + m.User.LastName,
                    m.Address,
                    m.RequestedDate,
                    m.ApprovedDate,
                    m.FaultDescription,
                    m.UserConfirmationStatus
                })
                .ToList();

            // Pass the data to the view
            return View(reportData);
        }

        //public IActionResult ExportToPDF()
        //{
        //    var reportData = _context.MaintenanceRequests
        //        .Where(m => m.IsApprovedByTechnician)
        //        .Select(m => new
        //        {
        //            m.BookingID,
        //            //TechnicianName = m.User.FirstName + " " + m.User.LastName,
        //            m.Address,
        //            m.RequestedDate,
        //            m.ApprovedDate,
        //            m.FaultDescription,
        //            m.UserConfirmationStatus
        //        })
        //        .ToList();

        //    using (MemoryStream stream = new MemoryStream())
        //    {
        //        Document pdfDoc = new Document(PageSize.A4, 25, 25, 30, 30);
        //        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
        //        pdfDoc.Open();

        //        pdfDoc.Add(new Paragraph("Maintenance Report"));
        //        pdfDoc.Add(new Paragraph(" "));

        //        PdfPTable table = new PdfPTable(7);
        //        table.AddCell("Booking ID");
        //        //table.AddCell("Technician Name");
        //        table.AddCell("Address");
        //        table.AddCell("Requested Date");
        //        table.AddCell("Approved Date");
        //        table.AddCell("Fault Description");
        //        table.AddCell("Customer Status");

        //        foreach (var item in reportData)
        //        {
        //            table.AddCell(item.BookingID.ToString());
        //            //table.AddCell(item.TechnicianName);
        //            table.AddCell(item.Address);
        //            table.AddCell(item.RequestedDate.ToString("g"));
        //            table.AddCell(item.ApprovedDate?.ToString("g"));
        //            table.AddCell(item.FaultDescription);
        //            table.AddCell(item.UserConfirmationStatus);
        //        }

        //        pdfDoc.Add(table);
        //        pdfDoc.Close();
        //        writer.Close();

        //        return File(stream.ToArray(), "application/pdf", "MaintenanceReport.pdf");
        //    }
        //}



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
