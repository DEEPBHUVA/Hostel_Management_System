namespace Hostel_Management_System.Areas.MST_BillType.Models
{
	public class MST_BillTypeModel
	{
        public int? BillTypeID { get; set; }
		public string BillType { get; set;}
		public DateTime Created {  get; set; }
		public DateTime Modified { get; set; }
    }

	public class MST_BillType_DropDown
	{
		public int BillTypeID { get; set; }
		public string BillType { get; set; }
	}
}
