using System;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cool_Co_Fridge_Management.Data;
using Cool_Co_Fridge_Management.Models;
using System.Net;
using PdfSharpCore.Pdf;
using PdfSharpCore.Drawing;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using PdfSharp.Pdf;
using Microsoft.AspNetCore;
using Cool_Co_Fridge_Management.Attributes;


namespace Cool_Co_Fridge_Management.Controllers
{
    //[RoleAuthorize("Purchasing Manager")]
    public class RFQViewModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RFQViewModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RFQViewModels
        public async Task<IActionResult> Index()
        {
            List<RFQuotation> rfqList = await _context.RFQuotation.Include(r => r.Supplier)
                .Include(r => r.Quotations)
                .ToListAsync();

            //Map model to modelView
            IEnumerable<RFQViewModel> viewModelList = rfqList.Select(rfq => new RFQViewModel
            {
                // mapping properties from RFQuotation to RFQViewModel
                RFQuotation = rfq,
                Supplier = rfq.Supplier,
                IsQuotationAdded = rfq.Quotations.Any()

            });
                
            return View(viewModelList);
        }

        // GET: RFQViewModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var rfq = await _context.RFQuotation.Include(r => r.Supplier).Include(r => r.Quotations).ThenInclude(q => q.Items).FirstOrDefaultAsync(r => r.RFQID == id);

            if(rfq == null)
            {
                return NotFound();
            }

            var viewModel = new RFQViewModel
            {
                RFQuotation = rfq,
                Supplier = rfq.Supplier,
                QuotationViewModel = rfq.Quotations.Select(q => new QuotationViewModel
                {
                    RFQID = q.RFQuotation.RFQID,
                    QuotationAmount = q.QuotationAmount,
                    PaymentTerms = q.PaymentTerms,
                    DeliveryDate = q.DeliveryDate??DateTime.MinValue,
                    QuotationNotes = q.QuotationNotes,
                    SupplierId = q.SupplierId,
                    SupplierName = q.Supplier.SupplierName,
                    ItemDesc = q.Items.Select(i => i.ItemName).FirstOrDefault(), // Example for ItemDesc
                    Quantity = q.Items.Select(i => i.Quantity).FirstOrDefault(), // Example for Quantity
                    UnitPrice = q.Items.Select(i => i.UnitPrice).FirstOrDefault(), // Example for UnitPrice
                    TotalAmount = q.Items.Select(i => i.TotalAmount).FirstOrDefault()
                }).FirstOrDefault()
            };
            return View(viewModel);
        }

        // GET: RFQViewModels/Create
        public IActionResult Create()
        {
            var rFQViewModel = new RFQViewModel
            {
                RFQuotation = new RFQuotation()
            };
            var supplier = _context.suppliers.ToList();
            ViewBag.Suppliers = new SelectList(supplier, "SupplierId", "SupplierName");

            return View();
        }

        // POST: RFQViewModels/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RFQuotation")] RFQViewModel rFQViewModel)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine($"Selected SupplierId: {rFQViewModel.RFQuotation.SupplierId}"); //AI
                var selectedSupplier = await _context.suppliers.FindAsync(rFQViewModel.RFQuotation.SupplierId);
                if (selectedSupplier == null)
                {
                    ModelState.AddModelError("RFQuotation.SupplierId", "Invalid supplier");
                    PopulateSupplierDropDown();
                    return View(rFQViewModel);
                }
                rFQViewModel.Supplier = selectedSupplier;

                _context.Add(rFQViewModel.RFQuotation);
                await _context.SaveChangesAsync();

                var pdfBytes = GeneratePdfFromHtml(rFQViewModel);
                //var recepientEmail = selectedSupplier.Email;
                ViewBag.SuccessMessage = "Request for Quotation has been sent and downloaded successfully";

                //SendEmailWithAttachment(selectedSupplier.Email, pdfBytes);
                return File(pdfBytes, "application/pdf", "GeneratedRFQ.pdf");
            }
            PopulateSupplierDropDown();
            return View(rFQViewModel);
        }
        private void PopulateSupplierDropDown() 
        {
            var supplier = _context.suppliers.ToList();
            ViewBag.Suppliers = new SelectList(supplier, "SupplierId", "SupplierName");
        }

        private byte[] GeneratePdfFromHtml(RFQViewModel model)
        {
            using (var stream = new MemoryStream())
            {
                PdfSharpCore.Pdf.PdfDocument document = new PdfSharpCore.Pdf.PdfDocument();
                document.Info.Title = "RFQ Form";
                
                //empty page
                PdfSharpCore.Pdf.PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);
                XFont fontRegular = new XFont("Verdana", 12, XFontStyle.Regular);
                XFont fontBold = new XFont("Verdana", 14, XFontStyle.Bold);

                //Company details
                gfx.DrawString("Company Details", fontBold, XBrushes.Black, new XRect(20,20, page.Width, 20), XStringFormats.TopLeft);
                gfx.DrawString("Company name: Cool Co.", fontRegular, XBrushes.Black, new XRect(20, 50, page.Width, 20), XStringFormats.TopLeft);
                gfx.DrawString("Company Address: Summerstrand, Gqeberha, Eastern Cape, 6001", fontRegular, XBrushes.Black, new XRect(20, 70, page.Width, 20), XStringFormats.TopLeft);

                //Supplier details
                gfx.DrawString("Supplier Details", fontBold, XBrushes.Black, new XRect(20, 100, page.Width, 20), XStringFormats.TopLeft);
                gfx.DrawString($"Supplier Name: {model.Supplier.SupplierName}", fontRegular, XBrushes.Black, new XRect(20, 130, page.Width, 20), XStringFormats.TopLeft);
                gfx.DrawString($"Supplier Email: {model.Supplier.Email}", fontRegular, XBrushes.Black, new XRect(20, 150, page.Width, 20), XStringFormats.TopLeft);
                gfx.DrawString($"Contact Number: {model.Supplier.ContactNumber}", fontRegular, XBrushes.Black, new XRect(20, 170, page.Width, 20), XStringFormats.TopLeft);

                //RFQ Details
                gfx.DrawString("RFQ Details", fontBold, XBrushes.Black, new XRect(20, 180, page.Width, 20), XStringFormats.TopLeft);
                gfx.DrawString($"Product Name: {model.RFQuotation.ItemDesc}", fontRegular, XBrushes.Black, new XRect(20,210, page.Width,20), XStringFormats.TopLeft);
                gfx.DrawString($"Quantity: {model.RFQuotation.Quantity}", fontRegular, XBrushes.Black, new XRect(20,230, page.Width,20), XStringFormats.TopLeft);
                gfx.DrawString($"Price: {model.RFQuotation.PrinceRange}", fontRegular, XBrushes.Black, new XRect(20,250,page.Width,20), XStringFormats.TopLeft);
  
                document.Save(stream);
                return stream.ToArray();
            }
        }
        // GET: RFQViewModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rfQuotation = await _context.RFQuotation.Include(r => r.Supplier).FirstOrDefaultAsync(m => m.RFQID == id);
            if (rfQuotation == null)
            {
                return NotFound();
            }

            var quotation = await _context.Quotations.Include(q => q.Items).FirstOrDefaultAsync(q => q.RFQID == rfQuotation.RFQID);

            var rFQViewModel = new RFQViewModel
            {
                RFQuotation = rfQuotation,
                Supplier = rfQuotation.Supplier,
                QuotationViewModel = new QuotationViewModel
                {
                    RFQID = rfQuotation.RFQID,
                    SupplierId = rfQuotation.SupplierId,
                    SupplierName = rfQuotation.Supplier?.SupplierName,
                    ItemDesc = rfQuotation.ItemDesc,
                    Quantity = rfQuotation.Quantity,

                    //populate quotation fields
                    QuotationAmount = quotation?.QuotationAmount ?? 0,
                    PaymentTerms = quotation?.PaymentTerms ?? string.Empty,
                    DeliveryDate = quotation?.DeliveryDate ?? DateTime.Now,
                    QuotationNotes = quotation?.QuotationNotes ?? string.Empty,

                    UnitPrice = quotation?.Items.FirstOrDefault()?.UnitPrice ??0,
                    TotalAmount = quotation?.Items.FirstOrDefault()?.TotalAmount ??0
                }
            };
            return View(rFQViewModel);
        }

        //private void SendEmailWithAttachment(string recipientEmail, byte[] pdfBytes)
        //{
        //    var smtpClient = new SmtpClient("smtp.gmail.com")
        //    {
        //        Port = 587,
        //        Credentials = new NetworkCredential("ayakhac16@gmail.com", "Athandile1"),
        //        EnableSsl = true,
        //    };

        //    var mailMessage = new MailMessage
        //    {
        //        From = new MailAddress("ayakhac16@gmail.com"),
        //        Subject = "RFQ Form",
        //        Body = "Please find the attached RFQ form",
        //        IsBodyHtml = true,
        //    };
        //    mailMessage.To.Add(recipientEmail);

        //    mailMessage.Attachments.Add(new Attachment(new MemoryStream(pdfBytes), "RFQ.pdf", "application/pdf"));

        //    smtpClient.Send(mailMessage);
        //}

        private async Task<string> RenderRazorViewToString(string viewName, object model)
        {
            var viewEngine = HttpContext.RequestServices.GetService<IRazorViewEngine>();
            var tempDataProvider = HttpContext.RequestServices.GetService<ITempDataProvider>();
            var serviceProvider = HttpContext.RequestServices.GetService<IServiceProvider>();

            var actionContext = new ActionContext(HttpContext, RouteData, ControllerContext.ActionDescriptor);

            using (var sw = new StringWriter())
            {
                var viewResult = viewEngine.FindView(actionContext, viewName, false);

                if (!viewResult.Success)
                {
                    throw new InvalidOperationException($"Unable to find view '{viewName}'");
                }

                var viewContext = new ViewContext(
                    actionContext,
                    viewResult.View,
                    ViewData,
                    TempData,
                    sw,
                    new HtmlHelperOptions());

                await viewResult.View.RenderAsync(viewContext);
                return sw.ToString();
            }
        }
        // POST: RFQViewModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  RFQViewModel viewModel)
        {
            if (id != viewModel.RFQuotation.RFQID)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                var rfQuotation = await _context.RFQuotation.Include(r => r.Supplier).FirstOrDefaultAsync(m => m.RFQID == id);
                if (rfQuotation == null)
                {
                    return NotFound();
                }

                viewModel.RFQuotation = rfQuotation;
                viewModel.Supplier = rfQuotation.Supplier;

                return View(viewModel);
            }
            var quotation = await _context.Quotations.Include(q => q.Items).FirstOrDefaultAsync(q => q.RFQID == id);

            if(quotation == null)
            {
                quotation = new Quotation
                {
                    RFQID = viewModel.RFQuotation.RFQID,
                    SupplierId = viewModel.Supplier.SupplierId,
                    QuotationAmount = viewModel.QuotationViewModel.QuotationAmount,
                    PaymentTerms = viewModel.QuotationViewModel.PaymentTerms,
                    DeliveryDate = viewModel.QuotationViewModel.DeliveryDate,
                    QuotationNotes = viewModel.QuotationViewModel.QuotationNotes,
                    Items = new List<QuotationItem>()
                };
                _context.Quotations.Add(quotation);
            }
            else
            {
                //if a quotation exists
                quotation.QuotationAmount = viewModel.QuotationViewModel.QuotationAmount;
                quotation.PaymentTerms = viewModel.QuotationViewModel.PaymentTerms;
                quotation.DeliveryDate = viewModel.QuotationViewModel.DeliveryDate;
                quotation.QuotationNotes = viewModel.QuotationViewModel.QuotationNotes;
                quotation.SupplierId = viewModel.Supplier.SupplierId;
            }
            var quotationItem = quotation.Items.FirstOrDefault();
            if (quotationItem == null)
            {
                quotationItem = new QuotationItem
                {
                    ItemName = viewModel.RFQuotation.ItemDesc,
                    Quantity = viewModel.RFQuotation.Quantity,
                    UnitPrice = viewModel.QuotationViewModel.UnitPrice,
                    TotalAmount = viewModel.QuotationViewModel.UnitPrice * viewModel.QuotationViewModel.Quantity
                };

                quotation.Items.Add(quotationItem);
            }
            else
            {
                //updating existing Quotation item
                quotationItem.ItemName = viewModel.RFQuotation.ItemDesc;
                quotationItem.Quantity = viewModel.RFQuotation.Quantity;
                quotationItem.UnitPrice = viewModel.QuotationViewModel.UnitPrice;
                quotationItem.TotalAmount = viewModel.QuotationViewModel.UnitPrice * viewModel.QuotationViewModel.Quantity;
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RFQuotationExists(viewModel.RFQuotation.RFQID))
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
        private bool RFQuotationExists(int id)
        {
            return _context.RFQuotation.Any(e => e.RFQID == id);
        }

        // GET: RFQViewModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rFQViewModel = await _context.RFQuotation
                .FirstOrDefaultAsync(m => m.RFQID == id);
            if (rFQViewModel == null)
            {
                return NotFound();
            }

            return View(rFQViewModel);
        }

        // POST: RFQViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rFQViewModel = await _context.RFQuotation.FindAsync(id);
            if (rFQViewModel != null)
            {
                _context.RFQuotation.Remove(rFQViewModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RFQViewModelExists(int id)
        {
            return _context.RFQuotation.Any(e => e.RFQID == id);
        }
    }
}
