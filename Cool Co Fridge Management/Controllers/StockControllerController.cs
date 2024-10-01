using Cool_Co_Fridge_Management.Data;
using Cool_Co_Fridge_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Cool_Co_Fridge_Management.Controllers
{
    public class StockControllerController : Controller
    {
        private readonly ApplicationDbContext _context;
        public StockControllerController(ApplicationDbContext dbContext)
        {
            _context= dbContext;
        }
        public IActionResult Fridge_Stock() /*this is the list*/
        {
            var stocklist = _context.stock
                .Include(s => s.Fridge_Type)
                .ToList();
            return View(stocklist);
        }
        public IActionResult Update_Items() /*Add items*/
        {
            ViewBag.FridgeTypes = new SelectList(_context.fridge_type, "FridgeTypeID", "FridgeType");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update_Items(Fridge_Stock stock)
        {
            _context.stock.Add(stock);
            _context.SaveChanges();
            ViewBag.FridgeTpyes = new SelectList(_context.fridge_type, "FridgeTypeID", "FridgeType");
            return View(stock);
            //return RedirectToAction("Fridge_Stock");
            //if (ModelState.IsValid)
            //{
                
            //}
            //ViewBag.FridgeTypes = new SelectList(_context.fridge_type, "FridgeTypeID", "FridgeType");
            //return View(stock);
        }
        public IActionResult LowStock() /*Should be linked w Customer service*/
        {
            return View();
        }

        public IActionResult CreatePurchaseRequest() 
        {
            return View();
        }

        public IActionResult PurchaseRequestList()/*might be linked with PM*/
        {
            return View();
        }
    }
}
