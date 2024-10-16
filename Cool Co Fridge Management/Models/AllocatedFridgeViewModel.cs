using System;
using System.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cool_Co_Fridge_Management.Models
{
        public class AllocatedFridgeViewModel
        {
            public int FridgeAllocationID { get; set; }
            public string Email { get; set; }
            public string ItemName { get; set; }
            public string FridgeType { get; set; }
            public DateTime AllocationDate { get; set; }
        }
}
