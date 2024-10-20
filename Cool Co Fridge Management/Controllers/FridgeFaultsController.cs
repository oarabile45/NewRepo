using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cool_Co_Fridge_Management.Data;
using Cool_Co_Fridge_Management.Models;

namespace Cool_Co_Fridge_Management.Controllers
{
    public class FridgeFaultsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FridgeFaultsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FridgeFaults
        public async Task<IActionResult> Index()
        {
            var pendingStatus = await _context.statuses
				.Where(y => y.StatusDesc == "Pending").FirstOrDefaultAsync();

			var applicationDbContext = _context.fridgeFaults
                .Include(f => f.faultType)
                .Include(f => f.status)
				.Where(f => f.StatusID == pendingStatus!.StatusID);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> AcceptedFaults()
        {
            var approveStatus = await _context.statuses
                .Where(y => y.StatusDesc == "Accepted").FirstOrDefaultAsync();

            var applicationDbContext = _context.fridgeFaults
                .Include(f => f.faultType)
                .Include(f => f.status)
                .Where(f => f.StatusID == approveStatus!.StatusID);

            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> RejectedFaults()
        {
            var rejectedStatus = await _context.statuses
                .Where(y => y.StatusDesc == "Rejected").FirstOrDefaultAsync();

            var applicationDbContext = _context.fridgeFaults
                .Include(f => f.faultType)
                .Include(f => f.status)
                .Where(f => f.StatusID == rejectedStatus!.StatusID);

            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> ScheduleRepairIndex()
        {
            var approveStatus = await _context.statuses
                .Where(y => y.StatusDesc == "Accepted").FirstOrDefaultAsync();

            var applicationDbContext = _context.fridgeFaults
                .Include(f => f.faultType)
                .Include(f => f.status)
                .Where(f => f.StatusID == approveStatus!.StatusID)
                .OrderBy(r => r.RepairDate);

            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> ApproveFaultIndex()
        {
			var pendingStatus = await _context.statuses
				.Where(y => y.StatusDesc == "Pending").FirstOrDefaultAsync();

			var applicationDbContext = _context.fridgeFaults
				.Include(f => f.faultType)
				.Include(f => f.status)
				.Where(f => f.StatusID == pendingStatus!.StatusID);
			return View(await applicationDbContext.ToListAsync());
		}

        // GET: FridgeFaults/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fridgeFault = await _context.fridgeFaults
                .Include(f => f.faultType)
                .Include(f => f.status)
                .FirstOrDefaultAsync(m => m.FridgeFaultID == id);
            if (fridgeFault == null)
            {
                return NotFound();
            }

            return View(fridgeFault);
        }

        // GET: FridgeFaults/Create
        public IActionResult Create()
        {
            ViewData["FaultTypeID"] = new SelectList(_context.faultTypes, "FaultTypeID", "FaultTypeDesc");
            ViewData["StatusID"] = new SelectList(_context.statuses
                .Where(y => y.StatusDesc == "Pending"), "StatusID", "StatusDesc");
            return View();
        }

        // POST: FridgeFaults/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FridgeFault fridgeFault)
        {
			fridgeFault.ReportedOn = DateTime.Now;
			var pendingStatus = await _context.statuses
				.Where(y => y.StatusDesc == "Pending").FirstOrDefaultAsync();

			fridgeFault.StatusID = pendingStatus.StatusID;

            //if (ModelState.IsValid)
            //{
            _context.Add(fridgeFault);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            ViewData["FaultTypeID"] = new SelectList(_context.faultTypes, "FaultTypeID", "FaultTypeDesc", fridgeFault.FaultTypeID);
            ViewData["StatusID"] = new SelectList(_context.statuses
                .Where(y => y.StatusDesc == "Pending"), "StatusID", "StatusDesc", fridgeFault.StatusID);
            return View(fridgeFault);
        }

        [HttpGet]
        public async Task<IActionResult> ApproveFault(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fridgeFault = await _context.fridgeFaults
                .Include(f => f.faultType)
                .Include(f => f.status)
                .FirstOrDefaultAsync(m => m.FridgeFaultID == id);
            if (fridgeFault == null)
            {
                return NotFound();
            }

            ViewData["FaultTypeID"] = new SelectList(_context.faultTypes, "FaultTypeID", "FaultTypeDesc");
            ViewData["StatusID"] = new SelectList(_context.statuses
                .Where(y => y.StatusDesc == "Pending"), "StatusID", "StatusDesc");

            return View(fridgeFault);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveFault(int? id, FridgeFault fault)
        {
            var approveStatus = await _context.statuses
                .Where(y => y.StatusDesc == "Accepted").FirstOrDefaultAsync();

            if (id == null)
            {
                return NotFound();
            }

            var fridgeFault = await _context.fridgeFaults
                .Include(f => f.faultType)
                .Include(f => f.status)
                .FirstOrDefaultAsync(m => m.FridgeFaultID == id);

            if (fridgeFault == null)
            {
                return NotFound();
            }

            fridgeFault.StatusID = approveStatus.StatusID;

            _context.Update(fridgeFault);
            await _context.SaveChangesAsync();

            ViewData["FaultTypeID"] = new SelectList(_context.faultTypes, "FaultTypeID", "FaultTypeDesc");
            ViewData["StatusID"] = new SelectList(_context.statuses
                .Where(y => y.StatusDesc == "Accepted"), "StatusID", "StatusDesc");

            return RedirectToAction(nameof(ApproveFaultIndex));
        }

        [HttpGet]
        public async Task<IActionResult> RejectFault(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fridgeFault = await _context.fridgeFaults
                .Include(f => f.faultType)
                .Include(f => f.status)
                .FirstOrDefaultAsync(m => m.FridgeFaultID == id);
            if (fridgeFault == null)
            {
                return NotFound();
            }

            ViewData["FaultTypeID"] = new SelectList(_context.faultTypes, "FaultTypeID", "FaultTypeDesc");
            ViewData["StatusID"] = new SelectList(_context.statuses
                .Where(y => y.StatusDesc == "Pending"), "StatusID", "StatusDesc");

            return View(fridgeFault);
        }

        [HttpPost]
        public async Task<IActionResult> RejectFault(int? id, FridgeFault fault)
        {
            var rejectStatus = await _context.statuses
                .Where(y => y.StatusDesc == "Rejected").FirstOrDefaultAsync();

            if (id == null)
            {
                return NotFound();
            }

            var fridgeFault = await _context.fridgeFaults
                .Include(f => f.faultType)
                .Include(f => f.status)
                .FirstOrDefaultAsync(m => m.FridgeFaultID == id);

            if (fridgeFault == null)
            {
                return NotFound();
            }

            fridgeFault.StatusID = rejectStatus.StatusID;

            _context.Update(fridgeFault);
            await _context.SaveChangesAsync();

            ViewData["FaultTypeID"] = new SelectList(_context.faultTypes, "FaultTypeID", "FaultTypeDesc");
            ViewData["StatusID"] = new SelectList(_context.statuses
                .Where(y => y.StatusDesc == "Rejected"), "StatusID", "StatusDesc");

            return RedirectToAction(nameof(ApproveFaultIndex));
        }

        [HttpGet]
        public async Task<IActionResult> ScheduleRepair(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fridgeFault = await _context.fridgeFaults
                .Include(f => f.faultType)
                .Include(f => f.status)
                .FirstOrDefaultAsync(m => m.FridgeFaultID == id);
            if (fridgeFault == null)
            {
                return NotFound();
            }

            ViewData["FaultTypeID"] = new SelectList(_context.faultTypes, "FaultTypeID", "FaultTypeDesc");
            ViewData["StatusID"] = new SelectList(_context.statuses
                .Where(y => y.StatusDesc == "Accepted"), "StatusID", "StatusDesc");

            return View(fridgeFault);
        }

        [HttpPost]
        public async Task<IActionResult> ScheduleRepair(int? id, FridgeFault fault)
        {
            var approveStatus = await _context.statuses
                .Where(y => y.StatusDesc == "Accepted").FirstOrDefaultAsync();

            if (id == null)
            {
                return NotFound();
            }

            var fridgeFault = await _context.fridgeFaults
                .Include(f => f.faultType)
                .Include(f => f.status)
                .FirstOrDefaultAsync(m => m.FridgeFaultID == id);

            if (fridgeFault == null)
            {
                return NotFound();
            }

            fridgeFault.StatusID = approveStatus!.StatusID;

			fridgeFault.RepairDate = fault.RepairDate;

			_context.Update(fridgeFault);
            await _context.SaveChangesAsync();

            ViewData["FaultTypeID"] = new SelectList(_context.faultTypes, "FaultTypeID", "FaultTypeDesc");
            ViewData["StatusID"] = new SelectList(_context.statuses
                .Where(y => y.StatusDesc == "Accepted"), "StatusID", "StatusDesc");

            return RedirectToAction(nameof(ScheduleRepairIndex));
        }

        public async Task<IActionResult> FaultTechIndex()
        {
            var approveStatus = await _context.statuses
                 .Where(y => y.StatusDesc == "Accepted").FirstOrDefaultAsync();

			var applicationDbContext = _context.fridgeFaults
				.Include(f => f.faultType)
				.Include(f => f.status)
				.Where(f => f.StatusID == approveStatus!.StatusID)
				.OrderBy(r => r.RepairDate);

			return View(await applicationDbContext.ToListAsync());
        }

        //public async Task<IActionResult> PendingFilteredCount(FridgeFault fridgeFault)
        //{
        //    var pendingStatus = await _context.statuses
        //        .Where(y => y.StatusDesc == "Pending").FirstOrDefaultAsync();
        //    int pendingFaultsCount = _context.fridgeFaults.Count();

        //    fridgeFault.StatusID = pendingStatus!.StatusID;

        //    return View(pendingFaultsCount);
        //}

        public async Task<IActionResult> FridgeConditionIndex()
        {
            var fixedCondition = await _context.FridgeConditions
                .Where(y => y.ConditionDesc == "Fixed").FirstOrDefaultAsync();

            var beyondRepairCondition = await _context.FridgeConditions
                .Where(y => y.ConditionDesc == "Beyond Repair").FirstOrDefaultAsync();

            var today = DateOnly.FromDateTime(DateTime.Today);

            var pastRepairs = await _context.fridgeFaults
                .Include(f => f.faultType)
                .Include(f => f.status)
                .Where(r => r.RepairDate != null && r.RepairDate <= today)
                .OrderBy(r => r.RepairDate)
                .ToListAsync();

            return View(pastRepairs);
        }

        [HttpGet]
        public async Task<IActionResult> FixedFridgeCondition(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fridgeFault = await _context.fridgeFaults
                .Include(f => f.faultType)
                .Include(f => f.Condition)
                .FirstOrDefaultAsync(m => m.FridgeFaultID == id);
            if (fridgeFault == null)
            {
                return NotFound();
            }

            ViewData["FaultTypeID"] = new SelectList(_context.faultTypes, "FaultTypeID", "FaultTypeDesc");
            ViewData["StatusID"] = new SelectList(_context.FridgeConditions
                .Where(y => y.ConditionDesc == "Fixed"), "ConditionID", "ConditionDesc");

            return View(fridgeFault);
        }

        [HttpPost]
        public async Task<IActionResult> FixedFridgeCondition(int? id, FridgeFault fault)
        {
            var fixedCondition = await _context.FridgeConditions
                .Where(y => y.ConditionDesc == "Fixed").FirstOrDefaultAsync();

            if (id == null)
            {
                return NotFound();
            }

            var fridgeFault = await _context.fridgeFaults
                .Include(f => f.faultType)
                .Include(f => f.status)
                .FirstOrDefaultAsync(m => m.FridgeFaultID == id);

            if (fridgeFault == null)
            {
                return NotFound();
            }

            fridgeFault.ConditionID = fixedCondition!.ConditionID;

            _context.Update(fridgeFault);
            await _context.SaveChangesAsync();

            ViewData["FaultTypeID"] = new SelectList(_context.faultTypes, "FaultTypeID", "FaultTypeDesc");
            ViewData["StatusID"] = new SelectList(_context.FridgeConditions
                .Where(y => y.ConditionDesc == "Fixed"), "ConditionID", "ConditionDesc");

            return RedirectToAction(nameof(FridgeConditionIndex));
        }

        [HttpGet]
        public async Task<IActionResult> BeyondRepairFridgeCondition(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fridgeFault = await _context.fridgeFaults
                .Include(f => f.faultType)
                .Include(f => f.Condition)
                .FirstOrDefaultAsync(m => m.FridgeFaultID == id);
            if (fridgeFault == null)
            {
                return NotFound();
            }

            ViewData["FaultTypeID"] = new SelectList(_context.faultTypes, "FaultTypeID", "FaultTypeDesc");
            ViewData["StatusID"] = new SelectList(_context.FridgeConditions
                .Where(y => y.ConditionDesc == "Beyond Repair"), "ConditionID", "ConditionDesc");

            return View(fridgeFault);
        }

        [HttpPost]
        public async Task<IActionResult> BeyondRepairFridgeCondition(int? id, FridgeFault fault)
        {
            var beyondRepairCondition = await _context.FridgeConditions
                .Where(y => y.ConditionDesc == "Beyond Repair").FirstOrDefaultAsync();

            if (id == null)
            {
                return NotFound();
            }

            var fridgeFault = await _context.fridgeFaults
                .Include(f => f.faultType)
                .Include(f => f.status)
                .FirstOrDefaultAsync(m => m.FridgeFaultID == id);

            if (fridgeFault == null)
            {
                return NotFound();
            }

            fridgeFault.ConditionID = beyondRepairCondition!.ConditionID;

            _context.Update(fridgeFault);
            await _context.SaveChangesAsync();

            ViewData["FaultTypeID"] = new SelectList(_context.faultTypes, "FaultTypeID", "FaultTypeDesc");
            ViewData["StatusID"] = new SelectList(_context.FridgeConditions
                .Where(y => y.ConditionDesc == "Beyond Repair"), "ConditionID", "ConditionDesc");

            return RedirectToAction(nameof(FridgeConditionIndex));
        }

        // GET: FridgeFaults/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fridgeFault = await _context.fridgeFaults.FindAsync(id);
            if (fridgeFault == null)
            {
                return NotFound();
            }
            ViewData["FaultTypeID"] = new SelectList(_context.faultTypes, "FaultTypeID", "FaultTypeDesc", fridgeFault.FaultTypeID);
            ViewData["StatusID"] = new SelectList(_context.statuses, "StatusID", "StatusDesc", fridgeFault.StatusID);
            return View(fridgeFault);
        }

        // POST: FridgeFaults/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FridgeFault fridgeFault)
        {
            if (id != fridgeFault.FridgeFaultID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fridgeFault);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FridgeFaultExists(fridgeFault.FridgeFaultID))
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
            ViewData["FaultTypeID"] = new SelectList(_context.faultTypes, "FaultTypeID", "FaultTypeDesc", fridgeFault.FaultTypeID);
            ViewData["StatusID"] = new SelectList(_context.statuses, "StatusID", "StatusDesc", fridgeFault.StatusID);
            return View(fridgeFault);
        }

        // GET: FridgeFaults/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fridgeFault = await _context.fridgeFaults
                .Include(f => f.faultType)
                .Include(f => f.status)
                .FirstOrDefaultAsync(m => m.FridgeFaultID == id);
            if (fridgeFault == null)
            {
                return NotFound();
            }

            return View(fridgeFault);
        }

        // POST: FridgeFaults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fridgeFault = await _context.fridgeFaults.FindAsync(id);
            if (fridgeFault != null)
            {
                _context.fridgeFaults.Remove(fridgeFault);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FridgeFaultExists(int id)
        {
            return _context.fridgeFaults.Any(e => e.FridgeFaultID == id);
        }
    }
}
