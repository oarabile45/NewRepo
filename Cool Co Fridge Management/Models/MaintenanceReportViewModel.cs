using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Cool_Co_Fridge_Management.Models
{
    public class MaintenanceReportViewModel
    {
        public  int BookingID { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public DateTime RequestedDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public RequestStatus status { get; set; }
        public string FaultDescription { get; set; }
        public bool IsApprovedByTechnician { get; set; }
    }
}
