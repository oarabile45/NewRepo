using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cool_Co_Fridge_Management.Models;
using System.Linq;
using System.Threading.Tasks;
using Cool_Co_Fridge_Management.Data;

namespace Cool_Co_Fridge_Management.Controllers
{
    public class FridgeAllocationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FridgeAllocationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: View Requests and Create Fridge Allocation
        public async Task<IActionResult> ViewRequests()
        {
            var pendingRequests = await _context.FridgeRequests
                .Include(fr => fr.FridgeType)
                .Where(fr => fr.Status == "Pending")
                .ToListAsync();

            return View(pendingRequests);
        }

        // GET: Allocate Fridge
        public async Task<IActionResult> AllocateFridge(int fridgeRequestID)
        {
            var fridgeRequest = await _context.FridgeRequests
                .Include(fr => fr.FridgeType)
                .FirstOrDefaultAsync(fr => fr.FridgeRequestID == fridgeRequestID);

            if (fridgeRequest == null)
            {
                return NotFound();
            }

            var availableFridges = await _context.stock
                .Where(f => f.Availability && f.FridgeTypeID == fridgeRequest.FridgeTypeID)
                .ToListAsync();

            ViewData["Email"] = new SelectList(_context.users, "Email", "Email", fridgeRequest.Email);
            ViewData["FridgeID"] = new SelectList(availableFridges, "StockID", "ItemName");
            ViewData["FridgeRequestID"] = fridgeRequestID;

            return View();
        }

        // POST: Allocate Fridge
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AllocateFridge([Bind("FridgeAllocationID,Email,FridgeID,FridgeRequestID")] FridgeAllocation fridgeAllocation)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.users.FirstOrDefaultAsync(u => u.Email == fridgeAllocation.Email);
                if (user == null)
                {
                    ModelState.AddModelError("Email", "User with this email does not exist.");
                    return View(fridgeAllocation);
                }

                fridgeAllocation.Id = user.ID; // Set the user ID from the email
                fridgeAllocation.AllocationDate = DateTime.Now; // Set the allocation date

                // Check if the selected fridge is available
                var fridge = await _context.stock.FindAsync(fridgeAllocation.FridgeID);
                if (fridge == null || !fridge.Availability)
                {
                    ModelState.AddModelError("FridgeID", "The selected fridge is not available.");
                    return View(fridgeAllocation);
                }

                fridge.Availability = false; // Mark the fridge as not available
                _context.Update(fridge);
                
                // Update the fridge request status
                var fridgeRequest = await _context.FridgeRequests.FindAsync(fridgeAllocation.FridgeRequestID);
                if (fridgeRequest != null)
                {
                    fridgeRequest.Status = "Allocated"; // Update the status of the fridge request
                    _context.Update(fridgeRequest);
                }

                _context.Add(fridgeAllocation); // Add the fridge allocation
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ViewRequests));
            }

            // If we got this far, something failed; redisplay the form
            var request = await _context.FridgeRequests.FindAsync(fridgeAllocation.FridgeRequestID);
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
        public async Task<IActionResult> Edit(int id, [Bind("FridgeAllocationID,Email,FridgeID,Status,FridgeRequestID,AllocationDate")] FridgeAllocation fridgeAllocation)
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

                    fridgeAllocation.Id = user.ID; // Set the user ID from the email
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
                return RedirectToAction(nameof(ViewRequests));
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
            if (fridgeAllocation != null)
            {
                _context.FridgeAllocation.Remove(fridgeAllocation);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(ViewRequests));
        }

        private bool FridgeAllocationExists(int id)
        {
            return _context.FridgeAllocation.Any(e => e.FridgeAllocationID == id);
        }
    }
}
