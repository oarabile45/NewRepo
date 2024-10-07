using Microsoft.AspNetCore.Mvc;
using Cool_Co_Fridge_Management.Models;
using Cool_Co_Fridge_Management.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using PdfSharp.Pdf;



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

        public IActionResult GeneratePurchaseOrderReport()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            //get data from database
            var reportData = (from po in applicationDbContext.orders
                              join dn in applicationDbContext.DeliveryNotes on po.DeliveryNoteId equals dn.DeliveryNoteId into pdn
                              from dn in pdn.DefaultIfEmpty()
                              join s in applicationDbContext.suppliers on po.SupplierId equals s.SupplierId
                              join ft in applicationDbContext.fridge_type on po.FridgeTypeID equals ft.FridgeTypeID
                              join os in applicationDbContext.orderStatus on po.OrderStatusId equals os.OrderStatusId
                              select new OrderReportViewModel 
                              { 
                                  OrderID = po.OrderID,
                                  ItemName = po.ItemName,
                                  Quantity = po.Quantity,
                                  OrderedDate = po.OrderedDate,
                                  Supplier = s.SupplierName,
                                  FridgeType = ft.FridgeType,
                                  OrderStatus = os.OrderDesc,
                                  DeliveryDate = dn != null ? dn.DeliveredDate : null,
                                  DeliveryNoteDetails = dn != null ? dn.DeliveryDetails: "N/A"

                              }).ToList();


            //generating Excel file
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("OrderReportViewModel");

                //Headers
                worksheet.Cells[1, 1].Value = "OrderID";
                worksheet.Cells[1, 2].Value = "Item Name";
                worksheet.Cells[1, 3].Value = "Quantity";
                worksheet.Cells[1, 4].Value = "Ordered Date";
                worksheet.Cells[1, 5].Value = "Supplier Name";
                worksheet.Cells[1, 6].Value = "Fridge Type";
                worksheet.Cells[1, 7].Value = "Order Status";
                worksheet.Cells[1, 8].Value = "Delivery Date";
                worksheet.Cells[1, 9].Value = "Delivery Note Details";

                //Add the data
                for(int i = 0; i< reportData.Count; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = reportData[i].OrderID;
                    worksheet.Cells[i + 2, 2].Value = reportData[i].ItemName;
                    worksheet.Cells[i + 2, 3].Value = reportData[i].Quantity;
                    worksheet.Cells[i + 2, 4].Value = reportData[i].OrderedDate.ToString();
                    worksheet.Cells[i + 2, 5].Value = reportData[i].Supplier;
                    worksheet.Cells[i + 2, 6].Value = reportData[i].FridgeType;
                    worksheet.Cells[i + 2, 7].Value = reportData[i].OrderStatus;
                    worksheet.Cells[i + 2, 8].Value = reportData[i].DeliveryDate?.ToString();
                    worksheet.Cells[i + 2, 9].Value = reportData[i].DeliveryNoteDetails;

                }
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                //export to excel file
                var excelFile = new MemoryStream();
                package.SaveAs(excelFile);
                excelFile.Position = 0;

                string excelName = $"Purchase Order Report - {DateTime.Now:yyyyMMddHH}.xlsx";
                return File(excelFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);

            }
        }
    }
}
