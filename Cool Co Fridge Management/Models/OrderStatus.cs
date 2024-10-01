using System.ComponentModel.DataAnnotations;

namespace Cool_Co_Fridge_Management.Models
{
    public class OrderStatus
    {
        [Key]
        public int OrderStatusId { get; set; }
        public string OrderDesc { get; set; }
    }
}
