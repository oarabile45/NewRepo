using System;
using System.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cool_Co_Fridge_Management.Models
{
    public class Fridge_Stock
    {
        [Key]
        public int StockID { get; set; }
        [Required]
        public string ItemName { get; set; }
        [Required]
        public int FridgeTypeID { get; set; } /*foreign key from FridgeType table*/
        [ForeignKey("FridgeTypeID")]
        public Fridge_Type Fridge_Type { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public bool Availability { get; set; } 
    }
}
