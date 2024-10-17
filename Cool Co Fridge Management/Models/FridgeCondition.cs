using System.ComponentModel.DataAnnotations;

namespace Cool_Co_Fridge_Management.Models
{
    public class FridgeCondition
    {
        [Key]
        public int ConditionID { get; set; }

        [Required]
		[Display(Name = "CONDITION")]
		public string ConditionDesc { get; set; }

        //might have to put fridgeType as foreign key not sure yet
    }
}
