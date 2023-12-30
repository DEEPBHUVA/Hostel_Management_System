namespace Hostel_Management_System.Areas.MST_Notice.Models
{
    public class MST_NoticeModel
    {
        public int NoticeID { get; set; }
        public string Title { get; set;}
        public string Description { get; set;}
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
