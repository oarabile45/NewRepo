using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Cool_Co_Fridge_Management.Models
{
    public class PurchaseOrder
    {
        [Key]
        public int OrderID { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public DateOnly OrderedDate { get; set; }
        public int SupplierId { get; set; }
        public int FridgeTypeID { get; set; }
        public int OrderStatusId { get; set; }

        [ForeignKey("SupplierId")]
        public Supplier? Supplier { get; set; }
        [ForeignKey("FridgeTypeID")]
        public Fridge_Type? Fridge_Type { get; set; }
        [ForeignKey("OrderStatusId")]
        public OrderStatus? OrderStatus { get; set; }

    }
}
