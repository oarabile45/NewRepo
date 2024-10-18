namespace Cool_Co_Fridge_Management.Models
{
    public class QuotationItem
    {
        public int QuotationItemId { get; set; }
        public int QuotationId { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalAmount { get; set; }
        public Quotation Quotations { get; set; }
    }
}
