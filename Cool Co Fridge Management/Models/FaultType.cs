using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace Cool_Co_Fridge_Management.Models
{
	public class FaultType
	{
		[Key]
		public int FaultTypeID { get; set; }

		[Required]
		[Display(Name = "FAULT TYPE")]
		public string FaultTypeDesc { get; set; }

	
		
	}
}
