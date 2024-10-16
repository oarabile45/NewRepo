using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Cool_Co_Fridge_Management.Models
{
    public class Quotation
    {
        [Key]
        public int QuotationId { get; set; }
        public int RFQID { get; set; }
        [ForeignKey("RFQID")]
        public virtual RFQuotation RFQuotation { get; set; } //dummy change
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public decimal QuotationAmount { get; set; }
        public DateTime ReceivedDate { get; set; }
        public string PaymentTerms { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string QuotationNotes { get; set; }
        public List<QuotationItem> Items { get; set; } = new List<QuotationItem>();
    }
}
