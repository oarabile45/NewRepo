using Cool_Co_Fridge_Management.Data;
using Cool_Co_Fridge_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.Style;

namespace Cool_Co_Fridge_Management.Controllers
{
    public class QuotationController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public QuotationController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult QuotationIndex()
        {
            var quotation = _dbContext.Quotations
                .Include(q => q.RFQuotation)
                .Include(q => q.Supplier)
                .Select(q => new QuotationViewModel
                {
                    RFQID = q.RFQuotation.RFQID,
                    QuotationAmount = q.QuotationAmount,
                    SupplierName = q.Supplier.SupplierName,
                    DeliveryDate = q.DeliveryDate??DateTime.MinValue
                }).ToList();

            if (!quotation.Any())
            {
                TempData["Error Message"] = "No quotations found";
            }

            return View(quotation);
        }

        public IActionResult CreateQuotation(int rfqId)
        {
            var rfq = _dbContext.RFQuotation.Include(r => r.Supplier).FirstOrDefault(r => r.RFQID == rfqId);

            if(rfq == null)
            {
                return NotFound();
            }
            var viewModel = new QuotationViewModel
            {
                RFQID = rfq.RFQID,
                SupplierId = rfq.SupplierId,
                SupplierName = rfq.Supplier.SupplierName,
                ItemDesc = rfq.ItemDesc,
                Quantity = rfq.Quantity
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateQuotation(QuotationViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine($"ViewModel RFQID: {viewModel.RFQID}");

                var rfq = _dbContext.RFQuotation.Include(r => r.Supplier).FirstOrDefault(r => r.RFQID == viewModel.RFQID);
                
                if(rfq == null)
                {
                    return NotFound();
                }
                var quotation = new Quotation
                {
                    RFQID = viewModel.RFQID,
                    SupplierId = rfq.SupplierId,
                    QuotationAmount = viewModel.QuotationAmount,
                    PaymentTerms = viewModel.PaymentTerms,
                    DeliveryDate = viewModel.DeliveryDate,
                    QuotationNotes = viewModel.QuotationNotes,
                    ReceivedDate = DateTime.Now,
                };

                _dbContext.Quotations.Add(quotation);
                _dbContext.SaveChanges();

                var quotationItem = new QuotationItem
                {
                    QuotationId = quotation.QuotationId,
                    ItemName = viewModel.ItemDesc,
                    Quantity = viewModel.Quantity,
                    UnitPrice = viewModel.UnitPrice,
                    TotalAmount = viewModel.UnitPrice * viewModel.Quantity
                };

                _dbContext.QuotationItems.Add(quotationItem);
                _dbContext.SaveChanges();

                return RedirectToAction("QuotationIndex");
            }
            return View(viewModel);
        }
        public IActionResult QuotationDetails(int id)
        {
            var quotation = _dbContext.Quotations.Include(q => q.Supplier).Include(q => q.RFQuotation).Select(q => new QuotationViewModel
            {
                RFQID = q.RFQID,
                QuotationAmount = q.QuotationAmount,
                PaymentTerms = q.PaymentTerms,
                DeliveryDate = q.DeliveryDate?? DateTime.MinValue,
                QuotationNotes = q.QuotationNotes,
                SupplierId = q.Supplier.SupplierId,
                SupplierName = q.Supplier.SupplierName,
                ItemDesc = q.RFQuotation.ItemDesc,
                Quantity = q.RFQuotation.Quantity,
                UnitPrice = q.Items.Select(i => i.UnitPrice).FirstOrDefault(),
                TotalAmount = q.Items.Select(i => i.TotalAmount).FirstOrDefault()
            }).FirstOrDefault(q => q.RFQID == id);
            
            if(quotation == null)
            {
                return NotFound();
            }
            return View(quotation);
        }

















        //public IActionResult CreateQuotation(int rFQ)
        //{
        //    var viewModel = new QuotationViewModel
        //    {
        //        Quotation = new Quotation
        //        {
        //            RFQID = rFQ
        //        }
        //    };
        //    return View(viewModel);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult CreateQuotation(QuotationViewModel viewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var quotation = new Quotation
        //        {
        //            RFQID = viewModel.Quotation.RFQID,
        //            SupplierId = viewModel.Quotation.SupplierId,
        //            QuotationAmount = viewModel.Quotation.QuotationAmount,
        //            ReceivedDate = DateTime.Now,
        //            PaymentTerms = viewModel.Quotation.PaymentTerms,
        //            DeliveryDate = viewModel.Quotation.DeliveryDate,
        //            QuotationNotes = viewModel.Quotation.QuotationNotes,
        //            Items = new List<QuotationItem>()
        //        };
        //        if(viewModel.QuotationItem != null)
        //        {
        //            var quotationItem = new QuotationItem
        //            {
        //                ItemName = viewModel.QuotationItem.ItemName,
        //                Quantity = viewModel.QuotationItem.Quantity,
        //                UnitPrice = viewModel.QuotationItem.UnitPrice,
        //                TotalAmount = viewModel.QuotationItem.Quantity * viewModel.QuotationItem.UnitPrice
        //            };

        //            quotation.Items.Add(quotationItem);
        //        }
        //        _dbContext.Add(quotation);
        //        _dbContext.SaveChanges();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(viewModel);
        //}
    }
}
