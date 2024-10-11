using System;
using System.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cool_Co_Fridge_Management.ViewModels
{
    public class FridgeAllocationViewModel
    {
        public int FridgeAllocationID { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public int FridgeRequestID { get; set; }
        public string FridgeType { get; set; } // Fridge type from Fridge_Type model
        public string ItemName { get; set; } // Item name from Fridge_Stock
    }
}
