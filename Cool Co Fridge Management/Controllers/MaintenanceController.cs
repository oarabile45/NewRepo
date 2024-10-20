using Cool_Co_Fridge_Management.Data;
using Cool_Co_Fridge_Management.Models;
using Cool_Co_Fridge_Management.ViewModels;
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
            // Fetch users for dropdown selection (if applicable)
            var users = _dbcontext.users.ToList();
            ViewBag.Users = users; // Pass users to the view for selection
            return View();
        }

        // POST: MaintenanceRequest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MaintenanceRequestViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var maintenanceRequest = new MaintenanceRequest
                {
                    UserId = viewModel.UserId,
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    Address = viewModel.Address,
                    RequestedDate = viewModel.RequestedDate,
                    Status = RequestStatus.Pending,
                    UserConfirmationStatus = "Pending"
                };

                _dbcontext.MaintenanceRequests.Add(maintenanceRequest);
                _dbcontext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        // GET: MaintenanceRequest/Approve
        public IActionResult Approve()
        {
            var pendingRequests = _dbcontext.MaintenanceRequests
                .Where(r => r.Status == RequestStatus.Pending)
                .ToList();
            return View(pendingRequests);
        }

        // POST: MaintenanceRequest/Approve
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Approve(int bookingId)
        {
            //var request = _dbcontext.MaintenanceRequests.Find(bookingId);
            var request = _dbcontext.MaintenanceRequests
                .FirstOrDefault(r => r.BookingID == bookingId);

            if (request != null)
            { 
                request.Status = RequestStatus.Approved;
                request.ApprovedDate = DateTime.Now;
                request.IsApprovedByTechnician = true;

                _dbcontext.SaveChanges();
            }
            return RedirectToAction("Approve");
        }

        // POST: MaintenanceRequest/Reject
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Reject(int bookingId)
        {
            var request = _dbcontext.MaintenanceRequests
                .FirstOrDefault(r => r.BookingID == bookingId);

            if (request != null)
            {
                request.Status = RequestStatus.Rejected;
                request.IsApprovedByTechnician = false;

                _dbcontext.SaveChanges();
            }
            return RedirectToAction("Approve");
        }

        // GET: MaintenanceRequest/Confirm
        public IActionResult Confirm()
        {
            var approvedRequests = _dbcontext.MaintenanceRequests
                .Where(r => r.Status == RequestStatus.Approved)
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
                request.Status = status == "Confirmed" ? RequestStatus.Completed : RequestStatus.Rejected;
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
            var requests = _dbcontext.MaintenanceRequests
               .Select(m => new MaintenanceRequest
        {
            BookingID = m.BookingID,
            FirstName = m.FirstName ?? "N/A", // Provide a default value if null
            LastName = m.LastName ?? "N/A",
            Address = m.Address ?? "N/A",
            RequestedDate = m.RequestedDate,
            ApprovedDate = m.ApprovedDate,
            FaultDescription = m.FaultDescription ?? "No description",
            UserConfirmationStatus = m.UserConfirmationStatus ?? "Pending"
        })
        .ToList();

            return View(requests);
           
        }
    

   


   




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
                    //TechnicianName = m.User.FirstName + " " + m.User.LastName,
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



       
    }
}
