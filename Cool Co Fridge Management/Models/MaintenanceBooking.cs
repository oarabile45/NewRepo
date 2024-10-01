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
    public class MaintenanceBooking
    {
        [Key]
        public int BookingID { get; set; }
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public Users User { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //[ForeignKey("FirstName, LastName")]
        //public Users Users { get; set;}



        public string Address { get; set; }
        //[ForeignKey("Address")]
        //public Users address { get; set; }
        [Required]
        public DateTime RequestedDate { get; set; }

        public DateTime? ApprovedDate { get; set; }
        public RequestStatus status { get; set; }


    }

    public enum RequestStatus
    {
        Pending,
        Approved,
        Rejected,
        Completed
    }


}
