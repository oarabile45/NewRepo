using System.ComponentModel.DataAnnotations;
namespace Cool_Co_Fridge_Management.Models
{
    public class DeliveryNote
    {
        [Key]
        public int DeliveryNoteId { get; set; }
        public int OrderID { get; set; }
        public DateTime DeliveredDate { get; set; }  //comment
        public string ReceiverName { get; set; }
        public string DeliveryDetails { get; set; }
    }
}
