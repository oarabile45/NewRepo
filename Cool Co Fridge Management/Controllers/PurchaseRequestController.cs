using Cool_Co_Fridge_Management.Data;
using Microsoft.AspNetCore.Mvc;
using Cool_Co_Fridge_Management.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cool_Co_Fridge_Management.Attributes;

namespace Cool_Co_Fridge_Management.Controllers
{
    public class PurchaseRequestController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public PurchaseRequestController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Requests() //list of requests placed. Sil
        {
            var requests = _dbContext.PurchaseRequests.Include(p => p.FridgeType).Include(p => p.OrderStatus).ToList();
            if (!requests.Any())
            {
                TempData["Error Message"] = "No purchase requests found";
            }
            return View(requests);
        }
        //[RoleAuthorize("Stock Controller")]
        public IActionResult NewRequest() {
            var orderstatus = _dbContext.orderStatus.ToList();
            ViewBag.OrderStatus = new SelectList(orderstatus, "OrderStatusId", "OrderDesc");
            var type = _dbContext.fridge_type.ToList();
            ViewBag.Types = new SelectList(type, "FridgeTypeID", "FridgeType");

            var purchase = new PurchaseRequest
            {
                OrderStatusId = orderstatus.FirstOrDefault(p => p.OrderDesc == "Pending")?.OrderStatusId??2
            };
            return View(purchase);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NewRequest(PurchaseRequest purchase)
        {
            if (!ModelState.IsValid)
            {
                // Debugging model state errors
                var errors = ModelState.Where(x => x.Value.Errors.Count > 0)
                                       .Select(x => new
                                       {
                                           Key = x.Key,
                                           Errors = x.Value.Errors.Select(e => e.ErrorMessage).ToList()
                                       })
                                       .ToList();
                foreach (var error in errors)
                {
                    Console.WriteLine($"Field: {error.Key}, Errors: {string.Join(", ", error.Errors)}");
                }
            }
            if (ModelState.IsValid)
            {
                _dbContext.Add(purchase);
                _dbContext.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("Requests");
            }

            var orderstatus = _dbContext.orderStatus.ToList();
            ViewBag.OrderStatus = new SelectList(orderstatus, "OrderStatusId", "OrderDesc");
            var type = _dbContext.fridge_type.ToList();
            ViewBag.Types = new SelectList(type, "FridgeTypeID", "FridgeType");

            return View(purchase);
        }
        //[RoleAuthorize("Purchasing Manager")]
        public IActionResult UpdateRequest(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var request = _dbContext.PurchaseRequests.Include(p => p.FridgeType).FirstOrDefault(r => r.PurchaseRequestId == id);
            if(request == null)
            {
                return NotFound();
            }
            var orderstatus = _dbContext.orderStatus.ToList();
            ViewBag.OrderStatus = new SelectList(orderstatus, "OrderStatusId", "OrderDesc");

            return View(request);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateRequest(int id, PurchaseRequest request)
        {
            if (!ModelState.IsValid)
            {
                var orderstatus = _dbContext.orderStatus.ToList();
                ViewBag.OrderStatus = new SelectList(orderstatus, "OrderStatusId", "OrderDesc");
                return View(request);
            }

            if(request == null)
            {
                return NotFound();
            }

            _dbContext.Update(request);
            _dbContext.SaveChanges();
            //return RedirectToAction(nameof(Index));

            return View(request);
        }
        public IActionResult RequestDetails(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var request = _dbContext.PurchaseRequests.FirstOrDefault(m => m.PurchaseRequestId == id);
            if(request == null)
            {
                return NotFound();
            }
            return View(request);
        }


    }
}
