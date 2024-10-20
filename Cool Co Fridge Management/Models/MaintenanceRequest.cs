using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace Cool_Co_Fridge_Management.Models
{
    public class MaintenanceRequest
    {
        [Key]
        public int BookingID { get; set; }
      
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
      
        [Required]
        public string Address { get; set; }
   
        [Required]
        public DateTime RequestedDate { get; set; }

        public DateTime? ApprovedDate { get; set; }
        public bool IsApprovedByTechnician { get; set; } = false;
        public string UserConfirmationStatus { get; set; } = "Pending";
        public RequestStatus Status { get; set; }
        public string FaultDescription { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public Users User { get; set; }


    }

    public enum RequestStatus
    {
        Pending,
        Approved,
        Rejected,
        Completed
    }


}
