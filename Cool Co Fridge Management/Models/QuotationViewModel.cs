namespace Cool_Co_Fridge_Management.Models
{
    public class QuotationViewModel
    {
        public int RFQID { get; set; }
        public decimal QuotationAmount { get; set; }
        public string PaymentTerms { get; set; }
        public DateTime DeliveryDate { get; set; } 
        public string QuotationNotes { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }

        public string ItemDesc { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalAmount { get; set; }
    } 
}
