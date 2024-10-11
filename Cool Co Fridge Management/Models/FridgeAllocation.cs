using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cool_Co_Fridge_Management.Models
{
    public class FridgeAllocation
    {
        [Key]
        public int FridgeAllocationID { get; set; }

        [Required]
        public int Id { get; set; }

        [Required]
        public int FridgeID { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public int FridgeRequestID { get; set; }

        [Required]
        public string Email { get; set; }

        // Navigation properties
        [ForeignKey("Id")]
        public Users users { get; set; }
        [ForeignKey("FridgeRequestId")]
        public FridgeRequest FridgeRequest { get; set; }
        [ForeignKey("FridgeID")]
        public Fridge_Stock Fridge_Stock { get; set; } 
    }
}







