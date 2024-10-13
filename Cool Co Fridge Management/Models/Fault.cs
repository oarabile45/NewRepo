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
    public class Fault
    {
        [Key]
        public int FaultId { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int ID { get; set; }
        [ForeignKey("ID")]
        public MaintenanceRequest MaintenanceBooking { get; set; }
        [Required]
        public int FaultTechId { get; set; }
        [ForeignKey("FaultTechId")]
        public FaultTech FaultTech { get; set; }
    }
}
