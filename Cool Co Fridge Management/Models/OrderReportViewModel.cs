namespace Cool_Co_Fridge_Management.Models
{
    public class OrderReportViewModel
    {
        public int OrderID { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public DateOnly OrderedDate { get; set; }
        public string Supplier { get; set; }
        public string FridgeType { get; set; }
        public string OrderStatus { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string DeliveryNoteDetails { get; set; }
    }
}
