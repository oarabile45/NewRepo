
using System.ComponentModel.DataAnnotations;

namespace Cool_Co_Fridge_Management.Models
{
    public class RFQuotation
    {
        [Key]
        public int RFQID { get; set; }
        [Required(ErrorMessage = "Item Name is required")]
        public string ItemDesc { get; set; }
        public int Quantity { get; set; }
        public decimal PrinceRange { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int SupplierId { get; set; }
        public Supplier? Supplier { get; set; }
    }
}
