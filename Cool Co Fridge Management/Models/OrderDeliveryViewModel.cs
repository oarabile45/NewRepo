namespace Cool_Co_Fridge_Management.Models
{
    public class OrderDeliveryViewModel
    {
        public DeliveryNote? DeliveryNote { get; set; }
        public PurchaseOrder? PurchaseOrder { get; set; }
        public bool HasDeliveryNote { get; set; }
    }
}
