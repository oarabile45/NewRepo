using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Cool_Co_Fridge_Management.Models
{
    public class RFQViewModel
    {
        public RFQuotation RFQuotation { get; set; } = new RFQuotation();
        public Supplier? Supplier { get; set; }
    }
}
