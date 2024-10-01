using System.ComponentModel.DataAnnotations;

namespace Cool_Co_Fridge_Management.Models
{
	public class Status
	{
		[Key]
		public int StatusID { get; set; }

		[Required]
		[Display(Name = "STATUS")]
		public string StatusDesc { get; set; }
	}
}
