using System;
using System.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
namespace Cool_Co_Fridge_Management.Models
{
    public class Fridge_Type
    {
        [Key]
        public int FridgeTypeID { get; set; }
        [Required]
        public string FridgeType { get; set; }
        [Required]
        public int SupplierID { get; set; } /*foreign from supplier table*/
        [Required]
        public bool Availablity { get; set; }
        public Supplier Supplier { get; set; }
    }
}
