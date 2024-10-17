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
    public class FridgeConditionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FridgeConditionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FridgeConditions
        public async Task<IActionResult> Index()
        {
            return View(await _context.FridgeConditions.ToListAsync());
        }

        // GET: FridgeConditions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fridgeCondition = await _context.FridgeConditions
                .FirstOrDefaultAsync(m => m.ConditionID == id);
            if (fridgeCondition == null)
            {
                return NotFound();
            }

            return View(fridgeCondition);
        }

        // GET: FridgeConditions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FridgeConditions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConditionID,ConditionDesc")] FridgeCondition fridgeCondition)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fridgeCondition);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fridgeCondition);
        }

        // GET: FridgeConditions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fridgeCondition = await _context.FridgeConditions.FindAsync(id);
            if (fridgeCondition == null)
            {
                return NotFound();
            }
            return View(fridgeCondition);
        }

        // POST: FridgeConditions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConditionID,ConditionDesc")] FridgeCondition fridgeCondition)
        {
            if (id != fridgeCondition.ConditionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fridgeCondition);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FridgeConditionExists(fridgeCondition.ConditionID))
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
            return View(fridgeCondition);
        }

        // GET: FridgeConditions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fridgeCondition = await _context.FridgeConditions
                .FirstOrDefaultAsync(m => m.ConditionID == id);
            if (fridgeCondition == null)
            {
                return NotFound();
            }

            return View(fridgeCondition);
        }

        // POST: FridgeConditions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fridgeCondition = await _context.FridgeConditions.FindAsync(id);
            if (fridgeCondition != null)
            {
                _context.FridgeConditions.Remove(fridgeCondition);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FridgeConditionExists(int id)
        {
            return _context.FridgeConditions.Any(e => e.ConditionID == id);
        }
    }
}
