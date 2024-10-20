using Cool_Co_Fridge_Management.Data;
using Cool_Co_Fridge_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace Cool_Co_Fridge_Management.Controllers
{
    public class MaintenanceController : Controller
    {
        private readonly ApplicationDbContext _dbcontext;
        public MaintenanceController(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }


        // GET: MaintenanceRequest/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MaintenanceRequest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MaintenanceRequest request)
        {
            if (ModelState.IsValid)
            {
                _dbcontext.MaintenanceRequests.Add(request);
                _dbcontext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(request);
        }

        // GET: MaintenanceRequest/Approve
        public IActionResult Approve()
        {
            var pendingRequests = _dbcontext.MaintenanceRequests
                .Where(r => r.status == RequestStatus.Pending)
                .ToList();
            return View(pendingRequests);
        }

        // POST: MaintenanceRequest/Approve
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Approve(int bookingId)
        {
            var request = _dbcontext.MaintenanceRequests.Find(bookingId);
            if (request != null)
            {
                request.status = RequestStatus.Approved;
                request.ApprovedDate = DateTime.Now;
                _dbcontext.SaveChanges();
            }
            return RedirectToAction("Approve");
        }

        // POST: MaintenanceRequest/Reject
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Reject(int bookingId)
        {
            var request = _dbcontext.MaintenanceRequests.Find(bookingId);
            if (request != null)
            {
                request.status = RequestStatus.Rejected;
                _dbcontext.SaveChanges();
            }
            return RedirectToAction("Approve");
        }

        // GET: MaintenanceRequest/Confirm
        public IActionResult Confirm()
        {
            var approvedRequests = _dbcontext.MaintenanceRequests
                .Where(r => r.status == RequestStatus.Approved)
                .ToList();
            return View(approvedRequests);
        }

        // POST: MaintenanceRequest/Confirm
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Confirm(int bookingId, string status)
        {
            var request = _dbcontext.MaintenanceRequests.Find(bookingId);
            if (request != null)
            {
                request.UserConfirmationStatus = status;
                request.status = status == "Confirmed" ? RequestStatus.Completed : RequestStatus.Rejected;
                _dbcontext.SaveChanges();
            }
            return RedirectToAction("Confirm");
        }

        // GET: MaintenanceRequest/Edit
        public IActionResult Edit(int id)
        {
            var request = _dbcontext.MaintenanceRequests.Find(id);
            if (request == null)
            {
                return NotFound();
            }
            return View(request);
        }

        // POST: MaintenanceRequest/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, MaintenanceRequest request)
        {
            if (id != request.BookingID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _dbcontext.Update(request);
                _dbcontext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(request);
        }

        // GET: MaintenanceRequest/Delete
        public IActionResult Delete(int id)
        {
            var request = _dbcontext.MaintenanceRequests.Find(id);
            if (request == null)
            {
                return NotFound();
            }
            return View(request);
        }

        // POST: MaintenanceRequest/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var request = _dbcontext.MaintenanceRequests.Find(id);
            if (request != null)
            {
                _dbcontext.MaintenanceRequests.Remove(request);
                _dbcontext.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: MaintenanceRequest/ConfirmedBookings
        public IActionResult ConfirmedBookings()
        {
            var confirmedRequests = _dbcontext.MaintenanceRequests
                .Where(r => r.UserConfirmationStatus == "Confirmed")
                .ToList();
            return View(confirmedRequests);
        }

        // GET: MaintenanceRequest/Index
        public IActionResult Index()
        {
            var requests = _dbcontext.MaintenanceRequests.ToList();
            return View(requests);
        }
    

   

    //    //GET: MaintenanceRequest/Details
    //    public IActionResult Details(int? bookingID)
    //    {
    //        if (bookingID == null)
    //        {
    //            return NotFound();
    //        }
    //        var maintenanceRequest = _context.MaintenanceRequests
    //            .Include(m => m.User)
    //            .FirstOrDefault(b => b.BookingID == bookingID);

    //        if (maintenanceRequest == null)
    //        {
    //            return NotFound();
    //        }
    //        return View(maintenanceRequest);
    //    }
   




    //    private void NotifyTechnician(MaintenanceRequest maintenanceRequest)
    //    {
    //        var maintenancetechId = 1;
    //        var message = $"The service request for {maintenanceRequest.FirstName} has been {maintenanceRequest.UserConfirmationStatus.ToLower()}";

    //        var notification = new Notification
    //        {
    //            MaintenanceTechID = maintenancetechId, ///fix to maintenance
    //            Message = message,
    //            CreatedAt = DateTime.Now,
    //            IsRead = false
    //        };
    //        _context.Notifications.Add(notification);
    //        _context.SaveChanges();
    //        var maintenanceTech = _context.MaintenanceTech.FirstOrDefault();
    //        if (maintenanceTech == null)
    //        {
    //            throw new Exception("No Maintenance Technician available");
    //        }

    //        var message = $"The service request for {maintenanceRequest.FirstName} has been {maintenanceRequest.UserConfirmationStatus.ToLower()}";

    //        var notification = new Notification
    //        {
    //            MaintenanceTechID = maintenanceTech.MaintenanceTechID, ///fix to maintenance
    //            Message = message,
    //            CreatedAt = DateTime.Now,
    //            IsRead = false
    //        };
    //        _context.Notifications.Add(notification);
    //        _context.SaveChanges();


    //    }

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
            var reportData = _dbcontext.MaintenanceRequests
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

        public IActionResult ExportToPDF()
        {
            var reportData = _dbcontext.MaintenanceRequests
                .Where(m => m.IsApprovedByTechnician)
                .Select(m => new
                {
                    m.BookingID,
                    //TechnicianName = m.User.FirstName + " " + m.User.LastName,
                    m.Address,
                    m.RequestedDate,
                    m.ApprovedDate,
                    m.FaultDescription,
                    m.UserConfirmationStatus
                })
                .ToList();

            using (MemoryStream stream = new MemoryStream())
            {
                Document pdfDoc = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();

                pdfDoc.Add(new Paragraph("Maintenance Report"));
                pdfDoc.Add(new Paragraph(" "));

                PdfPTable table = new PdfPTable(7);
                table.AddCell("Booking ID");
                //table.AddCell("Technician Name");
                table.AddCell("Address");
                table.AddCell("Requested Date");
                table.AddCell("Approved Date");
                table.AddCell("Fault Description");
                table.AddCell("Customer Status");

                foreach (var item in reportData)
                {
                    table.AddCell(item.BookingID.ToString());
                    //table.AddCell(item.TechnicianName);
                    table.AddCell(item.Address);
                    table.AddCell(item.RequestedDate.ToString("g"));
                    table.AddCell(item.ApprovedDate?.ToString("g"));
                    table.AddCell(item.FaultDescription);
                    table.AddCell(item.UserConfirmationStatus);
                }

                pdfDoc.Add(table);
                pdfDoc.Close();
                writer.Close();

                return File(stream.ToArray(), "application/pdf", "MaintenanceReport.pdf");
            }
        }



        //    public IActionResult Reports(DateTime? startDate, DateTime? endDate, string faultStatus = null)
        //    {
        //        var query = _context.MaintenanceRequests.Include(m => m.User)
        //            .Select(m => new MaintenanceReportViewModel
        //            {
        //                BookingID = m.BookingID,
        //                CustomerName = $"{m.FirstName} {m.LastName}",
        //                Address = m.Address,
        //                RequestedDate = m.RequestedDate,
        //                ApprovedDate = m.ApprovedDate,
        //                RequestStatus = m.status.ToString(),
        //                FaultDescription = m.FaultDescription,
        //                IsApprovedByTechnician = m.IsApprovedByTechnician
        //            })
        //            .AsQueryable();



        //    }
    }
}
