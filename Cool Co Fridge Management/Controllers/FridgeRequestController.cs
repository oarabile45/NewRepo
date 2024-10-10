using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cool_Co_Fridge_Management.Models;
using System.Linq;
using System.Threading.Tasks;
using Cool_Co_Fridge_Management.Data;

namespace Cool_Co_Fridge_Management.Controllers
{
    public class FridgeRequestController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FridgeRequestController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FridgeRequest
        public async Task<IActionResult> Index()
        {
            var fridgeRequests = await _context.FridgeRequests
                                               .Include(fr => fr.FridgeType) // No longer filtered by logged-in user
                                               .ToListAsync();
            return View(fridgeRequests);
        }

        // GET: FridgeRequest/Create
        public IActionResult Create()
        {
            ViewBag.FridgeTypeID = new SelectList(_context.fridge_type, "FridgeTypeID", "FridgeType");
            return View();
        }

        // POST: FridgeRequest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,FridgeTypeID")] FridgeRequest fridgeRequest)
        {
            fridgeRequest.Status = "Pending"; // Default status

            // if (ModelState.IsValid)
            {
                _context.Add(fridgeRequest);
                await _context.SaveChangesAsync();

                // Set success message
                TempData["SuccessMessage"] = "Your fridge request was successful!";

                return RedirectToAction(nameof(Index));
            }

            ViewBag.FridgeTypeID = new SelectList(_context.fridge_type, "FridgeTypeID", "FridgeType", fridgeRequest.FridgeTypeID);
            return View(fridgeRequest);
        }

        // GET: FridgeRequest/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fridgeRequest = await _context.FridgeRequests.FindAsync(id);
            if (fridgeRequest == null) // No longer checks if the user owns the request
            {
                return NotFound();
            }

            ViewBag.FridgeTypeID = new SelectList(_context.fridge_type, "FridgeTypeID", "FridgeType", fridgeRequest.FridgeTypeID);
            return View(fridgeRequest);
        }

        // POST: FridgeRequest/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FridgeRequestID,FridgeTypeID")] FridgeRequest fridgeRequest)
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

            ViewBag.FridgeTypeID = new SelectList(_context.fridge_type, "FridgeTypeID", "FridgeType", fridgeRequest.FridgeTypeID);
            return View(fridgeRequest);
        }

        // GET: FridgeRequest/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fridgeRequest = await _context.FridgeRequests
                                               .Include(fr => fr.FridgeType)
                                               .FirstOrDefaultAsync(m => m.FridgeRequestID == id); // No longer checks if the user owns the request
            if (fridgeRequest == null)
            {
                return NotFound();
            }

            return View(fridgeRequest);
        }

        // POST: FridgeRequest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fridgeRequest = await _context.FridgeRequests.FindAsync(id);
            if (fridgeRequest == null) // No longer checks if the user owns the request
            {
                return NotFound();
            }

            _context.FridgeRequests.Remove(fridgeRequest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FridgeRequestExists(int id)
        {
            return _context.FridgeRequests.Any(e => e.FridgeRequestID == id);
        }
    }
}