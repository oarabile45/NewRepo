using System;
using System.ComponentModel.DataAnnotations;
using Cool_Co_Fridge_Management.Models;

namespace Cool_Co_Fridge_Management.ViewModels
{
    public class MaintenanceRequestViewModel
    {
        public int BookingID { get; set; }
        [Required]
        public int UserId { get; set; } // The ID of the user associated with the maintenance request

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public DateTime RequestedDate { get; set; }

        public string FaultDescription { get; set; }

        public bool IsApprovedByTechnician { get; set; }

        public string UserConfirmationStatus { get; set; } = "Pending";

        public RequestStatus Status { get; set; }
    }
}
