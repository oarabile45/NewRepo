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
    public class FaultTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FaultTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FaultTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.faultTypes.ToListAsync());
        }

        // GET: FaultTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faultType = await _context.faultTypes
                .FirstOrDefaultAsync(m => m.FaultTypeID == id);
            if (faultType == null)
            {
                return NotFound();
            }

            return View(faultType);
        }

        // GET: FaultTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FaultTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FaultTypeID,FaultTypeDesc")] FaultType faultType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(faultType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(faultType);
        }

        // GET: FaultTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faultType = await _context.faultTypes.FindAsync(id);
            if (faultType == null)
            {
                return NotFound();
            }
            return View(faultType);
        }

        // POST: FaultTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FaultTypeID,FaultTypeDesc")] FaultType faultType)
        {
            if (id != faultType.FaultTypeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(faultType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FaultTypeExists(faultType.FaultTypeID))
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
            return View(faultType);
        }

        // GET: FaultTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faultType = await _context.faultTypes
                .FirstOrDefaultAsync(m => m.FaultTypeID == id);
            if (faultType == null)
            {
                return NotFound();
            }

            return View(faultType);
        }

        // POST: FaultTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var faultType = await _context.faultTypes.FindAsync(id);
            if (faultType != null)
            {
                _context.faultTypes.Remove(faultType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FaultTypeExists(int id)
        {
            return _context.faultTypes.Any(e => e.FaultTypeID == id);
        }
    }
}
