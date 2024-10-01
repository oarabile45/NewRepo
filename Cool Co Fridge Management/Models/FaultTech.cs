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
    public class FaultTech
    {
        [Key]
        public int FaultTechId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
