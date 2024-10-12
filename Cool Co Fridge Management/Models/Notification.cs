using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace Cool_Co_Fridge_Management.Models
{
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; } = false;
        [Required]
        public int FaultTechId { get; set; }
        [ForeignKey("FaultTechId")]
        public FaultTech FaultTech { get; set; }
        public int MaintenanceTechID { get; set; }
        [ForeignKey("MaintenanceTechID")]
        public MaintenanceTech MaintenanceTech { get; set; }
    }
}
