using Hostel_Management_System.Areas.MST_Payment.Models;
using Hostel_Management_System.Areas.MST_Student.Models;

namespace Hostel_Management_System.Areas.MST_Student.ViewModel
{
	public class View_Model
	{
        public MST_StudentModel mst_StudentModel { get; set; }
        public List<MST_StudentModel> list_StudentModel { get; set; }
		public List<MST_PaymentModel> mst_PaymentModel { get; set; }
    }
}
