using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cool_Co_Fridge_Management.Data;
using Cool_Co_Fridge_Management.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Security.Claims;


namespace Cool_Co_Fridge_Management.Controllers
{
    public class MaintenanceRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MaintenanceRequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MaintenanceRequests
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MaintenanceRequests.Include(m => m.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MaintenanceRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenanceRequest = await _context.MaintenanceRequests
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.BookingID == id);
            if (maintenanceRequest == null)
            {
                return NotFound();
            }

            return View(maintenanceRequest);
        }

        // GET: MaintenanceRequests/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.users, "ID", "ID");
            return View();
        }

        // POST: MaintenanceRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingID,FirstName,LastName,Address,RequestedDate,ApprovedDate,IsApprovedByTechnician,UserConfirmationStatus,Status,FaultDescription,UserId,MaintenanceTechID")] MaintenanceRequest maintenanceRequest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(maintenanceRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.users, "ID", "ID", maintenanceRequest.UserId);
            return View(maintenanceRequest);
        }

        // GET: MaintenanceRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenanceRequest = await _context.MaintenanceRequests.FindAsync(id);
            if (maintenanceRequest == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.users, "ID", "ID", maintenanceRequest.UserId);
            return View(maintenanceRequest);
        }

        // POST: MaintenanceRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingID,FirstName,LastName,Address,RequestedDate,ApprovedDate,IsApprovedByTechnician,UserConfirmationStatus,Status,FaultDescription,UserId,MaintenanceTechID")] MaintenanceRequest maintenanceRequest)
        {
            if (id != maintenanceRequest.BookingID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(maintenanceRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaintenanceRequestExists(maintenanceRequest.BookingID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.users, "ID", "ID", maintenanceRequest.UserId);
            return View(maintenanceRequest);
        }

        // GET: MaintenanceRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenanceRequest = await _context.MaintenanceRequests
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.BookingID == id);
            if (maintenanceRequest == null)
            {
                return NotFound();
            }

            return View(maintenanceRequest);
        }

        // POST: MaintenanceRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var maintenanceRequest = await _context.MaintenanceRequests.FindAsync(id);
            if (maintenanceRequest != null)
            {
                _context.MaintenanceRequests.Remove(maintenanceRequest);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: MaintenanceRequests/Approve/5
        public async Task<IActionResult> Approve(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenanceRequest = await _context.MaintenanceRequests.FindAsync(id);
            if (maintenanceRequest == null)
            {
                return NotFound();
            }

            return View(maintenanceRequest);
        }

        // POST: MaintenanceRequests/Approve/5
        [HttpPost, ActionName("Approve")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveConfirmed(int id)
        {
            var maintenanceRequest = await _context.MaintenanceRequests.FindAsync(id);
            if (maintenanceRequest != null)
            {
                maintenanceRequest.IsApprovedByTechnician = true;
                maintenanceRequest.Status = RequestStatus.Approved;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: MaintenanceRequests/Reject/5
        public async Task<IActionResult> Reject(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenanceRequest = await _context.MaintenanceRequests.FindAsync(id);
            if (maintenanceRequest == null)
            {
                return NotFound();
            }

            return View(maintenanceRequest);
        }

        // POST: MaintenanceRequests/Reject/5
        [HttpPost, ActionName("Reject")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectConfirmed(int id)
        {
            var maintenanceRequest = await _context.MaintenanceRequests.FindAsync(id);
            if (maintenanceRequest != null)
            {
                maintenanceRequest.Status = RequestStatus.Rejected;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: MaintenanceRequests/Confirm/5
        public async Task<IActionResult> Confirm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenanceRequest = await _context.MaintenanceRequests.FindAsync(id);
            if (maintenanceRequest == null)
            {
                return NotFound();
            }

            return View(maintenanceRequest);
        }

        // POST: MaintenanceRequests/Confirm/5
        [HttpPost, ActionName("Confirm")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmConfirmed(int id)
        {
            var maintenanceRequest = await _context.MaintenanceRequests.FindAsync(id);
            if (maintenanceRequest != null)
            {
                maintenanceRequest.UserConfirmationStatus = "Confirmed";
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: MaintenanceRequests/ApprovedRequests
        public async Task<IActionResult> ApprovedRequests()
        {
            var approvedRequests = await _context.MaintenanceRequests
                .Where(m => m.Status == RequestStatus.Approved && m.UserConfirmationStatus == "Pending")
                .Include(m => m.User)
                .ToListAsync();

            return View(approvedRequests);
        }

        // GET: MaintenanceRequests/PendingRequests
        public async Task<IActionResult> PendingRequests()
        {
            var pendingRequests = await _context.MaintenanceRequests
                .Where(m => m.Status == RequestStatus.Pending)
                .Include(m => m.User)
                .ToListAsync();

            return View(pendingRequests);
        }

        // GET: MaintenanceRequests/ConfirmedRequests
        public async Task<IActionResult> ConfirmedRequests()
        {
            var confirmedRequests = await _context.MaintenanceRequests
                .Where(m => m.UserConfirmationStatus == "Confirmed")
                .Include(m => m.User)
                .ToListAsync();

            return View(confirmedRequests);
        }


// GET: MaintenanceRequests/CreateFault/5
public async Task<IActionResult> CreateFault(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenanceRequest = await _context.MaintenanceRequests
                .FirstOrDefaultAsync(m => m.BookingID == id);
            if (maintenanceRequest == null)
            {
                return NotFound();
            }

            return View(maintenanceRequest);
        }

        // POST: MaintenanceRequests/CreateFault/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFault(int id, string faultDescription)
        {
            var maintenanceRequest = await _context.MaintenanceRequests.FindAsync(id);
            if (maintenanceRequest != null)
            {
                maintenanceRequest.FaultDescription = faultDescription;
                maintenanceRequest.Status = RequestStatus.Completed;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ConfirmedRequests)); // Redirecting to the list of confirmed requests
            }

            return NotFound();
        }

        // GET: MaintenanceRequests/ClientHistory
        public async Task<IActionResult> ClientHistory()
        {
            // Get the current user's ID (assuming you are using ASP.NET Core Identity)
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return NotFound(); // Handle cases where user is not found
            }

            // Fetch the maintenance requests related to the logged-in user
            var clientHistory = await _context.MaintenanceRequests
                .Where(m => m.UserId == Convert.ToInt32(userId)) // Ensure userId is compared as int
                .ToListAsync();

            return View(clientHistory);
        }



        public async Task<IActionResult> GeneratePdfReport()
        {
            var requests = await _context.MaintenanceRequests
                .Include(m => m.User)
                .ToListAsync();

            using (var stream = new MemoryStream())
            {
                Document pdfDoc = new Document();
                PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();

                pdfDoc.Add(new Paragraph("Maintenance Requests Report") { Font = new Font(Font.FontFamily.HELVETICA, 20, Font.BOLD) });

                // Create a table with 8 columns
                PdfPTable table = new PdfPTable(8);
                table.AddCell("Booking ID");
                table.AddCell("First Name");
                table.AddCell("Last Name");
                table.AddCell("Address");
                table.AddCell("Requested Date");
                table.AddCell("Approved Date");
                table.AddCell("Status");
                table.AddCell("Fault Description");

                foreach (var request in requests)
                {
                    table.AddCell(request.BookingID.ToString());
                    table.AddCell(request.FirstName);
                    table.AddCell(request.LastName);
                    table.AddCell(request.Address);
                    table.AddCell(request.RequestedDate.ToShortDateString());
                    table.AddCell(request.ApprovedDate?.ToShortDateString() ?? "N/A");
                    table.AddCell(request.Status.ToString());
                    table.AddCell(request.FaultDescription);
                }

                pdfDoc.Add(table);
                pdfDoc.Close();

                var fileName = "MaintenanceRequestsReport.pdf";
                return File(stream.ToArray(), "application/pdf", fileName);
            }
        }

            private bool MaintenanceRequestExists(int id)
        {
            return _context.MaintenanceRequests.Any(e => e.BookingID == id);
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
