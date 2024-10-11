using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cool_Co_Fridge_Management.Data;
using Cool_Co_Fridge_Management.Models;
using Cool_Co_Fridge_Management.ViewModels;

namespace Cool_Co_Fridge_Management.Controllers
{
    public class FridgeAllocationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FridgeAllocationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FridgeAllocations
        public async Task<IActionResult> Index()
        {
            // Display all fridge allocations
            var fridgeAllocations = await _context.FridgeAllocation
                .Include(f => f.users)
                .Include(f => f.Fridge_Stock)
                .Include(f => f.FridgeRequest)
                .ToListAsync();

            // Map FridgeAllocation to FridgeAllocationViewModel
            var viewModel = fridgeAllocations.Select(fa => new FridgeAllocationViewModel
            {
                FridgeAllocationID = fa.FridgeAllocationID,
                Email = fa.Email,
                ItemName = fa.Fridge_Stock.ItemName, // Assuming Fridge_Stock has an ItemName property
                Status = fa.Status,
            }).ToList();

            return View(viewModel); // Pass the view model to the view
        }

        // GET: View Requests and Create Fridge Allocation
        public async Task<IActionResult> ViewRequests()
        {
            // Fetch all pending fridge requests
            var pendingRequests = await _context.FridgeRequests
                .Include(fr => fr.FridgeType)
                .Where(fr => fr.Status == "Pending")
                .ToListAsync();

            return View(pendingRequests); // Display the pending requests
        }

        // GET: FridgeAllocations/Create
        public async Task<IActionResult> Create(int fridgeRequestID)
        {
            // Get the fridge request by ID
            var fridgeRequest = await _context.FridgeRequests
                .Include(fr => fr.FridgeType)
                .FirstOrDefaultAsync(fr => fr.FridgeRequestID == fridgeRequestID);

            if (fridgeRequest == null)
            {
                return NotFound();
            }

            // Fetch available fridges of the requested type
            var availableFridges = await _context.stock
                .Where(f => f.Availability && f.FridgeTypeID == fridgeRequest.FridgeTypeID)
                .ToListAsync();

            ViewData["Email"] = new SelectList(_context.users, "Email", "Email", fridgeRequest.Email);
            ViewData["FridgeID"] = new SelectList(availableFridges, "StockID", "ItemName");
            ViewData["FridgeRequestID"] = fridgeRequestID;

            return View();
        }

        // POST: FridgeAllocations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FridgeAllocationID,Email,FridgeID,Status,FridgeRequestID")] FridgeAllocation fridgeAllocation)
        {
            if (ModelState.IsValid)
            {
                // Find the user by email
                var user = await _context.users.FirstOrDefaultAsync(u => u.Email == fridgeAllocation.Email);
                if (user == null)
                {
                    ModelState.AddModelError("Email", "User with this email does not exist.");
                    return View(fridgeAllocation);
                }

                // Assign the fridge allocation to the user
                fridgeAllocation.Id = user.ID;
                _context.Add(fridgeAllocation);

                // Mark the fridge as unavailable
                var fridge = await _context.stock.FindAsync(fridgeAllocation.FridgeID);
                if (fridge != null)
                {
                    fridge.Availability = false;
                    _context.Update(fridge);
                }

                // Update the fridge request status
                var fridgeRequest = await _context.FridgeRequests.FindAsync(fridgeAllocation.FridgeRequestID);
                if (fridgeRequest != null)
                {
                    fridgeRequest.Status = "Allocated";
                    _context.Update(fridgeRequest);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Email"] = new SelectList(_context.users, "Email", "Email", fridgeAllocation.Email);
            ViewData["FridgeID"] = new SelectList(_context.stock.Where(f => f.Availability), "StockID", "ItemName", fridgeAllocation.FridgeID);
            ViewData["FridgeRequestID"] = fridgeAllocation.FridgeRequestID;
            return View(fridgeAllocation);
        }

        // GET: FridgeAllocations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fridgeAllocation = await _context.FridgeAllocation.FindAsync(id);
            if (fridgeAllocation == null)
            {
                return NotFound();
            }

            ViewData["Email"] = new SelectList(_context.users, "Email", "Email", fridgeAllocation.Email);
            ViewData["FridgeID"] = new SelectList(_context.stock.Where(f => f.Availability), "StockID", "ItemName", fridgeAllocation.FridgeID);
            ViewData["FridgeRequestID"] = new SelectList(_context.FridgeRequests, "FridgeRequestID", "Email", fridgeAllocation.FridgeRequestID);
            return View(fridgeAllocation);
        }

        // POST: FridgeAllocations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FridgeAllocationID,Email,FridgeID,Status,FridgeRequestID")] FridgeAllocation fridgeAllocation)
        {
            if (id != fridgeAllocation.FridgeAllocationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _context.users.FirstOrDefaultAsync(u => u.Email == fridgeAllocation.Email);
                    if (user == null)
                    {
                        ModelState.AddModelError("Email", "User with this email does not exist.");
                        return View(fridgeAllocation);
                    }

                    fridgeAllocation.Id = user.ID;
                    _context.Update(fridgeAllocation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FridgeAllocationExists(fridgeAllocation.FridgeAllocationID))
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

            ViewData["Email"] = new SelectList(_context.users, "Email", "Email", fridgeAllocation.Email);
            ViewData["FridgeID"] = new SelectList(_context.stock.Where(f => f.Availability), "StockID", "ItemName", fridgeAllocation.FridgeID);
            ViewData["FridgeRequestID"] = new SelectList(_context.FridgeRequests, "FridgeRequestID", "Email", fridgeAllocation.FridgeRequestID);
            return View(fridgeAllocation);
        }

        // GET: FridgeAllocations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fridgeAllocation = await _context.FridgeAllocation
                .Include(f => f.users)
                .Include(f => f.Fridge_Stock)
                .Include(f => f.FridgeRequest)
                .FirstOrDefaultAsync(m => m.FridgeAllocationID == id);
            if (fridgeAllocation == null)
            {
                return NotFound();
            }

            return View(fridgeAllocation);
        }

        // POST: FridgeAllocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fridgeAllocation = await _context.FridgeAllocation.FindAsync(id);
            _context.FridgeAllocation.Remove(fridgeAllocation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FridgeAllocationExists(int id)
        {
            return _context.FridgeAllocation.Any(e => e.FridgeAllocationID == id);
        }
    }
}
