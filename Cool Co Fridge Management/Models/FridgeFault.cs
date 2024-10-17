using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cool_Co_Fridge_Management.Models
{
	public class FridgeFault
	{
		[Key]
		public int FridgeFaultID { get; set; }

		[Display(Name = "FAULT TYPE")]
		public int FaultTypeID { get; set; }
		[ForeignKey("FaultTypeID")]

		[Display(Name = "FAULT TYPE")]
		public FaultType faultType { get; set; }

		[Display(Name = "REPORTED ON")]
		public DateTime? ReportedOn { get; set; }

		public int StatusID { get; set; }
		[ForeignKey("StatusID")]

		[Display(Name = "STATUS")]
		public Status status { get; set; }

		[Display(Name = "REPAIR SCHEDULED FOR")]
		public DateOnly? RepairDate { get; set; }

		[ForeignKey("ID")]
		public Users? Users { get; set; }

        [Display(Name = "CONDITION")]
        public int? ConditionID { get; set; }

		[ForeignKey("ConditionID")]
		[Display(Name = "CONDITION")]
		public FridgeCondition? Condition { get; set; }
	}
}
