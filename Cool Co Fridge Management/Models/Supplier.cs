using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace Cool_Co_Fridge_Management.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; }
        [Required]
        public string SupplierName { get; set; } 
        [Required] //validation for 10 digits
        public string ContactNumber {  get; set; }
        [Required]
        public string Email { get; set; }
        //public ICollection<RFQuotation> Quotations { get; set; } 
    }
}
