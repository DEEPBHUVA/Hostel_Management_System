namespace Hostel_Management_System.Areas.MST_Visitor.Models
{
    public class MST_VisitorModel
    {
        public int? VisitorID { get; set; }
        public string VisitorName { get; set; }
        public string MobileNo { get; set; }
        public string Remark { get; set; }
        public bool IsSelected { get; set; }
        public DateTime DateIN { get; set; }
        public DateTime DateOUT { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
