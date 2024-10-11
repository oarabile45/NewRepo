using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cool_Co_Fridge_Management.Models
{
    public class FridgeRequest
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FridgeRequestID { get; set; }

        [Required]
        public string Email { get; set; }

        // Foreign key to Fridge_Type
        [Required]
        public int FridgeTypeID { get; set; }

        [Required]
        [ForeignKey("FridgeTypeID")]
        public Fridge_Type FridgeType { get; set; }

        [Required]
        public string Status { get; set; } = "Pending";
    }
}
