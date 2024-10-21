using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cool_Co_Fridge_Management.Models;
using System.Linq;
using System.Threading.Tasks;
using Cool_Co_Fridge_Management.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cool_Co_Fridge_Management.Controllers
{
    public class FridgeRequestController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FridgeRequestController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CustomerFridgeReq
        public IActionResult CustomerFridgeReq(string email)
        {
            // Fetch all requests
            var requests = _context.FridgeRequests.Include(fr => fr.FridgeType).ToList();

            // If an email is provided, filter the requests
            if (!string.IsNullOrEmpty(email))
            {
                requests = requests.Where(r => r.Email.Equals(email, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return View(requests);
        }

        // GET: FridgeRequests
        public async Task<IActionResult> Index()
        {
            var fridgeRequests = await _context.FridgeRequests
                .Include(fr => fr.FridgeType)
                .ToListAsync();

            return View(fridgeRequests);
        }

        // GET: FridgeRequests/Create
        public IActionResult Create()
        {
            ViewData["FridgeTypeID"] = new SelectList(_context.fridge_type, "FridgeTypeID", "FridgeType");
            return View();
        }

        // POST: FridgeRequests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FridgeRequestID,Email,FridgeTypeID,Status")] FridgeRequest fridgeRequest)
        {
            // if (ModelState.IsValid)
            {
                fridgeRequest.Status = "Pending";
                _context.Add(fridgeRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(CustomerIndex));

                // This line seems unreachable. You might want to use it in the appropriate place.
                // return RedirectToAction(nameof(Details), new { id = fridgeRequest.FridgeRequestID });
            }

            ViewData["FridgeTypeID"] = new SelectList(_context.fridge_type, "FridgeTypeID", "FridgeType", fridgeRequest.FridgeTypeID);
            return View(fridgeRequest);
        }

        // New action to display the created request
        public async Task<IActionResult> ShowRequest(int id)
        {
            var fridgeRequest = await _context.FridgeRequests
                .Include(fr => fr.FridgeType)
                .FirstOrDefaultAsync(fr => fr.FridgeRequestID == id);

            if (fridgeRequest == null)
            {
                return NotFound();
            }

            return View(fridgeRequest);
        }

        // POST: FridgeRequests/DeclineFridgeRequest
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeclineFridgeRequest(int fridgeRequestId)
        {
            var fridgeRequest = await _context.FridgeRequests.FindAsync(fridgeRequestId);
            if (fridgeRequest == null)
            {
                return NotFound();
            }

            fridgeRequest.Status = "Declined";

            try
            {
                _context.Update(fridgeRequest);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FridgeRequestExists(fridgeRequestId))
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

        // GET: FridgeRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fridgeRequest = await _context.FridgeRequests.FindAsync(id);
            if (fridgeRequest == null)
            {
                return NotFound();
            }

            ViewData["FridgeTypeID"] = new SelectList(_context.fridge_type, "FridgeTypeID", "FridgeType", fridgeRequest.FridgeTypeID);
            return View(fridgeRequest);
        }

        // POST: FridgeRequests/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FridgeRequestID,Email,FridgeTypeID,Status")] FridgeRequest fridgeRequest)
        {
            if (id != fridgeRequest.FridgeRequestID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fridgeRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FridgeRequestExists(fridgeRequest.FridgeRequestID))
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

            ViewData["FridgeTypeID"] = new SelectList(_context.fridge_type, "FridgeTypeID", "FridgeType", fridgeRequest.FridgeTypeID);
            return View(fridgeRequest);
        }

        // GET: FridgeRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fridgeRequest = await _context.FridgeRequests
                .Include(fr => fr.FridgeType)
                .FirstOrDefaultAsync(m => m.FridgeRequestID == id);

            if (fridgeRequest == null)
            {
                return NotFound();
            }

            return View(fridgeRequest);
        }

        // GET: FridgeRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fridgeRequest = await _context.FridgeRequests
                .Include(fr => fr.FridgeType)
                .FirstOrDefaultAsync(m => m.FridgeRequestID == id);
            if (fridgeRequest == null)
            {
                return NotFound();
            }

            return View(fridgeRequest);
        }

        // POST: FridgeRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fridgeRequest = await _context.FridgeRequests.FindAsync(id);
            if (fridgeRequest != null)
            {
                _context.FridgeRequests.Remove(fridgeRequest);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult AcceptedRequests()
        {
            var acceptedRequests = _context.FridgeRequests
                .Where(r => r.Status == "Allocated")
                .Include(fr => fr.FridgeType)
                .ToList();

            // Debugging output
            Console.WriteLine($"Accepted Requests Count: {acceptedRequests.Count}");

            return View(acceptedRequests);
        }

        public IActionResult DeclinedRequests()
        {
            var declinedRequests = _context.FridgeRequests
                .Where(r => r.Status == "Declined")
                .Include(fr => fr.FridgeType)
                .ToList();

            // Debugging output
            Console.WriteLine($"Declined Requests Count: {declinedRequests.Count}");

            return View(declinedRequests);
        }

        // GET: CustomerIndex
        public async Task<IActionResult> CustomerIndex()
        {
            var fridgeRequests = await _context.FridgeRequests
                .Include(fr => fr.FridgeType)
                .ToListAsync();

            return View(fridgeRequests);
        }

        private bool FridgeRequestExists(int id)
        {
            return _context.FridgeRequests.Any(e => e.FridgeRequestID == id);
        }
    }
}
