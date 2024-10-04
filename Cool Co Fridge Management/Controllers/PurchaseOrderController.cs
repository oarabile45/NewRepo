using Microsoft.AspNetCore.Mvc;
using Cool_Co_Fridge_Management.Models;
using Cool_Co_Fridge_Management.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace Cool_Co_Fridge_Management.Controllers
{
    public class PurchaseOrderController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;
        public PurchaseOrderController(ApplicationDbContext _applicationDbContext)
        {
            applicationDbContext = _applicationDbContext;
        }
        public IActionResult Index()
        {
            var orders = applicationDbContext.orders
                .Include(o => o.Fridge_Type)
                .Include(o => o.Supplier)
                .Include(o => o.OrderStatus)
                .OrderByDescending(o => o.OrderedDate)
                .ToList();
            return View(orders);
        }

        public IActionResult CreateNewOrder()
        {
            var Suppliers = applicationDbContext.suppliers.ToList();
            ViewBag.Suppliers = new SelectList(Suppliers, "SupplierId", "SupplierName");

            var Types = applicationDbContext.fridge_type.ToList();
            ViewBag.Types = new SelectList(Types, "FridgeTypeID", "FridgeType");

            var orderstatus = applicationDbContext.orderStatus.ToList();
            ViewBag.OrderStatus = new SelectList(orderstatus, "OrderStatusId", "OrderDesc");

            var model = new PurchaseOrder
            {
                OrderStatusId = orderstatus.FirstOrDefault(s => s.OrderDesc == "Pending")?.OrderStatusId??2
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateNewOrder(PurchaseOrder pOrder)
        {
            if (ModelState.IsValid)
            {
                if (pOrder.SupplierId == 0 || pOrder.FridgeTypeID == 0)
                {
                    ModelState.AddModelError("", "Please select valid Supplier and Fridge Type.");
                    RepopulateViewBags();
                    return View(pOrder);
                }
                applicationDbContext.Add(pOrder);
                applicationDbContext.SaveChanges();
                TempData["SuccessMessage"] = "Order sent successfully";
                ModelState.Clear();
                return View(new PurchaseOrder());
            }
            RepopulateViewBags();
            return View(pOrder);
        }
        private void RepopulateViewBags()
        {
            var Suppliers = applicationDbContext.suppliers.ToList();
            ViewBag.Suppliers = new SelectList(Suppliers, "SupplierId", "SupplierName");
            var Types = applicationDbContext.fridge_type.ToList();
            ViewBag.Types = new SelectList(Types, "FridgeTypeID", "FridgeType");
            var orderstatus = applicationDbContext.orderStatus.ToList();
            ViewBag.OrderStatus = new SelectList(orderstatus, "OrderStatusId", "OrderDesc");
        }
        public IActionResult UpdateOrder(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pOrder = applicationDbContext.orders.Find(id);
            if (pOrder == null)
            {
                return NotFound();
            }
            var orderstatus = applicationDbContext.orderStatus.ToList();
            ViewBag.OrderStatus = new SelectList(orderstatus, "OrderStatusId", "OrderDesc");
            return View(pOrder);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateOrder(PurchaseOrder purchase)
        {
            if(purchase == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                applicationDbContext.Update(purchase);
                applicationDbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(purchase);    
        }
    }
}
