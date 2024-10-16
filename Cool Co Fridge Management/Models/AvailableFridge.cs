using System;
using System.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cool_Co_Fridge_Management.Models
{
    public class AvailableFridge
    {
        public int StockID { get; set; } 
        public string ItemName { get; set; }
        public int FridgeTypeID { get; set; }
        public string FridgeType { get; set; }
        public bool Availability { get; set; }
    }

}
