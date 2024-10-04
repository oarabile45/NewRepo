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
                .Select(o => new PurchaseOrder
                {
                    OrderID = o.OrderID,
                    Fridge_Type = o.Fridge_Type,
                    Supplier = o.Supplier,
                    Quantity = o.Quantity,
                    OrderedDate = o.OrderedDate,
                    OrderStatus = o.OrderStatus,
                    //DeliveryNoteId = o.DeliveryNoteId
                })
                .ToList();

            if (!orders.Any())
            {
                TempData["Error Message"] = "No orders found";
            }
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
                pOrder.DeliveryNoteId = null;
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
            ViewData["Title"] = "Order Details";
            if (id == null)
            {
                return NotFound();
            }

            var pOrder = applicationDbContext.orders.Find(id);
            if(pOrder == null)
            {

                return NotFound();
            }
            var deliveryNote = applicationDbContext.DeliveryNotes.FirstOrDefault(d => d.DeliveryNoteId == pOrder.DeliveryNoteId) ?? new DeliveryNote();

            var viewModel = new OrderDeliveryViewModel
            {
                PurchaseOrder = pOrder,
                DeliveryNote = deliveryNote
            };

            var orderstatus = applicationDbContext.orderStatus.ToList();
            ViewBag.OrderStatus = new SelectList(orderstatus, "OrderStatusId", "OrderDesc");

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateOrder(int id,OrderDeliveryViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var orderstatus = applicationDbContext.orderStatus.ToList();
                ViewBag.OrderStatus = new SelectList(orderstatus, "OrderStatusId", "OrderDesc");
                return View(viewModel);
            }

            var order = applicationDbContext.orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            //if user is adding a delivery note
            if(viewModel.HasDeliveryNote && (viewModel.DeliveryNote.DeliveryNoteId == 0 || viewModel.DeliveryNote.DeliveryNoteId == null))
            {
                var newDeliveryNote = new DeliveryNote
                {
                    DeliveredDate = viewModel.DeliveryNote.DeliveredDate,
                    ReceiverName = viewModel.DeliveryNote.ReceiverName,
                    DeliveryDetails = viewModel.DeliveryNote.DeliveryDetails,
                    OrderID = order.OrderID
                };

                applicationDbContext.DeliveryNotes.Add(newDeliveryNote);
                applicationDbContext.SaveChanges();

                order.DeliveryNoteId = newDeliveryNote.DeliveryNoteId;
            }
            else if (viewModel.HasDeliveryNote)
            {
                var existingNote = applicationDbContext.DeliveryNotes.Find(viewModel.DeliveryNote.DeliveryNoteId);
                if (existingNote != null)
                {
                    existingNote.DeliveredDate = viewModel.DeliveryNote.DeliveredDate;
                    existingNote.ReceiverName = viewModel.DeliveryNote.ReceiverName;
                    existingNote.DeliveryDetails = viewModel.DeliveryNote.DeliveryDetails;
                    existingNote.OrderID = order.OrderID;

                    applicationDbContext.Update(existingNote);
                }

                //assigning note to an order
                if(order.DeliveryNoteId != existingNote.DeliveryNoteId)
                {
                    order.DeliveryNoteId = existingNote.DeliveryNoteId;
                }
            }


            order.OrderStatusId = viewModel.PurchaseOrder.OrderStatusId;

            applicationDbContext.Update(order);
            applicationDbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult OrderDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = applicationDbContext.orders
                .Include(o => o.Supplier)
                .Include(o => o.Fridge_Type)
                .Include(o => o.OrderStatus)
                .Include(o => o.DeliveryNote)
                .FirstOrDefault(o => o.OrderID == id);

            if(order == null)
            {
                return NotFound();
            }
            return View(order);
        }
    }
}
