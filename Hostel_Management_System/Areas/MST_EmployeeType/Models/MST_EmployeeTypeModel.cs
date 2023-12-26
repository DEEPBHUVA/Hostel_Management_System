using System.ComponentModel.DataAnnotations;

namespace Hostel_Management_System.Areas.MST_EmployeeType.Models
{
	public class MST_EmployeeTypeModel
	{
		public int? EmployeeTypeID { get; set; }

        [Required(ErrorMessage = "Enter Employee Type!")]
        public string EmployeeType { get; set; }
		public DateTime Created {  get; set; }
		public DateTime Modified { get; set; }
	}

	public class MST_EmployeeType_Dropdown
	{
		public int EmployeeTypeID { get; set;}
        public string EmployeeType { get; set; }
    }
}
