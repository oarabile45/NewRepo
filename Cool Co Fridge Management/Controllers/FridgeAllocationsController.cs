using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cool_Co_Fridge_Management.Models;
using Cool_Co_Fridge_Management.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<IActionResult> AllocateFridge(int fridgeRequestId)
        {
            var fridgeRequest = await _context.FridgeRequests
                .Include(fr => fr.FridgeType)
                .FirstOrDefaultAsync(fr => fr.FridgeRequestID == fridgeRequestId);

            if (fridgeRequest == null)
            {
                return NotFound();
            }

            var availableFridges = await _context.stock
                .Where(f => f.Availability && f.FridgeTypeID == fridgeRequest.FridgeTypeID)
                .ToListAsync();

            ViewBag.AvailableFridges = new SelectList(availableFridges, "StockID", "ItemName");

            var fridgeAllocation = new FridgeAllocation
            {
                FridgeRequestID = fridgeRequestId,
                Email = fridgeRequest.Email // This is optional depending on your requirement
            };

            return View(fridgeAllocation);
        }

        // POST: Allocate Fridge
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AllocateFridge(FridgeAllocation fridgeAllocation)
        {
            // Ensure the model state is valid
           // if (ModelState.IsValid)
            {
                // Get the user based on the provided email
                var user = await _context.users.FirstOrDefaultAsync(u => u.Email == fridgeAllocation.Email);
                if (user != null)
                {
                    // Set the user ID and allocation date
                    fridgeAllocation.Id = user.ID;
                    fridgeAllocation.AllocationDate = DateTime.UtcNow;

                    // Set the initial status for the allocation
                    fridgeAllocation.Status = "Allocated"; // Set a default status here

                    // Find the selected fridge and mark it as unavailable
                    var fridge = await _context.stock.FindAsync(fridgeAllocation.FridgeID);
                    if (fridge != null && fridge.Availability)
                    {
                        fridge.Availability = false;
                        _context.stock.Update(fridge);

                        // Update the fridge request status to "Allocated"
                        var existingFridgeRequest = await _context.FridgeRequests.FindAsync(fridgeAllocation.FridgeRequestID);
                        if (existingFridgeRequest != null)
                        {
                            existingFridgeRequest.Status = "Allocated"; // Assuming the fridge request needs to be updated too
                            _context.FridgeRequests.Update(existingFridgeRequest);
                        }

                        // Add the fridge allocation and save changes
                        _context.FridgeAllocation.Add(fridgeAllocation);
                        await _context.SaveChangesAsync();

                        TempData["SuccessMessage"] = "Fridge allocation completed successfully.";
                        return RedirectToAction(nameof(AllocatedFridges));
                    }
                    else
                    {
                        ModelState.AddModelError("FridgeID", "The selected fridge is not available.");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "User with this email does not exist.");
                }
            }

            //// If allocation is not successful or model state is invalid, reload the allocation view
            //var fridgeRequest = await _context.FridgeRequests
            //    .Include(fr => fr.FridgeType)
            //    .FirstOrDefaultAsync(fr => fr.FridgeRequestID == fridgeAllocation.FridgeRequestID);

            //if (fridgeRequest != null)
            //{
            //    var availableFridges = await _context.stock
            //        .Where(f => f.Availability && f.FridgeTypeID == fridgeRequest.FridgeTypeID)
            //        .ToListAsync();
            //    ViewBag.AvailableFridges = new SelectList(availableFridges, "StockID", "ItemName");
            //}

            ViewData["Email"] = fridgeAllocation.Email;
            return View(fridgeAllocation);
        }



        // GET: Allocated Fridges
        public async Task<IActionResult> AllocatedFridges()
        {
            var allocatedFridges = await _context.FridgeAllocation
                .Include(fa => fa.Fridge_Stock)
                .Include(fa => fa.users)
                .Select(fa => new AllocatedFridgeViewModel
                {
                    FridgeAllocationID = fa.FridgeAllocationID,
                    Email = fa.users.Email,
                    ItemName = fa.Fridge_Stock.ItemName,
                    FridgeType = fa.Fridge_Stock.Fridge_Type.FridgeType,
                    AllocationDate = fa.AllocationDate
                })
                .ToListAsync();

            return View(allocatedFridges);
        }

        // GET: Edit Fridge Allocation
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

        // POST: Edit Fridge Allocation
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
                return RedirectToAction(nameof(ViewRequests));
            }

            ViewData["Email"] = new SelectList(_context.users, "Email", "Email", fridgeAllocation.Email);
            ViewData["FridgeID"] = new SelectList(_context.stock.Where(f => f.Availability), "StockID", "ItemName", fridgeAllocation.FridgeID);
            ViewData["FridgeRequestID"] = new SelectList(_context.FridgeRequests, "FridgeRequestID", "Email", fridgeAllocation.FridgeRequestID);
            return View(fridgeAllocation);
        }

        // GET: Delete Fridge Allocation
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

        // POST: Delete Fridge Allocation
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fridgeAllocation = await _context.FridgeAllocation.FindAsync(id);
            _context.FridgeAllocation.Remove(fridgeAllocation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ViewRequests));
        }

        private bool FridgeAllocationExists(int id)
        {
            return _context.FridgeAllocation.Any(e => e.FridgeAllocationID == id);
        }
    }
}
