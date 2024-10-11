using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cool_Co_Fridge_Management.Models
{
    public class PurchaseRequest
    {
        [Key]
        public int PurchaseRequestId { get; set; }
        [Required(ErrorMessage ="Fridge Type is required")]
        public int FridgeTypeID { get; set; }
        [Required(ErrorMessage = "Date is required")]
        public DateTime DateCreated { get; set; }
        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero")]
        public int RequestQuantity { get; set; }
        [Required(ErrorMessage = "Status is required")]
        public int OrderStatusId { get; set; }
        public Fridge_Type? FridgeType { get; set; }
        public OrderStatus? OrderStatus { get; set; }
    }
}
